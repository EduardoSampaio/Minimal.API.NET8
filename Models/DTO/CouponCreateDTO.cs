using System.ComponentModel.DataAnnotations;

namespace Minimal.API.NET8.Models.DTO   
{
    public class CouponCreateDTO
    {
        
        public string Name { get; set; }
        public int Percent { get; set; }
        public bool IsActive { get; set; }
    }
}
