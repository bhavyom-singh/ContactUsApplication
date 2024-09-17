namespace ContactUsApplication.Constants
{
    public class EmailConstants
    {
        /* 
         * EmailFrom : Either create a new outlook account and replace this or use the same account
         * If you choose to change this email, set the password of that account in the appsettings.json as well.
         * Caution: This email id is just for demonstration only, do not use this in production.
         */
        public readonly string EmailFrom = "bhavyomwfs19@outlook.com";      
        public readonly string EmailTo = "bhavyom19@csu.fullerton.edu";     // Change this email to the email id you want to send email to
        public readonly string EmailSubject = "Someone wants to contact us.";
        public readonly string EmailSMTPServer = "smtp.office365.com";
        public readonly int EmailSMTPPort = 587;
        public readonly string EmailSentSuccess = "Mail Sent Successfully. A representative of our company will contact you on the email provided by you. Thank you for your interest in our company.";
        public readonly string EmailSentFailure = "We encountered an error while sending query. Please try again later. Thank you for connecting with us.";
        public string createEmailBodyContent(string name, string emailAddress, string notes) => $"Hi Rep, \nHope you are having an awesome day.\n{name} with email id : {emailAddress} wants to know the following :\n\"\n{notes}\n\"\n Thanks and Regards,\nContact Us Service";

    }
}
