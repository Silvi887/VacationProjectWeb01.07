using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
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
        private readonly UserManager<IdentityUser?>? UserManager;

        public HotelsController(IHotelService hotelservice1, IVacationService vacservice1, UserManager<IdentityUser> userManager)
        {
            this.hotelService = hotelservice1;
            this.vacationService = vacservice1;
            this.UserManager = userManager;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? UserId = this.GetUserId();
            IEnumerable<AllHotelsIndexViewModel> allVacations = await this.vacationService.GetAllHotelsAsync(UserId);


            var user = await UserManager.FindByIdAsync(UserId);


            ViewBag.EmailConfirmed = user?.EmailConfirmed ?? false;


            return RedirectToAction(nameof(Index), "Vacation");
            // return View(allVacations);
        }


        [HttpGet]
        public async Task<IActionResult> AddHotel()
        {


            string? UserId = this.GetUserId();
            try
            {

                AddHotel addhotel = new AddHotel()
                {

                    HotelName = "",

                    //Stars { get; set; }

                    //NumberofRooms { get; set; }

                    ImageUrl = "",

                    //HotelInfo { get; set; } = null!;
                    ListTowns = (IEnumerable<TownModel>)await this.hotelService.TownViewDataAsync(),
                    IDManager = UserId
                };




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
                viewModelhotel.ListTowns = (IEnumerable<TownModel>)await this.hotelService.TownViewDataAsync();


                //if (!this.ModelState.IsValid)
                //{
                //    return this.RedirectToAction(nameof(Index));
                //}

                bool isvalid = await hotelService.AddHotelModel(UserId, viewModelhotel);

                if (isvalid == false)
                {

                    ModelState.AddModelError(string.Empty, "Fatal error accure while adding a hotel!");
                    return this.RedirectToAction(nameof(AddHotel));
                }

                viewModelhotel.ListTowns = (IEnumerable<TownModel>)await this.hotelService.TownViewDataAsync();
                ViewBag.SuccessMessage = "Successful addes hotel!";



                return RedirectToAction(nameof(Index), "Vacation");
                //return View("Views/Vacation/Index.cshtml", viewModelhotel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.RedirectToAction(nameof(Index));

            }
        }


        [HttpGet]
        public async Task<IActionResult> DeleteHotel(string id) //Delete
        {

            try
            {

                int id1 = int.Parse(id);
                string Userid = this.GetUserId();

                DeleteHotelIndexModel deleted =  await hotelService.GetForDeletedhotel(Userid, id1);

                if (deleted == null)
                {


                    return RedirectToAction("Details", "Vacation");
                }


                

                return View("Views/Vacation/DeleteHotel.cshtml", deleted);
                return RedirectToAction(nameof(Index), "Vacation");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.RedirectToAction("Details", "Vacation");

            }

        }


        [HttpPost]
        public async Task<IActionResult> DeleteHotel(DeleteHotelIndexModel deletedhotel)
        {

            try
            {

                var UserId = this.GetUserId();

                bool isdeleted = false;
                isdeleted = await hotelService.Deletedhotel(UserId, int.Parse(deletedhotel.Idhotel));

                if (isdeleted == true)
                {
                    return RedirectToAction(nameof(Index), "Vacation");
                }


                return this.RedirectToAction("Details", "Vacation");
            }

              
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.RedirectToAction("Details", "Vacation");

            }

            }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            int id1 = int.Parse(id);
            var UserId = this.GetUserId();

            try
            {
                EditHotelModel hotelMode =await hotelService.GetForEditHotel(id1, UserId);

                //hotelMode.ListTowns = (IEnumerable<TownModel>)await this.hotelService.TownViewDataAsync();
                if (!this.ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View("Views/Vacation/EditHotel.cshtml", hotelMode);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));

            }

            throw new Exception();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditHotelModel hotelnmodel)
        {


            var UserId = this.GetUserId();
            try
            {

                bool isvalidhotel= await hotelService.EditHotel(UserId, hotelnmodel);

               // hotelnmodel.ListTowns= (IEnumerable<TownModel>)await this.hotelService.TownViewDataAsync();

                if (!ModelState.IsValid)
                {
                    return View("Views/Vacation/EditHotel.cshtml", hotelnmodel);
                }


                ViewBag.SuccessMessage = "Successful update of hotel!";
                return RedirectToAction(nameof(Index), "Vacation");

                // return View("Views/Hotel/EditHotelModel.cshtml", hotelnmodel);
            }

            catch (Exception ex)
            {


                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));

            }

           
        }

       


    }
}
