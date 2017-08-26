using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace StudentsBase.ViewModels.Converters {


    [ValueConversion(typeof(int), typeof(string))]
    public class AgeToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int intEnd = ((int)value) % 10;
            string posfix = "";
            if (intEnd > 1) posfix = "а";
            return String.Format("{0} {1}", 
                                (int)value ,
                                ((intEnd > 0 && intEnd < 5 ) ? "год"+ posfix : "лет"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {

            int result=0;

            int.TryParse((string)value, out result);

            return result;
        }

    }
}
