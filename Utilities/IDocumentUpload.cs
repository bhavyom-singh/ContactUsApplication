using Microsoft.AspNetCore.Http;

namespace ContactUsApplication.Utilities
{
    public interface IDocumentUpload
    {
        public string UploadDocument(IFormFile file);
    }
}
