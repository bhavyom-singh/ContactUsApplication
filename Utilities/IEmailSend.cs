using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ContactUsApplication.Utilities
{
    public interface IEmailSend
    {
        public Task<bool> SendEmail(string name, string enquirersEmail, string? notes, IFormFile? file);
    }
}
