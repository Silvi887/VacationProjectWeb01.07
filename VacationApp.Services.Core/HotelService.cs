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
                        IDManager = int.Parse(user1.Id)

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

        public Task<bool> EditHotel(string UserId, EditHotelModel edithotelmodel)
        {
            throw new NotImplementedException();
        }

        public async Task<EditReservation> GetForEditHotel( string? Userid, EditHotelModel hotelmodel)
        {


            IdentityUser? currentUser = await UserManager.FindByIdAsync(Userid);

            EditHotelModel edithotelmodel = null;
            if(currentUser != null)
            {
                Hotel? currenthotel = await dbcontext.Hotels.AsNoTracking()
                                        .FirstOrDefaultAsync(h=> h.IdHotel== int.Parse(hotelmodel.idhotel));

                currenthotel.HotelName = hotelmodel.HotelName;
                currenthotel.Stars = hotelmodel.Stars;
                currenthotel.NumberofRooms = hotelmodel.NumberofRooms;
                currenthotel.IdHotel = int.Parse(hotelmodel.idhotel);
                    currenthotel.IDManager= int.Parse(currentUser.Id),

            }

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
