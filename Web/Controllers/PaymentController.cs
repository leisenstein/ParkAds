using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using DataTransferObjects;

namespace Web.Controllers
{
    [Route("payment")]
    public class PaymentController : Controller
    {
        private SessionController sessionController;

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Pay(string booking)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:1914/");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string json = JsonConvert.SerializeObject(BuildPaymentObject(booking));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("api/reservation", content);
            string jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

            TempData["Reservation"] = jsonResponse;

            PaymentDTO paymentDTO = JsonConvert.DeserializeObject<PaymentDTO>(jsonResponse);

            return (RedirectToAction("Payment", "Payment", new { id = paymentDTO.Id }));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Payment(int id)
        {
            PaymentDTO paymentDTO = null;
            if (TempData["Reservation"] == null)
                paymentDTO = GetReservation(id).Result;
            else
                paymentDTO = JsonConvert.DeserializeObject<PaymentDTO>(TempData["Reservation"].ToString());

            return View(BuildViewModel(paymentDTO));
        }

        private async Task<PaymentDTO> GetReservation(int bookingId)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:1914/");
            httpClient.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/reservation/id/payment" + bookingId).ConfigureAwait(false);

            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            PaymentDTO paymentDTO = JsonConvert.DeserializeObject<PaymentDTO>(response);

            return paymentDTO;
        }

        private PaymentDTO BuildPaymentObject(string booking)
        {
            BookingDTO bookingDTO = JsonConvert.DeserializeObject<BookingDTO>(Decode(booking));

            PaymentDTO paymentDTO = new PaymentDTO
            {
                BookingDTO = bookingDTO,
                DateTime = DateTime.Now,
                Price = 20
            };

            return paymentDTO;
        }

        private PaymentViewModel BuildViewModel(PaymentDTO paymentDTO)
        {
            PaymentViewModel paymentViewModel = new PaymentViewModel
            {
                Id = paymentDTO.Id,
                DateTime = paymentDTO.DateTime,
                Price = paymentDTO.Price,
                BookingDTO = paymentDTO.BookingDTO
            };

            return paymentViewModel;
        }

        private string Decode(string input)
        {
            var toBytes = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(toBytes);
        }
    }
}