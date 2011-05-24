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
using System.Xml.Linq;
using System.IO.IsolatedStorage;
using System.IO;
using WindowsPhoneApplication1.ChatAppWebservice;

namespace WindowsPhoneApplication1
{
    /**
     * TODO:
     * Deze pagina moet nog "weten" met wie er gesproken wordt.
     * Dit is nodig om de webservice juist aan te kunnen roepen.
     */
    public partial class PageChat : PhoneApplicationPage
    {
        private HelloWorldServiceSoapClient webservice;

        public PageChat()
        {
            // Init GUI
            InitializeComponent();

            // Init webservice client
            webservice = new HelloWorldServiceSoapClient();
            webservice.get_messagesCompleted += new EventHandler<get_messagesCompletedEventArgs>(webservice_get_messagesCompleted);

            /**
             * TODO
             * In de bovenste 2 regels wordt de webservice client aangemaakt
             * en een handler gekoppeld die wordt aangeroepen wanneer hij berichten heeft opgehaald
             * 
             */
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

        private void webservice_get_messagesCompleted(object sender, get_messagesCompletedEventArgs e)
        {
            /**
             * TODO:
             * Deze handler moet de ontvangen berichten van de webservice afhandelen.
             * - Het bericht moet op het scherm worden getoond
             * - Het bericht moet in de isolated storage worden opgeslagen
             */


            // Dit is allemaal test code,
            // ik was bezig om te kijken hoe je met de isolated storage XML kan opslaan.
            XElement xml = e.Result;

            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            if (!store.DirectoryExists("Conversations"))
            {
                store.CreateDirectory("Conversations");
            }

            IsolatedStorageFileStream isoStream = store.OpenFile(@"Conversations\0031698765432.xml", FileMode.OpenOrCreate);

            // Save to file through the stream
            xml.Save(isoStream);

            // Clean up
            store.Dispose();
            isoStream.Dispose();


            //IEnumerable<XElement> messages = from p in xml.Descendants("message") select p;

            //foreach (var message in messages)
            //{
                
            //}
        }
    }
}