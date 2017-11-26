using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ExternalServices
{
    public static class EmailService
    {
        public static IRestResponse SendSimpleMessage()
        {
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
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}
