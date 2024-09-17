using System;
using System.ComponentModel.DataAnnotations;

namespace ContactUsApplication.DTO
{
    public class ContactUs
    {
        [Key]
        public int ContactUsId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Notes { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
