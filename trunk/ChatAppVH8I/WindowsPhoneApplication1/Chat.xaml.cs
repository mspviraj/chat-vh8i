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
using System.Threading;

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
        private Timer messageTimer;

        int bla = 0;

        // TIJDELIJK
        private string myPhoneNr = "0031698765432";
        private string toPhoneNr = "0031629261646";

        public PageChat()
        {
            // Init GUI
            InitializeComponent();

            // Init webservice client
            webservice = new HelloWorldServiceSoapClient();
            webservice.get_messagesCompleted += new EventHandler<get_messagesCompletedEventArgs>(webservice_get_messagesCompleted);

            // Init the timer
            TimerCallback callback = HandleTimer;
            messageTimer = new Timer(callback, null, 0, 1000);
        }

        /// <summary>
        /// Handler for the timer,
        /// Call webservice to pull for messages.
        /// </summary>
        /// <param name="o"></param>
        private void HandleTimer(object o)
        {
            // Pull the server for messages
            webservice.get_messagesAsync(myPhoneNr);
        }

        /// <summary>
        /// On complete event handler for the get_messages request.
        /// Gets calles when get_messages is completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webservice_get_messagesCompleted(object sender, get_messagesCompletedEventArgs e)
        {
            /**
             * TODO:
             * - Ontvangen berichten moeten nog in de isolated storage komen te staan.
             */

            XElement xml = e.Result;
            IEnumerable<XElement> xmlMessages = xml.Descendants("message");
            List<Message> messages = new List<Message>();

            foreach (XElement xmlMsg in xmlMessages)
            {
                Message message = new Message();
                message.TelephoneNrTo = xmlMsg.Element("to").Value;
                message.TelephoneNrFrom = xmlMsg.Element("from").Value;
                //message.DateTime = DateTime.Parse(xmlMsg.Element("date").Value);
                message.Content = xmlMsg.Element("content").Value;
                messages.Add(message);
            }

            foreach (Message m in messages)
            {
                // Toon het bericht
                DisplayMessage(m);
            }

            /**
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
            */
        }

        /// <summary>
        /// Handler for the send message button.
        /// Gets calles when the user has typed a message and wants to send it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            // Create message
            Message message = new Message();
            message.TelephoneNrTo = toPhoneNr;
            message.TelephoneNrFrom = myPhoneNr;
            message.DateTime = DateTime.Now;
            message.Content = inpSendMessage.Text;

            // Send the message
            SendMessage(message);

            // Clear input field
            inpSendMessage.Text = "";
        }

        /// <summary>
        /// Send message to the server and display it in the chat.
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(Message message)
        {
            /**
             * TODO:
             * - Verzonden berichten opslaan in de isolated storage
             */

            // Send message to the server
            webservice.send_messageAsync(message.TelephoneNrTo,
                                        message.TelephoneNrFrom,
                                        message.DateTime.ToUniversalTime().ToString(),
                                        message.Content);

            // Display the message in the chat
            DisplayMessage(message);
        }

        /// <summary>
        /// Deze methode toont een bericht in de chat.
        /// </summary>
        /// <param name="message"></param>
        private void DisplayMessage(Message message)
        {
            // Create panel
            StackPanel messagePanel = new StackPanel();
            messagePanel.Margin = new Thickness(4);

            // Create textblock
            TextBlock messsageText = new TextBlock();
            messsageText.Margin = new Thickness(4);
            messsageText.Text = message.Content;

            // Add textblock to panel
            messagePanel.Children.Add(messsageText);

            // Is this message for me?
            if (message.TelephoneNrTo == myPhoneNr)
            {
                // YES: Display Green Left
                messagePanel.Background = new SolidColorBrush(Colors.Green);
                messagePanel.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                // NO: Display Yellow Right
                messagePanel.Background = new SolidColorBrush(Colors.Yellow);
                messagePanel.HorizontalAlignment = HorizontalAlignment.Right;
            }

            // Display message on screen
            Messages.Children.Add(messagePanel);
        }
    }
}