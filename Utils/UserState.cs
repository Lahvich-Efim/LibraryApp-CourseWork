using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Utils
{
    public class UserState : PropertyChangedNotification
    {
        public int UserId
        {
            get { return GetValue(() => UserId); }
            set { SetValue(() => UserId, value); }
        }
        public string Username
        {
            get { return GetValue(() => Username); }
            set { SetValue(() => Username, value); }
        }
        public string? Pavatar
        {
            get { return GetValue(() => Pavatar); }
            set { SetValue(() => Pavatar, value); }
        }
    }
}
