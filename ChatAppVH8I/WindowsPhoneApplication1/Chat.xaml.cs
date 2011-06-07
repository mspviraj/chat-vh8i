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
using System.Xml;

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
        private int pullInterval = 5000;

        //
        private string conversationXmlPath;

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
            
            // Init conversation XML file
            conversationXmlPath = "conversations\\conversation_" + toPhoneNr + ".xml";
            InitConversationXmlFile();

            // Init the timer
            TimerCallback callback = HandleTimer;
            messageTimer = new Timer(callback, null, 0, pullInterval);
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
                // Toon het bericht op het scherm
                DisplayMessage(m);

                // Sla het bericht op in de XML
                StoreMessage(m);
            }
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
            StoreMessage(message);
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
            messsageText.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

            // Add textblock to panel
            messagePanel.Children.Add(messsageText);

            // Is this message for me?
            if (message.TelephoneNrTo == myPhoneNr)
            {
                // YES:
                messagePanel.Background = new SolidColorBrush(Colors.White);
                messagePanel.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                // NO:
                messagePanel.Background = new SolidColorBrush(Colors.Yellow);
                messagePanel.HorizontalAlignment = HorizontalAlignment.Right;
            }

            // Display message on screen
            Messages.Children.Add(messagePanel);
        }

        private void InitConversationXmlFile()
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();

            // TIJDELIJK
            if (store.FileExists(conversationXmlPath))
            {
                store.DeleteFile(conversationXmlPath);
            }

            // TIJDELIJK MOET ERGENS ANDERS STAAN
            if (!store.DirectoryExists("conversations"))
            {
                store.CreateDirectory("conversations");
            }


            if (!store.FileExists(conversationXmlPath))
            {
                // The is not yet a xml file for this conversation
                // So, create a new file
                IsolatedStorageFileStream isoStream = store.CreateFile(conversationXmlPath);

                // Create XML structure
                // and save it
                XDocument docje = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), null);
                docje.Add(new XElement("messages", ""));
                docje.Save(isoStream);

                // Clean up
                isoStream.Close();
            }

            // Clean up
            store.Dispose();
        }

        private XDocument GetCurrentXmlConversation()
        {
            XDocument xmlDoc = null;

            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isoStream = store.OpenFile(conversationXmlPath, FileMode.Open))
                {
                    isoStream.Position = 0;
                    xmlDoc = XDocument.Load(isoStream);
                }
            }

            return xmlDoc;
        }

        private void DeleteCurrentXmlConversation()
        {
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                store.DeleteFile(conversationXmlPath);
            }
        }

        private void SaveXmlConversation(XDocument xmlDox)
        {
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isoFileStream = store.OpenFile(conversationXmlPath, FileMode.Create))
                {
                    xmlDox.Save(isoFileStream);

                    // TIJDELIJK (DEBUG)
                    isoFileStream.Position = 0;
                    byte[] bla = new byte[isoFileStream.Length];
                    isoFileStream.Read(bla, 0, (int)isoFileStream.Length);
                    string ssss = System.Text.Encoding.UTF8.GetString(bla, 0, bla.Length);
                    System.Diagnostics.Debug.WriteLine(ssss);
                }
            }
        }

        public void StoreMessage(Message message)
        {
            // Get the current XML document
            XDocument xmlDoc = GetCurrentXmlConversation();
            DeleteCurrentXmlConversation();

            // Create new message XML element
            XElement xElMessage = new XElement("message");
            xElMessage.Add(new XElement("to", message.TelephoneNrTo));
            xElMessage.Add(new XElement("from", message.TelephoneNrFrom));
            xElMessage.Add(new XElement("date", message.DateTime.ToString()));
            xElMessage.Add(new XElement("content", message.Content));

            // Add element to XML document
            xmlDoc.Element("messages").Add(xElMessage);

            // Save XML document
            SaveXmlConversation(xmlDoc);
        }
    }
}