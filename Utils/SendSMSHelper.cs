
using LibraryApp.Model;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Messaging;

namespace LibraryApp
{
    public static class SendSMSHelper
    {
        private static Twilio.Types.PhoneNumber from;
        private static Twilio.Types.PhoneNumber to;
        private static string accaunt;
        private static string token;
        private static User User;
        static SendSMSHelper()
        {
            accaunt = Properties.Settings.Default.SMSAuth;
            token = Properties.Settings.Default.SMSToken;
            from = new Twilio.Types.PhoneNumber(Properties.Settings.Default.SMSNumber);
            TwilioClient.Init(accaunt, token);
        }
        public static void SetToUser(string s, User user)
        {
            to = new Twilio.Types.PhoneNumber(s);
            User = user;
        }

        public static void SendMessadeVerification(int code)
        {
            try
            {
                var message = MessageResource.Create(
                   body: $"Код подтверждение номера теленофа: {code}",
                   from: from,
                   to: to
                   );
            }
            catch (Exception ex)
            { 
                throw new Exception("Send error: " + ex.ToString());
            }
        }

        public static void SendMessadeOrderProduct(OrderState order)
        {
            string Name = string.IsNullOrEmpty(User.FirstName) ? User.Username : User.FirstName;
            var message = MessageResource.Create(
               body: $"Здравствуйте {Name}," +
               $"\n Вы успешно оформили заказ №{order.Id}. Более подробную информацию вы можете узнать в список уведомлений и в истории заказов." +
               $"\n С уважением, LibraryApp",
               from: from,
               to: to
               );
        }

        public static void SendMessadeComplitOrder(OrderState order)
        {
            string Name = string.IsNullOrEmpty(User.FirstName) ? User.Username : User.FirstName;
            var message = MessageResource.Create(
            body: $"Здравствуйте {Name}," +
               $"Ваш заказ №{order.Id} собран и готов к выдаче. Пожалуйста, посетите нашу библиотеку для получения книги." +
               $"\nС уважением, LibraryApp,",
               from: from,
               to: to
               );
        }

        public static void SendMessadeCanceledOrder(OrderState order)
        {
            string Name = string.IsNullOrEmpty(User.FirstName) ? User.Username : User.FirstName;
            var message = MessageResource.Create(
            body: $"Здравствуйте {Name}," + 
               $"Ваш заказ {order.Id} оформлен, но к сожалению, не может быть выполнен по не зависящим от нас причинам. " +
               $"Пожалуйста, обратитесь к нашему персоналу для уточнения информации. Спасибо за понимание." +
               $"\nС уважением, LibraryApp,",
               from: from,
               to: to
               );
        }

    }
}
 