using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace StudentsBase.ViewModels.Converters {


    [ValueConversion(typeof(int), typeof(string))]
    public class GenderToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((int)value == 0) ? "м" : "ж";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return  ((string)value == "м") ? 0 : 1;
        }
    }
}
