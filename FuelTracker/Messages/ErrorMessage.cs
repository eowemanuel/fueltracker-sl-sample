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
using GalaSoft.MvvmLight.Messaging;

namespace FuelTracker.Messages
{
    public class ErrorMessage : MessageBase
    {
        public ErrorMessage(Error error)
        {
            Error = error;
        }

        public Error Error { get; set; }
    }
}
