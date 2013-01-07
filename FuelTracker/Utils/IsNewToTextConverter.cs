using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FuelTracker.ViewModels;
using System.Windows.Data;

namespace FuelTracker
{
    public class IsNewToTextConverter : IValueConverter
    {
        private const string NEW_MESSAGE = "Нов";
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is bool))
            {
                return string.Empty;
            }

            bool isNew = (bool)value;

            if (isNew)
            {
                return NEW_MESSAGE;
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

