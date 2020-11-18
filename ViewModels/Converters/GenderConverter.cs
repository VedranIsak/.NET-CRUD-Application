using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CRUD_Application.ViewModels.Converters
{
    [ValueConversion(typeof(string), typeof(SolidColorBrush))]
    class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string gender = (string)value;

            if (gender.Equals("Male", StringComparison.CurrentCultureIgnoreCase))
                return new SolidColorBrush(Colors.DarkBlue);
            else if (gender.Equals("Female", StringComparison.CurrentCultureIgnoreCase))
                return new SolidColorBrush(Colors.Purple);
            else
                return new SolidColorBrush(Colors.LightGray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
