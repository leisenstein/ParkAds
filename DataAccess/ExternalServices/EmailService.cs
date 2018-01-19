using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace DataAccess.ExternalServices
{
    public static class EmailService
    {
        public static IRestResponse SendSimpleMessage(Ad ad, Object obj)
        {
            string base64imageString = ad.ImageData;
            //byte[] img = Convert.FromBase64String(base64imageString);
            var imgSrc = string.Format("data:image/gif;base64,{0}", base64imageString);

            if (obj.GetType().ToString().Equals("Domain.Payment"))
            {

                Payment pay = (Payment)obj;
                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator =
                new HttpBasicAuthenticator("api",
                                          "key-5a2d15a9d535a17b1aa6cd3be2190167");
                RestRequest request = new RestRequest();
                request.AddParameter("domain", "sandbox4efa56cba19f43c3b1096d2281b57d0c.mailgun.org", ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "Mailgun Sandbox <postmaster@sandbox4efa56cba19f43c3b1096d2281b57d0c.mailgun.org>");
                request.AddParameter("to", "Georgi <iwanmanew@yahoo.com>");
                request.AddParameter("subject", "Payment");
                request.AddParameter("text", "Congratulations Georgi, you just sent an email with Mailgun!  You are truly awesome!");
                request.AddParameter("html", "<h1>test<h1>");
                request.AddParameter("html", String.Format("Inline image here: <img src=\"data:image/png;base64,{0}\" />", base64imageString));

                 
                 
                request.Method = Method.POST;
                return client.Execute(request);
            }
            else
            {
                Booking book = (Booking)obj;
                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator =
                new HttpBasicAuthenticator("api",
                                          "key-5a2d15a9d535a17b1aa6cd3be2190167");
                RestRequest request = new RestRequest();
                request.AddParameter("domain", "sandbox4efa56cba19f43c3b1096d2281b57d0c.mailgun.org", ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "Mailgun Sandbox <postmaster@sandbox4efa56cba19f43c3b1096d2281b57d0c.mailgun.org>");
                request.AddParameter("to", "Georgi <gkaravasilev@gmail.com>");
                request.AddParameter("subject", "Hello Georgi");
                request.AddParameter("text", "Congratulations Georgi, you just sent an email with Mailgun!  You are truly awesome!");
                request.AddParameter("html", "<h1>Georgi<h1>");
                request.AddParameter("html","<img src=\"data:image/png;base64,"+ base64imageString +"/>");

                request.Method = Method.POST;
                return client.Execute(request);

            }

           
        }
    }
}
