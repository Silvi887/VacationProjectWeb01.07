using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Vacation.GConstants;
using VacationApp.Services.Core.Interface;
using VacationApp.ViewModels.Hotel;
using VacationApp.ViewModels.Vacation;

namespace BookVacation.Controllers
{
    public class HotelsController : BaseController
    {

        private readonly IHotelService hotelService;
        private readonly IVacationService vacationService;

        public HotelsController(IHotelService hotelservice1, IVacationService vacservice1)
        {
            this.hotelService = hotelservice1;
            this.vacationService = vacservice1;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? UserId = this.GetUserId();
            IEnumerable<AllHotelsIndexViewModel> allVacations = await this.vacationService.GetAllHotelsAsync(UserId);
            return View(allVacations);
        }


        [HttpGet]
        public  async Task<IActionResult> AddHotel(){

            try{

                AddHotel addhotel = new AddHotel()
                {

                    // HotelName { get; set; } = null!;

                    //Stars { get; set; }

                    //NumberofRooms { get; set; }

                    //ImageUrl { get; set; }

                    //HotelInfo { get; set; } = null!;

                    //IDManager { get; set; }
                };

                addhotel.ListTowns = (IEnumerable<TownModel>)await this.hotelService.TownViewDataAsync();


                return View("Views/Vacation/AddHotel.cshtml", addhotel);

            }
             catch (Exception ex)
            {
            Console.WriteLine(ex.Message);
            return this.RedirectToAction(nameof(Index));

                }

        }



        [HttpPost]
        public async Task<IActionResult> AddHotel(AddHotel viewModelhotel)
        {
            try
            {

                string? UserId = this.GetUserId();
                if (!this.ModelState.IsValid)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                bool isvalid= await hotelService.AddHotelModel(UserId, viewModelhotel);

                if(isvalid == false)
                {

                    ModelState.AddModelError(string.Empty, "Fatal error accure while adding a hotel!");
                    return this.RedirectToAction(nameof(AddHotel));
                }

                viewModelhotel.ListTowns = (IEnumerable<TownModel>)await this.hotelService.TownViewDataAsync();
                ViewBag.SuccessMessage = "Successful reservation!";
                return View("Views/Vacation/AddReservation.cshtml", viewModelhotel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.RedirectToAction(nameof(Index));

            }
        }


    }
}
