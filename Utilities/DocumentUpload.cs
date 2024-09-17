using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using ContactUsApplication.Helpers;
using Microsoft.Extensions.Options;

namespace ContactUsApplication.Utilities
{
    public class DocumentUpload : IDocumentUpload
    {
        private Cloudinary _cloudinary;
        public DocumentUpload(IOptions<CloudinaryParams> cloudinaryConfig)
        {
            // Setting up cloud details for authentication
            /*
             * Caution : the credentials for cloud are for demonstration only, do not use it in production.
             */
            Account acc = new Account(
                cloudinaryConfig.Value.CloudName,
                cloudinaryConfig.Value.ApiKey,
                cloudinaryConfig.Value.ApiSecret
                );

            _cloudinary = new Cloudinary(acc);
        }

        public string UploadDocument(IFormFile file)
        {
            if (file.Length>0)
            {
                var uploadResult = new RawUploadResult();
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new RawUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                    };

                    // Uploading the file to cloud
                    uploadResult = _cloudinary.Upload(uploadParams, "auto");
                }
                var url = uploadResult.Url.ToString();
                return url;
            }
            return string.Empty;
        }
    }
}
