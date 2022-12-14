using HandlebarsDotNet;
using MailKit;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using PostingManagement.Application.Contracts.Infrastructure;
using PostingManagement.Application.Models.Mail;
using PostingManagement.Domain.Entities;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;



namespace PostingManagement.Infrastructure.Mail
{
    public class SMTPEmailService : ISMTPEmailService
    {
        public SendOTPEmailSetting _emailSettings { get; }
        public SMTPEmailService(IOptions<SendOTPEmailSetting> mailSettings)
        {
            _emailSettings = mailSettings.Value;
        }
        public async Task<bool> SendOTPEmail(string userEmail, int otp, DateTime expiryTime)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailSettings.EmailUserName));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "OTP for Reset Password";

            var data = new
            {
                OTP = otp,
                ExpiryTime = expiryTime

            };
            var template = Handlebars.Compile(htmlTemplate());
            var result = template(data);
            email.Body = new TextPart(TextFormat.Html) { Text = result };
            // email.Attachments=
            var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.EmailHost, 587, SecureSocketOptions.StartTls);//host and port
            smtp.Authenticate(_emailSettings.EmailUserName, _emailSettings.EmailPassword);
            bool response = false;
            smtp.MessageSent += (object sender, MessageSentEventArgs e) => { response = true; };
            smtp.Send(email);
            smtp.Disconnect(true);
            return response;
        }
        public string htmlTemplate()
        {
            var html = "this OTP is for Reset the Password of the Posting Management Users Account , it is valide for 5 minutes only <br> <br> OTP : <b> {{OTP}} </b> <br> above Otp will be Valid till {{ExpiryTime}} ";
            return html;
        }
    }
}
