using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace BomTool.Converters
{
    public abstract class ValueConverterBase<T> : MarkupExtension, IValueConverter where T : class, new()
    {
        private static T _Instance = new T();

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _Instance ?? (_Instance = new T());
        }
    }

    public class StringIsNullOrWhiteSpace2Visibility : ValueConverterBase<StringIsNullOrWhiteSpace2Visibility>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return true;
            }

            return false;
        }
    }
}