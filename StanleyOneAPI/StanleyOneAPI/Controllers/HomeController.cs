using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StanleyOneAPI.Models;

namespace StanleyOneAPI.Controllers
{
    public class HomeController : ApiController
    {
        [Route("api/HomeController/ContactUs")]
        [HttpPost]
        public IHttpActionResult Email( ContactUsModel contact)
            {

            string status = "";


            try
                {
                status = contact.contactUs(contact);
                }
            catch (Exception ex)
                {
                string msg = ex.Message;
                }

            return Ok(status);
            }

        [Route("api/HomeController/CheckApi")]
        [HttpPost]
        public IHttpActionResult Check(ContactUsModel contact)
            {

            string status = "";
            try
                {
                status = "success1";
                }
            catch (Exception ex)
                {
                string msg = ex.Message;
                }

            return Ok(status);
            }
        }
}
