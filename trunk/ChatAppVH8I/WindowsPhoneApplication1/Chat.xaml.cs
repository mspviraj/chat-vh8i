using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace WindowsPhoneApplication1
{
    public partial class PageChat : PhoneApplicationPage
    {
        public PageChat()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DisplayMessage();
        }

        private void DisplayMessage()
        {
            // Create panel
            StackPanel messagePanel = new StackPanel();
            messagePanel.Background = new SolidColorBrush(Colors.Green);
            messagePanel.HorizontalAlignment = HorizontalAlignment.Left;

            // Create textblock
            TextBlock messsageText = new TextBlock();
            messsageText.Text = "nieuw blok";

            // Add textblock to panel
            messagePanel.Children.Add(messsageText);

            // Display message panel on screen
            Messages.Children.Add(messagePanel);
        }
    }
}