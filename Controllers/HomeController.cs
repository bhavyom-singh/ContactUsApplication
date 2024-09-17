using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContactUsApplication.Models;
using ContactUsApplication.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ContactUsApplication.Constants;
using ContactUsApplication.Utilities;
using ContactUsApplication.Repository;

namespace ContactUsApplication.Controllers
{
    public class HomeController : Controller
    {
        private IEmailSend _emailSend;
        private EmailConstants _emailConstants;
        private IDocumentUpload _documentUpload;
        private readonly ILogger<HomeController> _logger;        
        private readonly IOptions<OutlookParams> _outlookParams;
        private readonly IContactUsRepository _contactUsRepository;
        private readonly IOptions<CloudinaryParams> _cloudinaryConfig;

        public HomeController(ILogger<HomeController> logger, IOptions<OutlookParams> outlookParams, IOptions<CloudinaryParams> cloudinaryConfig, IContactUsRepository contactUsRepository,
            IEmailSend emailSend, IDocumentUpload documentUpload)
        {
            _logger = logger;
            _outlookParams = outlookParams;
            _cloudinaryConfig = cloudinaryConfig;
            _emailConstants = new EmailConstants();
            _emailSend = emailSend;
            _contactUsRepository = contactUsRepository;
            _documentUpload = documentUpload;                        
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult ContactUs()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(ContactUs contactUs)
        {
            string fileUrl = string.Empty;
            // Checking if the model is valid or not
            if (ModelState.IsValid)
            {
                // validation to check if the user has attached a file or not
                if(contactUs.File!=null && contactUs.File.Length>0)
                {
                    // uploading the file attached to cloud
                    fileUrl =_documentUpload.UploadDocument(contactUs.File);
                }
                
                // Creating model to save the user contact details
                DTO.ContactUs contact = new DTO.ContactUs()
                {
                    Email = contactUs.Email,
                    FileUrl = fileUrl,
                    Name = contactUs.Name,
                    Notes = contactUs.Notes
                };

                // Saving contact info to database
                _contactUsRepository.SaveContactUs(contact);

                // Sending email and updating page 
                Task<bool> task = _emailSend.SendEmail(contactUs.Name, contactUs.Email, contactUs.Notes, contactUs.File);
                if (task.Result)
                {
                    ViewBag.Message = _emailConstants.EmailSentSuccess;
                }
                else
                {
                    ViewBag.Message = _emailConstants.EmailSentFailure;
                }

                // Clearing the model
                ModelState.Clear();
            }
            
            return View();
        }
    }
}
