using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using DataTransferObjects;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Web.Models;

namespace Web.Controllers
{
    [Route("booking")]
    public class BookingController : Controller
    {
        private SessionController sessionController;

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Book(string spot)
        {
            ModelState.Clear();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:1914/");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string json = JsonConvert.SerializeObject(BuildBookingObject(spot));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("api/reservation", content);

            string jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            TempData["Reservation"] = jsonResponse;
            BookingDTO bookingDTO = JsonConvert.DeserializeObject<BookingDTO>(jsonResponse);

            return (RedirectToAction("Booking", "Booking", new { id = bookingDTO.Id }));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Booking(int id)
        {
            BookingDTO bookingDTO = null;
            if (TempData["Reservation"] == null)
                bookingDTO = GetReservation(id).Result;
            else
                bookingDTO = JsonConvert.DeserializeObject<BookingDTO>(TempData["Reservation"].ToString());

            return View(BuildViewModel(bookingDTO));
        }

        private async Task<BookingDTO> GetReservation(int bookingId)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:1914/");
            httpClient.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/reservation/id/booking" + bookingId).ConfigureAwait(false);

            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            BookingDTO bookingDTO = JsonConvert.DeserializeObject<BookingDTO>(response);

            return bookingDTO;
        }

        private BookingViewModel BuildViewModel(BookingDTO bookingDTO)
        {
            BookingViewModel bookingViewModel = new BookingViewModel
            {
                Id = bookingDTO.Id,
                UserDTO = bookingDTO.UserDTO,
                SpotDTO = bookingDTO.SpotDTO,
                DateTime = bookingDTO.DateTime,
                IsPayed = bookingDTO.IsPayed
            };

            return bookingViewModel;
        }

        private BookingDTO BuildBookingObject(string spot)
        {
            sessionController = new SessionController(HttpContext);

            BookingDTO bookingDTO = new BookingDTO
            {
                Id = 0,
                DateTime = DateTime.Now,
                IsPayed = false,
                UserDTO = sessionController.GetUser(),
                SpotDTO = JsonConvert.DeserializeObject<SpotDTO>(Decode(spot))
            };

            return bookingDTO;
        }

        private string Decode(string input)
        {
            var toBytes = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(toBytes);
        }
    }
}