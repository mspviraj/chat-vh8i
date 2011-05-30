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

namespace WindowsPhoneApplication1
{
    public class Message
    {
        private string telephoneNrTo;
        private string telephoneNrFrom;
        private DateTime dateTime;
        private string content;

        public string TelephoneNrTo { get; set; }
        public string TelephoneNrFrom { get; set; }
        public DateTime DateTime { get; set; }
        public string Content { get; set; }
    }
}
