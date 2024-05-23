using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
    public enum OrderStatus
    {
        Waiting = 0,      // Ожидание - Заказ ожидает подтверждения и обработки
        Collected = 1,    // Собрано - Книги для заказа собраны и готовы к выдаче
        Received = 2,    // Получено - Заказ отправлен пользователю
        Completed = 3,    // Выполнено - Заказ успешно завершен
        Cancelled = 4     // Отказано - Заказ отклонен или отменен
    }
    public static class OrderStatusHelper
    {
        private static Dictionary<OrderStatus, string> StatusDescriptions;
        private static void SwitchLang()
        {
            switch (Settings.Lang)
            {
                case Settings.Languages.RU:
                    StatusDescriptions = new Dictionary<OrderStatus, string>()
                    {
                        { OrderStatus.Waiting, "Ожидание" },
                        { OrderStatus.Collected, "Собрано" },
                        { OrderStatus.Received, "Получено" },
                        { OrderStatus.Completed, "Выполнено" },
                        { OrderStatus.Cancelled, "Отказано" },
                    };
                    break;
                case Settings.Languages.EN:
                    StatusDescriptions = new Dictionary<OrderStatus, string>()
                {
                        { OrderStatus.Waiting, "Waiting" },
                        { OrderStatus.Collected, "Collected" },
                        { OrderStatus.Received, "Received" },
                        { OrderStatus.Completed, "Completed" },
                        { OrderStatus.Cancelled, "Cancelled" },
                };
                    break;
            }
        }
        static  OrderStatusHelper()
        {
            SwitchLang();
            Settings.changeLang += SwitchLang;
        }
        public static string GetStatusDescription(OrderStatus status)
        {
            return StatusDescriptions.TryGetValue(status, out var description) ? description : "Неизвестный статус";
        }
    }
}

