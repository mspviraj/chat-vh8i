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
using Microsoft.Phone.Tasks;

namespace WindowsPhoneApplication1
{
    public partial class MainPage : PhoneApplicationPage
    {
        private PhoneNumberChooserTask phoneNrChooserTask;

        public MainPage()
        {
            InitializeComponent();
            phoneNrChooserTask = new PhoneNumberChooserTask();
            phoneNrChooserTask.Completed += new EventHandler<PhoneNumberResult>(phoneNrChooserTask_Completed);
        }

        private void btnConversations_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to active conversation page
            NavigationService.Navigate(new Uri("/Conversations.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnContacts_Click(object sender, RoutedEventArgs e)
        {
            // Show phone number chooser
            phoneNrChooserTask.Show();
        }

        private void phoneNrChooserTask_Completed(object sender, PhoneNumberResult e)
        {
            System.Diagnostics.Debug.WriteLine(e.PhoneNumber);
        }
    }
}