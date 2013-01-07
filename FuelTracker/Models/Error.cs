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
using GalaSoft.MvvmLight;

namespace FuelTracker
{
    public class Error : ViewModelBase
    {
        public Error()
        {

        }

        public Error(string title, string message)
        {
            this.Title = title;
            this.Message = message;
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (_title == value)
                {
                    return;
                }
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                if (_message == value)
                {
                    return;
                }
                _message = value;
                RaisePropertyChanged("Message");
            }
        }
    }
}
