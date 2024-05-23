using System.Windows;
using System.Windows.Controls;

namespace LibraryApp
{
    public class TabItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? CollectionState { get; set; }
        public DataTemplate? CollectionDefault { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is null)
                throw new System.NullReferenceException("Element is null. [SelectTemplate]");

            return SelectTemplateCore(item);
        }

        private DataTemplate? SelectTemplateCore(object item)
        {
            if (item is CollectionState)
            {
                return CollectionState;
            }
            else if (item is CollectionDefault)
            {
                return CollectionDefault;
            }
            return null;

            throw new System.NotImplementedException($"Uknown DataTemplate parameter. \nType: {nameof(TabItemDataTemplateSelector)}\nParametr: {item}");
        }
    }
}
