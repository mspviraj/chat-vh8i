using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Xml;
using System.Xml.XPath;

namespace WcfService1
{
    /// <summary>
    /// Summary description for HelloWorldService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class HelloWorldService : System.Web.Services.WebService
    {
        [WebMethod]
        public XmlDocument get_messages(string telephone)
        {
            // Create XML document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

            XmlNode xmlMessages = xmlDoc.CreateElement("messages");
            xmlDoc.AppendChild(xmlMessages);

            // Check database for new messages,
            // and add them to the messages element
            MessageAppDataContext dc = new MessageAppDataContext();
            var messages = from m in dc.Messages where m.TelephoneNrTo == telephone select m;

            foreach (Message message in messages)
            {
                // Create new <message>...</message> element
                // And add it to the <messages> element
                XmlNode xmlMessage = xmlDoc.CreateElement("message");

                XmlNode to = xmlDoc.CreateElement("to");
                to.AppendChild(xmlDoc.CreateTextNode(message.TelephoneNrTo));
                xmlMessage.AppendChild(to);

                XmlNode from = xmlDoc.CreateElement("from");
                from.AppendChild(xmlDoc.CreateTextNode(message.TelephoneNrFrom));
                xmlMessage.AppendChild(from);

                XmlNode date = xmlDoc.CreateElement("date");
                date.AppendChild(xmlDoc.CreateTextNode(message.Datetime.ToString()));
                xmlMessage.AppendChild(date);

                XmlNode content = xmlDoc.CreateElement("content");
                content.AppendChild(xmlDoc.CreateTextNode(message.Content));
                xmlMessage.AppendChild(content);

                xmlMessages.AppendChild(xmlMessage);

                // Mark message for deletion from database
                //dc.Messages.DeleteOnSubmit(message);
            }

            // Delete messages from database
            //dc.SubmitChanges();
            
            // Return XML to caller
            return xmlDoc;
        }

        [WebMethod]
        public XmlDocument send_message(string to, string from, string date, string content)
        {
            // Create new message object
            Message message = new Message();
            message.TelephoneNrTo = to;
            message.TelephoneNrFrom = from;
            message.Datetime = DateTime.Parse(date);
            message.Content = content;

            // Store message object in DB
            MessageAppDataContext dc = new MessageAppDataContext();
            dc.Messages.InsertOnSubmit(message);
            dc.SubmitChanges();

            // Create XML document for response
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

            XmlNode xmlResponsecode = xmlDoc.CreateElement("responsecode");
            xmlResponsecode.AppendChild(xmlDoc.CreateTextNode("200"));
            xmlDoc.AppendChild(xmlResponsecode);

            // Return XML response to caller
            return xmlDoc;
        }
    }
}
