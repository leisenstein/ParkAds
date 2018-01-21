using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using DataTransferObjects;
using Newtonsoft.Json;

namespace DataAccess.ExternalServices
{
    public static class EmailService
    {
        public static IRestResponse SendSimpleMessage(EmailTransferObject emailTransferObject)
        {
            string base64imageString = emailTransferObject.Ad.ImageData;
            var imgSrc = string.Format("data:image/gif;base64,{0}", base64imageString);

            RestClient client;
            RestRequest request;
            PrepareEmailRequest(base64imageString, out client, out request);

            if (emailTransferObject.Reservation.ToString().Contains("Price"))
            {
                Payment pay = JsonConvert.DeserializeObject<Payment>(emailTransferObject.Reservation.ToString());
                request.AddParameter("subject", "Payment for spot");
                request.AddParameter("html", "<h3>Spot name:" + pay.Booking.Spot.name + "<h3>");
                request.AddParameter("html", "<p>Payment date:" + pay.DateTime.ToString() + "<p>");
                request.AddParameter("html", "<p>Payment price:" + pay.Price.ToString() + "<p>");
            }
            else
            {
                Booking book = JsonConvert.DeserializeObject<Booking>(emailTransferObject.Reservation.ToString());
                request.AddParameter("subject", "Booking for a spot");
                request.AddParameter("html", "<h3>Spot name:" + book.Spot.name + "<h3>");
                request.AddParameter("html", "<p>Booking date:" + book.DateTime.ToString() + "<p>"); 
            }


            request.Method = Method.POST;
            return client.Execute(request);
        }

        private static void PrepareEmailRequest(string base64imageString, out RestClient client, out RestRequest request)
        {
            client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
            new HttpBasicAuthenticator("api",
                                      "key-5a2d15a9d535a17b1aa6cd3be2190167");
            request = new RestRequest();
            request.AddParameter("domain", "sandbox4efa56cba19f43c3b1096d2281b57d0c.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Mailgun Sandbox <postmaster@sandbox4efa56cba19f43c3b1096d2281b57d0c.mailgun.org>");
            request.AddParameter("to", "Ivan <gkaravasilev@gmail.com>");
            request.AddParameter("text", "Congratulations, you just sent an email with Mailgun!  You are truly awesome!");
            request.AddParameter("html", "<h1>ParkAds<h1>");
            request.AddParameter("html", String.Format("<img src=\"data:image/png;base64,{0}\" />", base64imageString));
        }
    }
}
