using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Vacation.GConstants;
using VacationAdd.Data;
using VacationAdd.Data.Models;
using VacationApp.Services.Core.Interface;
using VacationApp.ViewModels.Hotel;
using VacationApp.ViewModels.Vacation;

namespace VacationApp.Services.Core
{
    public class HotelService : IHotelService
    {

        private readonly VacationAddDbContext dbcontext;
        private readonly UserManager<IdentityUser> UserManager;

        public HotelService(VacationAddDbContext pDbcontext, UserManager<IdentityUser> usermanager)
        {
            this.dbcontext = pDbcontext;
            this.UserManager = usermanager;
        }

        //public  async  Task<bool> AddHotel(string Userid, AddHotel hotelmodel)
        //{

        //    try
        //    {

        //        bool operationResult = false;
        //        IdentityUser? user1 = await this.UserManager.FindByIdAsync(Userid);


        //        var currenthotel = dbcontext.Hotels.SingleOrDefaultAsync(h => h.IdHotel == int.Parse(hotelmodel.Idhotel));

        //    }


        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        throw;
        //    }
        //    throw new NotImplementedException();
        //}

        public async Task<bool> AddHotelModel(string Userid, AddHotel hotelmodel)
        {

            try
            {
                bool operationResult = false;
                IdentityUser? user1 = await this.UserManager.FindByIdAsync(Userid);


                if (user1 != null)
                {
                    Hotel currenthotel = new Hotel()
                    {

                        HotelName = hotelmodel.HotelName,
                        Stars = hotelmodel.Stars,
                        NumberofRooms = hotelmodel.NumberofRooms,
                        ImageUrl = hotelmodel.ImageUrl,
                        HotelInfo = hotelmodel.HotelInfo,
                        IDManager = user1.Id

                    };
                    this.dbcontext.AddAsync(currenthotel);
                    this.dbcontext.SaveChanges();

                    operationResult = true;
                }


                return operationResult;

            }
            
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
    //  throw new NotImplementedException();
}

        public async Task<bool> EditHotel(string UserId, EditHotelModel edithotelmodel)
        {

            bool isValidhotelop = false;
            IdentityUser? currentUser= await UserManager.FindByIdAsync(UserId);
            Hotel? currenthotel = await  dbcontext.Hotels
                                    .FirstOrDefaultAsync(h => h.IdHotel == int.Parse(edithotelmodel.Idhotel));

            if (currentUser != null)
            {

                currenthotel.HotelName = edithotelmodel.HotelName;
                currenthotel.Stars = edithotelmodel.Stars;
                currenthotel.NumberofRooms = edithotelmodel.NumberofRooms;
                //currenthotel.IdHotel = int.Parse(edithotelmodel.Idhotel);
                currenthotel.IDManager = edithotelmodel.IDManager;


                isValidhotelop = true;
            }

            dbcontext.SaveChanges();


            return isValidhotelop;
        }

        public async Task<EditHotelModel> GetForEditHotel( string? Userid, int? id)
        {


            IdentityUser? currentUser = await UserManager.FindByIdAsync(Userid);
            Hotel? currenthotel = await dbcontext.Hotels.Include(h=> h.Manager)
                                        .FirstOrDefaultAsync(h => h.IdHotel == id);

            EditHotelModel edithotelmodel = null;
            if(currentUser != null)
            {

                edithotelmodel = new EditHotelModel()
                {
                    HotelName = currenthotel.HotelName,
                    Stars = currenthotel.Stars,
                    NumberofRooms = currenthotel.NumberofRooms,
                    Idhotel = currenthotel.IdHotel.ToString(),
                    IDManager = currenthotel.IDManager


                };

            }

            return edithotelmodel;
        }

        public Task<EditHotelModel> GetForEditHotel(int? id, string? Userid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TownModel>> TownViewDataAsync()  //padashto menu gradove
        {

            IEnumerable<TownModel> alltowns = await  dbcontext.Towns
             .AsNoTracking()
             .Select(r => new TownModel()
             {
                 IdTown = r.IdTown,
                 NameTown = r.NameTown
             }
           ).ToListAsync();


            return alltowns;
        }

    }
}
