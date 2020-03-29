using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace StanleyOneAPI.Models
    {
    public class ContactUsModel
        {
        public string userName { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string message { get; set; }

        public string contactUs(ContactUsModel contact)
            {

            string status="";

            try
                {
                contact = contact.makeEmail(contact);
                status = contact.sendEmail(contact);
                }
            catch (Exception ex)
                {
                string msg = ex.Message;
                }


            return status;
            }

        public ContactUsModel makeEmail(ContactUsModel contact)
            {
                       
            try
                {
                contact.message=  "Username: " + contact.userName + "<br/>" +
                                    "Email: " + contact.email + "<br/>" +
                                   "Message:" +contact.message+ "<br/>" +
                                    "Team Prabhat" + "<br/>" +
                                    "Expense Management";
            

                }
            catch (Exception ex)
                {
                string msg = ex.Message;
                }
            
            return contact;
            }

        public string sendEmail(ContactUsModel mailData)
            {

            string status = "success";

            try
                {
                string emailDisplayHeader = ConfigurationManager.AppSettings["emailDisplayHeader"];
                //string appname = ConfigurationManager.AppSettings["AppName"];
                string fromEmail = ConfigurationManager.AppSettings["fromEmail"];
                string toEmail = ConfigurationManager.AppSettings["toEmail"];
                string password = ConfigurationManager.AppSettings["password"];
                string host = ConfigurationManager.AppSettings["host"];

                MailMessage sendMail = new MailMessage();
                sendMail.From = new MailAddress(fromEmail);
                sendMail.Subject = mailData.subject;
                sendMail.Body = mailData.message;
                sendMail.IsBodyHtml = true;
                sendMail.To.Add(new MailAddress(toEmail).ToString());

                SmtpClient client = new SmtpClient();
                client.Host = host;
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(fromEmail, password);
                client.Send(sendMail);

                }

            catch (Exception ex)
                {
                string msg = ex.Message;
                }
            
            return status;
            }
        
        }
    }