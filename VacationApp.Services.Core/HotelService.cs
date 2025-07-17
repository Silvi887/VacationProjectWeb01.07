using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Formats.Tar;
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
                        RoomCapacityCount = hotelmodel.NumberofRooms,
                        RoomBookedRooms = 0,
                        ImageUrl = hotelmodel.ImageUrl,
                        HotelInfo = hotelmodel.HotelInfo,
                        IDManager = user1.Id,
                        TownId= int.Parse(hotelmodel.TownId),
                        AddressHotel= hotelmodel.AddressHotel,
                        IdsRooms=""

                    };
                    this.dbcontext.Hotels.AddAsync(currenthotel);
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
                currenthotel.RoomCapacityCount = edithotelmodel.NumberofRooms;
                //currenthotel.IdHotel = int.Parse(edithotelmodel.Idhotel);
                currenthotel.IDManager = edithotelmodel.IDManager;


                isValidhotelop = true;
            }

            dbcontext.SaveChanges();


            return isValidhotelop;
        }

        public async Task<EditHotelModel> GetForEditHotel( int? id ,string? Userid)
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
                    NumberofRooms = currenthotel.RoomCapacityCount,
                    Idhotel = currenthotel.IdHotel.ToString(),
                    IDManager = currenthotel.IDManager,
                    ImageUrl= currenthotel.ImageUrl,
                    HotelInfo= currenthotel.HotelInfo
                   
                   

                };

            }

            return edithotelmodel;
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




        public async Task<DeleteHotelIndexModel> GetForDeletedhotel(string userId, int id)
        {
            IdentityUser? currentUser = await UserManager.FindByIdAsync(userId);


            Hotel? currenthotel=await dbcontext.Hotels.SingleOrDefaultAsync(h=> h.IdHotel==id);


            DeleteHotelIndexModel fordeletedhotel = new DeleteHotelIndexModel()
            {
                Idhotel = currenthotel.IdHotel.ToString(),
                HotelName = currenthotel.HotelName,
                NameManager = currenthotel.Manager.UserName
            };
            return fordeletedhotel;

        }
        public async Task<bool> Deletedhotel(string userId, int id)
        {


            IdentityUser? currentUser = await UserManager.FindByIdAsync(userId);
            bool isdeleted = false;

            if (currentUser != null)
            {
                var selectedhotel = dbcontext.Hotels.SingleOrDefault(h => h.IdHotel == id);

                selectedhotel.IsDeleted = true;

                isdeleted = true;

                this.dbcontext.SaveChanges();

             }

            return isdeleted;
        }

    }
}
