using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
    public class SpecificationState: PropertyChangedNotification
    {
        public string NameEn
        {
            get { return GetValue(() => NameEn); }
            set { SetValue(() => NameEn, value); }
        }

        public string NameRu
        {
            get { return GetValue(() => NameRu); }
            set { SetValue(() => NameRu, value); }
        }
        public int SpecId
        {
            get { return GetValue(() => SpecId); }
            set { SetValue(() => SpecId, value); }
        }
    }


    public class SpecificValueState : PropertyChangedNotification
    {
        public int SpecValueId
        {
            get { return GetValue(() => SpecValueId); }
            set { SetValue(() => SpecValueId, value); }
        }

        public string ValueStringRu
        {
            get { return GetValue(() => ValueStringRu); }
            set { SetValue(() => ValueStringRu, value); }
        }

        public string ValueStringEn
        {
            get { return GetValue(() => ValueStringEn); }
            set { SetValue(() => ValueStringEn, value); }
        }
        public string ValueType
        {
            get { return GetValue(() => ValueType); }
            set { SetValue(() => ValueType, value); }
        }
        public decimal ValueInt
        {
            get { return GetValue(() => ValueInt); }
            set { SetValue(() => ValueInt, value); }
        }

        public int SpecId
        {
            get { return GetValue(() => SpecId); }
            set { SetValue(() => SpecId, value); }
        }
    }
}
