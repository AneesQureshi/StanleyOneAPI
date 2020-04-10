using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using StanleyOneAPI.CommonHelper;

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
                                    "StanleyOne";
            

                }
            catch (Exception ex)
                {
                string msg = ex.Message;
                }
            
            return contact;
            }

        public string sendEmail(ContactUsModel mailData)
            {
            Log log = new Log();
            //for serialization of object
            //var json = new JavaScriptSerializer().Serialize(mailData);
            //JavaScriptSerializer jsserialize = new JavaScriptSerializer();
            //string output = jsserialize.Serialize(mailData);
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //string strout = serializer.Serialize(mailData);
           

            string status = "";
            string emailDisplayHeader = ConfigurationManager.AppSettings["emailDisplayHeader"];
            //string appname = ConfigurationManager.AppSettings["AppName"];
            string fromEmail = ConfigurationManager.AppSettings["fromEmail"];
            string toEmail = ConfigurationManager.AppSettings["toEmail"];
            string password = ConfigurationManager.AppSettings["password"];
            string host = ConfigurationManager.AppSettings["host"];

            try
                {
               

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
                status = "success";
                
                log.WriteErrorLog(fromEmail +" => "+ toEmail+ " => TO---------------Mail sent successfully---✔✔✔✔✔✔✔✔✔✔");
                }

            catch (Exception ex)
                {
                log.WriteErrorLog(fromEmail +" => "+ toEmail + "TO***************Mail not sent***✖✖✖✖✖✖✖✖✖✖");
                log.WriteExceptionLog(ex);
                }
            
            return status;
            }
        
        }
    }