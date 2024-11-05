using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BomTool.Converters
{
    public sealed class ReverBooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Convert bool or Nullable bool to Visibility
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = false;
            if (value is bool)
            {
                bValue = (bool)value;
            }
            else if (value is bool?)
            {
                bool? tmp = (bool?)value;
                bValue = tmp.Value;
            }
            return (!bValue) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Convert Visibility to boolean
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility)
            {
                return (Visibility)value != Visibility.Visible;
            }
            else
            {
                return true;
            }
        }
    }
}