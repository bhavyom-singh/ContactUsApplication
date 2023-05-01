using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ContactUsApplication.Models
{
    public class ContactUs
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }        
        public string Notes { get; set; }
        public IFormFile File { get; set; }
}
}
