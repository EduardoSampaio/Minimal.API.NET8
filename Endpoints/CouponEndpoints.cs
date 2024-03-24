using AutoMapper;
using FluentValidation;
using MagicVilla_CouponAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Minimal.API.NET8.Models;
using Minimal.API.NET8.Models.DTO;
using System.Net;

namespace Minimal.API.NET8.Endpoints;

public static class CouponEndpoints
{
    public static void ConfigureCouponEndpoins(this WebApplication app)
    {
        app.MapGet("/api/coupon", GetAllCoupons)
           .WithName("GetCoupons")
           .Produces<APIResponse>(200);

        app.MapGet("/api/coupon/{id:int}", GetCoupon)
           .WithName("GetCoupon")
           .Produces<APIResponse>(200);

        app.MapPost("/api/coupon", CreateCoupon)
           .WithName("CreateCoupon")
           .Accepts<CouponDTO>("application/json")
           .Produces<APIResponse>(201)
           .Produces(400)
           .Produces(401)
           .RequireAuthorization();

        app.MapPut("/api/coupon/{id:int}", UpdateCoupon)
           .WithName("UpdateCoupon")
           .Produces(400)
           .Produces(401)
           .RequireAuthorization();

        app.MapDelete("/api/coupon/{id:int}", DeleteCoupon)
           .WithName("DeleteCoupon")
            .Produces(400)
           .Produces(401)
           .RequireAuthorization();
    }

    private async static Task<IResult> GetAllCoupons(IMapper _mapper, ICouponRepository repository)
    {
        var coupons = await repository.GetAllAsync();
        APIResponse reponse = new()
        {
            Result = _mapper.Map<IEnumerable<CouponDTO>>(coupons),
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK
        };

        return Results.Ok(reponse);
    }

    private async static Task<IResult> GetCoupon(IMapper _mapper, ICouponRepository repository, int id)
    {
        var coupon = await repository.GetAsync(id);
        APIResponse response = new()
        {
            Result = _mapper.Map<CouponDTO>(coupon),
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK
        };

        return Results.Ok(response);
    }

    private async static Task<IResult> CreateCoupon(IMapper _mapper,
            ICouponRepository repository,
            IValidator<CouponCreateDTO> _validation,
            [FromBody] CouponCreateDTO couponCreateDTO)
    {
        var validationResult = await _validation.ValidateAsync(couponCreateDTO);

        APIResponse response = new()
        {
            IsSuccess = true,
            StatusCode = HttpStatusCode.Created
        };

        if (!validationResult.IsValid)
        {
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
            return Results.BadRequest(response);
        }

        Coupon coupon = _mapper.Map<Coupon>(couponCreateDTO);
        await repository.CreateAsync(coupon);
        await repository.SaveAsync();

        response.Result = coupon;
        return Results.CreatedAtRoute("GetCoupon", new { Id = coupon.Id }, response);
    }

    private async static Task<IResult> UpdateCoupon(ICouponRepository repository, int id,
            [FromBody] CouponUpdateDTO couponUpdateDTO)
    {
        var coupon = await repository.GetAsync(id);
        coupon.LastUpdated = DateTime.UtcNow;
        coupon.Name = couponUpdateDTO.Name;
        coupon.Percent = couponUpdateDTO.Percent;

        await repository.UpdateAsync(coupon);
        await repository.SaveAsync();

        return Results.NoContent();
    }

    private async static Task<IResult> DeleteCoupon(ICouponRepository repository, int id)
    {
        var coupon = await repository.GetAsync(id);
        await repository.RemoveAsync(coupon);
        await repository.SaveAsync();
        return Results.NoContent();
    }
}
