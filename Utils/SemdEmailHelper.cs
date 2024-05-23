using System.Net.Mail;
using System.Net.Mime;

namespace LibraryApp
{
    public static class SendEmailHelper
    {
        private static MailMessage mailMessage;
        private static SmtpClient smtpClient;
        private static string password;
        static List<LinkedResource> images = new List<LinkedResource>();
        public enum MessageEmail
        {
            RegistrationCode
        }
        public static void InitMail(string To)
        {
            try
            {
                string From = Properties.Settings.Default.EmailSource;
                string Password = Properties.Settings.Default.EmailPwd;
                mailMessage = new MailMessage();
                mailMessage.To.Add(To);
                mailMessage.From = new System.Net.Mail.MailAddress(From);
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.Priority = System.Net.Mail.MailPriority.Normal;
                password = Password;
            }
            catch (Exception ex)
            {
                throw new Exception("Initialization email error!" + ex.Message);
            }
        }
        public static void Attachments(string Path)
        {
            try
            {
                string[] path = Path.Split(',');
                Attachment data;
                ContentDisposition disposition;
                for (int i = 0; i < path.Length; i++)
                {
                    data = new Attachment(path[i], MediaTypeNames.Application.Octet);// Instantiate attachment  
                    disposition = data.ContentDisposition;
                    mailMessage.Attachments.Add(data);// Add to the attachment  
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Add attachment error!" + ex.Message);
            }
        }
        public static void Send()
        {
            try
            {
                if (mailMessage != null)
                {
                    smtpClient = new SmtpClient();
                    smtpClient.Credentials = new System.Net.NetworkCredential(mailMessage.From.Address, password); // Set the bill of sender identity
                    smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;
                    smtpClient.Host = "smtp." + mailMessage.From.Host;
                    smtpClient.Port = 587;
                    smtpClient.Send(mailMessage);
                }
            }
            catch (SmtpException ex)
            {
                throw new SmtpException("Send Error!" + ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Send Error!" + ex.ToString());
            }
        }

        private static void GenerateHeader(string name)
        {
            LinkedResource picture = new LinkedResource(Settings.projectPath + "/image/Logo.png", "image/png");
            picture.ContentId = "logo";
            images.Add(picture);
            switch (Settings.Lang)
            {
                case Settings.Languages.RU:
                    mailMessage.Body += "<html lang=\"ru\">\n" +
                        $"<body>\n" +
                        $"<div width=\"200\" style=\"font-family: 'MS Sans Serif', Helvetica, sans-serif;\">\n" +
                        $"<img src=\"cid:logo\"/>\n" +
                        $"<h3>Здравствуйте, {name}!</h3>\n";

                    break;
                case Settings.Languages.EN:
                    mailMessage.Body += "<html lang=\"ru\">\n" +
                       $"<body>\n" +
                       $"<div style=\"font-family: 'MS Sans Serif', Helvetica, sans-serif;\">\n" +
                        $"<img src=\"cid:logo\"/>\n" +
                       $"<h3>Hello, {name}!</h3>\n";
                    break;
            }
        }
        public static void GenerateRegistrationCodeMessage(string name, int code)
        {
            GenerateHeader(name);
            switch (Settings.Lang)
            {
                case Settings.Languages.RU:
                    mailMessage.Subject = "Подтверждение почты для регистрации в LibraryApp.";
                    mailMessage.Body += $"<p>Код для подтверждения электронной почты:</p>\n" +
                        $"<p style=\"font-size: 16pt\"><strong>{code}<strong/></p>\n" +
                        $"<p style=\"color: black;\">Спасибо за регистрацию в приложении нашей библиотеки!</p>\n";
                    break;
                case Settings.Languages.EN:
                    mailMessage.Subject = "Email confirmation for registration in LibraryApp.";
                    mailMessage.Body += $"<p>Email verification code:</p>\n" +
                        $"<p style=\"font-size: 16pt\"><strong>{code}<strong/></p>" +
                       $"<p style=\"color: black;\">Thank you for registering in our library app!</p>\n";
                    break;
            }
            GenerateFooter();
        }
        public static void GenerateChangeEmailMessage(string name, int code)
        {
            GenerateHeader(name);
            switch (Settings.Lang)
            {
                case Settings.Languages.RU:
                    mailMessage.Body += $"<p>Код для подтверждения смены электронной почты:</p>\n" +
                        $"<p style=\"font-size: 16pt\"><strong>{code}<strong/></p>\n";
                    break;
                case Settings.Languages.EN:
                    mailMessage.Body += $"<p>Verification code for change email:</p>\n" +
                        $"<p style=\"font-size: 16pt\"><strong>{code}<strong/></p>";
                    break;
            }
            GenerateFooter();
        }
        public static void GenerateRecoverCodeMessage(string name, int code)
        {
            GenerateHeader(name);
            switch (Settings.Lang)
            {
                case Settings.Languages.RU:
                    mailMessage.Body += $"<p>Код для восстановления аккаунта:</p>\n" +
                        $"<p style=\"font-size: 16pt\"><strong>{code}<strong/></p>\n";
                    break;
                case Settings.Languages.EN:
                    mailMessage.Body += $"<p>Account recovery code:</p>\n" +
                        $"<p style=\"font-size: 16pt\"><strong>{code}<strong/></p>";
                    break;
            }
            GenerateFooter();
        }
        private static void GenerateFooter()
        {
            switch (Settings.Lang)
            {
                case Settings.Languages.RU:
                    mailMessage.Body += "<p style=\"color: gray\">Если вы не отправляли этот запрос, проигнорируйте данное письмо.</p>";
                    break;
                case Settings.Languages.EN:
                    mailMessage.Body += "<p style=\"color: gray\">If you did not make this request, please disregard this email.</p>";
                    break;
            }
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(mailMessage.Body, null, "text/html");
            mailMessage.AlternateViews.Add(alternateView);
            foreach (var img in images)
                alternateView.LinkedResources.Add(img);
            images.Clear();
        }
    }
}
