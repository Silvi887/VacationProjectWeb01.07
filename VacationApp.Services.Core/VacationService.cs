
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Vacation.GConstants;
using VacationAdd.Data;
using VacationAdd.Data.Models;
using VacationApp.Services.Core.Interface;
using VacationApp.ViewModels.Hotel;
using VacationApp.ViewModels.Vacation;

namespace VacationApp.Services.Core
{
    public class VacationService : IVacationService
    {
        private readonly VacationAddDbContext Dbcontext;
        private readonly UserManager<IdentityUser> userManager;

        public VacationService(VacationAddDbContext pDbcontext, UserManager<IdentityUser> usermanager)
        {
            this.Dbcontext = pDbcontext;
            this.userManager = usermanager;
        }

        public async Task<bool> AddReservationModel(string Userid, AddReservation reservationmodel)
        {

            try
            {


                bool operationResult = false;
                IdentityUser? user1 = await this.userManager.FindByIdAsync(Userid);

                //string? UserId = this.GetUserId();
                int idroom = int.Parse(reservationmodel.RoomId);
                var Room = this.Dbcontext.Rooms.FindAsync(idroom);

                if (user1 != null)
                {
                    Reservation reservation1 = new Reservation()
                    {
                        StartDate = DateTime.ParseExact(reservationmodel.StartDate,"yyyy-MM-dd", CultureInfo.InvariantCulture ,DateTimeStyles.None),

                        EndDate = DateTime.ParseExact(reservationmodel.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                        AdultsCount = reservationmodel.AdultsCount,

                        ChildrenCount = reservationmodel.ChildrenCount,
                        GuestId = Userid,

                        //Guest { get; set; } = null!;

                        RoomId = int.Parse(reservationmodel.RoomId),
                        HotelId = int.Parse(reservationmodel.HotelId),
                        FirstName = reservationmodel.GuestFirstName,
                        LastName = reservationmodel.LastNameG,
                        DateOfBirth = DateTime.ParseExact(reservationmodel.DateofBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                        Address = reservationmodel.GuestAddress,
                        Email = reservationmodel.GuestEmail,
                        NumberOfPhone = reservationmodel.GuestPhoneNumber
                      
                    };

                    //Guest guest = new Guest()
                    //{
                    //    IdGuest= Userid,
                    //    FirstName = reservationmodel.GuestFirstName,
                    //    LastName = reservationmodel.LastNameG,
                    //    DateOfBirth = DateTime.ParseExact(reservationmodel.DateofBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    //    Address = reservationmodel.GuestAddress,
                    //    Email = reservationmodel.GuestEmail,
                    //    NumberOfPhone = reservationmodel.GuestPhoneNumber


                    //};

                    //UserReservation reservation = new UserReservation()
                    //{
                    //    Reservation= reservation1,
                    //    User=guest
                    //}


                    await this.Dbcontext.Reservations.AddAsync(reservation1);
                   // await this.Dbcontext.Guests.AddAsync(guest);

                    await this.Dbcontext.SaveChangesAsync();
                    operationResult = true;

                };
            



                return operationResult;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public  async Task<EditReservation> GetForEditReservation(int? id, string? Userid)
        {


           IdentityUser? currentUser =await userManager.FindByIdAsync(Userid);
            EditReservation reservation1 = null;

            if (currentUser != null)
            {

                Reservation? curentreservation = await Dbcontext.Reservations.Include(r => r.Hotel)
                                              .FirstOrDefaultAsync(r => r.IdReservation == id);

                reservation1 = new EditReservation()
                {

                    IdReservation = curentreservation.IdReservation.ToString(),
                    StartDate = curentreservation.StartDate.ToString("yyyy-MM-dd"),

                    EndDate = curentreservation.StartDate.ToString("yyyy-MM-dd"),
                    AdultsCount = curentreservation.AdultsCount,

                    ChildrenCount = curentreservation.ChildrenCount,

                    RoomId = curentreservation.RoomId.ToString(),
                    HotelId = curentreservation.HotelId.ToString(),
                    HotelName = curentreservation.Hotel.HotelName,
                    GuestFirstName = curentreservation.FirstName,
                    LastNameG = curentreservation.LastName,
                    DateofBirth = curentreservation.DateOfBirth.ToString("yyyy-MM-dd"),
                    GuestAddress = curentreservation.Address,
                    GuestEmail = curentreservation.Email,
                    GuestPhoneNumber = curentreservation.NumberOfPhone
                };

            }

           
            return reservation1;
                
        }


        public async Task<bool> EditReservation(string UserId, EditReservation editreservation)
        {


            IdentityUser userid = await userManager.FindByIdAsync(UserId);
            bool resultReservation = false;

            Reservation? CurrentReservation = await Dbcontext.Reservations
                                    .FindAsync(int.Parse(editreservation.IdReservation));

            Room? Room = await Dbcontext.Rooms.FindAsync(int.Parse(editreservation.RoomId));


            if (userid != null && CurrentReservation != null)
            {

                CurrentReservation.StartDate =
                    DateTime.ParseExact(editreservation.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                CurrentReservation.AdultsCount = editreservation.AdultsCount;
                CurrentReservation.ChildrenCount = editreservation.ChildrenCount;
                CurrentReservation.GuestId = userid.Id;
                CurrentReservation.RoomId = Room.IdRoom;
                CurrentReservation.HotelId = int.Parse(editreservation.HotelId);
                CurrentReservation.FirstName = editreservation.GuestFirstName;
                CurrentReservation.LastName = editreservation.LastNameG;
                CurrentReservation.DateOfBirth = DateTime.ParseExact(editreservation.DateofBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                CurrentReservation.Address = editreservation.GuestAddress;
                CurrentReservation.Email = editreservation.GuestEmail;
                CurrentReservation.NumberOfPhone = editreservation.GuestPhoneNumber;


                this.Dbcontext.SaveChanges();
                resultReservation = true;

            }


            return resultReservation;
            // throw new NotImplementedException();
        }

        public async Task<IEnumerable<AllHotelsIndexViewModel>> GetAllHotelsAsync(string? UserId)
        {
            IEnumerable<AllHotelsIndexViewModel> allhotels = await Dbcontext.Hotels
                .AsNoTracking()
                .Select
                (h => new AllHotelsIndexViewModel()
                {
                    IdHotel = h.IdHotel,
                    HotelName = h.HotelName,
                    Stars = h.Stars,
                    HotelInfo = h.HotelInfo,
                    NumberofRooms = h.NumberofRooms,
                    ImageUrl = h.ImageUrl
                }).ToListAsync();

            return allhotels;
            
          //  throw new NotImplementedException();
        }

        public async Task<IEnumerable<AllReservationsViewModel>> GetAllReservations(string? Userid)
        {


            IdentityUser? user = await userManager.FindByIdAsync(Userid);
            var reservations =await  Dbcontext.Reservations
                                .Include(r=> r.Hotel)
                                .AsNoTracking()
                                .Where( r =>r.IsDeleted == false)   /* r.GuestId == Userid &&*/
                                .Select(r =>new AllReservationsViewModel()
                                {
                                    IdReservation= r.IdReservation,
                                    HotelName=r.Hotel.HotelName,
                                    StartDate = r.StartDate.ToString(ValidationConstants.DateFormat),
                                    EndDate= r.EndDate.ToString(ValidationConstants.DateFormat),
                                    IsUserGuest= user!=null?  r.GuestId== user.Id:false,
                                }).ToListAsync();



            return reservations;

           //List<AllReservationsViewModel> all = new List<AllReservationsViewModel>();

           // foreach (var r in Allreservations)
           // {

           // }
           
        }

        public async Task<DetailsHotelIndexViewModel?> GetHotelDetailsAsync(int? id, string? UserId)
        {

            IdentityUser? currentUser = await userManager.FindByIdAsync(UserId);
            DetailsHotelIndexViewModel? hoteldetails = null;
            Hotel? CurrentDetailshotel = await Dbcontext.Hotels
                .Include(h=> h.Town)
                .Include(h=> h.Manager)
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.IdHotel == id.Value);


            if (CurrentDetailshotel !=null)
            {
                 hoteldetails = new DetailsHotelIndexViewModel()
                {
                    IdHotel= CurrentDetailshotel.IdHotel,
                    HotelName = CurrentDetailshotel.HotelName,
                    Stars = CurrentDetailshotel.Stars,
                    NumberofRooms = CurrentDetailshotel.NumberofRooms,
                    ImageUrl = CurrentDetailshotel.ImageUrl,
                    HotelInfo = CurrentDetailshotel.HotelInfo,
                    TownName = CurrentDetailshotel.Town.NameTown,
                    ManagerId=CurrentDetailshotel.IDManager,
                    IsManager= currentUser != null ? currentUser.Id == CurrentDetailshotel.IDManager : false,

                };

            }

            return hoteldetails;

            //throw new NotImplementedException();
        }

        //public async Task<IEnumerable<RoomViewModel>> RoomViewDataAsync()
        //{

        //    var allrooms = await Dbcontext.Rooms.AsNoTracking()
        //         .Select(r => new RoomViewModel()
        //         {
        //             Id = r.IdRoom,
        //             RoomType = r.NameRoom
        //         }
        //       ).ToListAsync();


        //    return allrooms;
        //  //  throw new NotImplementedException();
        //}

       public async Task<IEnumerable<RoomViewModel>> RoomViewDataAsync()
        {

            IEnumerable<RoomViewModel> allrooms = await Dbcontext.Rooms
             .AsNoTracking()
             .Select(r => new RoomViewModel()
             {
                 Id = r.IdRoom,
                 RoomType = r.NameRoom
             }
           ).ToListAsync();


            return allrooms;
        }

        public async Task<DeleteReservationIndexModel> GetForDeleteReservation(int? id, string? Userid)
        {


            IdentityUser? currentUser = await userManager.FindByIdAsync(Userid);
            DeleteReservationIndexModel? reservation1 = null;

            if (currentUser != null)
            {

                Reservation? curentreservation = await Dbcontext.Reservations.Include(r => r.Hotel)

                    .FirstOrDefaultAsync(r => r.IdReservation == id);

                reservation1 = new DeleteReservationIndexModel()
                {


                    StartDate = curentreservation.StartDate.ToString("yyyy-MM-dd"),

                    EndDate = curentreservation.StartDate.ToString("yyyy-MM-dd"),
                    IdReservation= curentreservation.IdReservation,
                    GuestFirstName= curentreservation.FirstName+" "+ curentreservation.LastName,
                    HotelName= curentreservation.Hotel.HotelName

                    //AdultsCount = curentreservation.AdultsCount,

                    //ChildrenCount = curentreservation.ChildrenCount,

                    //RoomId = curentreservation.RoomId.ToString(),
                    //HotelId = curentreservation.HotelId.ToString(),
                    //HotelName = curentreservation.Hotel.HotelName,
                    //GuestFirstName = curentreservation.FirstName,
                    //LastNameG = curentreservation.LastName,
                    //DateofBirth = curentreservation.DateOfBirth.ToString("yyyy-MM-dd"),
                    //GuestAddress = curentreservation.Address,
                    //GuestEmail = curentreservation.Email,
                    //GuestPhoneNumber = curentreservation.NumberOfPhone
                };

            }


            return reservation1;
        }



        public  async Task<bool> DeleteReservation(string Userid, int? id)
        {
            bool issuccessdelete = false;

            IdentityUser? curentuser =await userManager.FindByIdAsync(Userid);

            Reservation? currentReservation = await Dbcontext.Reservations.FindAsync(id);

            if(currentReservation != null && currentReservation != null)
            {


                currentReservation.IsDeleted = true;
                //Dbcontext.Reservations.Remove(currentReservation);
                Dbcontext.SaveChanges();

                issuccessdelete = true;
            }
            return issuccessdelete;
        }

        public async Task<IEnumerable<FavoriteHotelIndexViewModel>> GetFavoriteHotels(string? Userid)
        {

            bool issuccessfavorite = false;
            IdentityUser? curentuser = await userManager.FindByIdAsync(Userid);
            IEnumerable<FavoriteHotelIndexViewModel>? favoritehotel = null;


            if (curentuser != null)
            {
                favoritehotel =await Dbcontext.UserHotels
                    .Where(f => f.UserId.ToLower() == curentuser.Id.ToLower() && f.Hotel.IsDeleted==false)
                    .Include(f=> f.Hotel.Town)
                     .Select(h => new FavoriteHotelIndexViewModel()
                     {
                         IdHotel= h.HotelID,
                         TownName=h.Hotel.Town.NameTown,
                         ImageUrl= h.Hotel.ImageUrl,
                         HotelName=h.Hotel.HotelName
                     }).ToArrayAsync();

                issuccessfavorite=true;
            }

            return favoritehotel;




           // throw new NotImplementedException();
        }

        public async Task<bool> FavoriteHotels(string Userid, int idhotel)
        {

            bool issuccessfavorite = false;
            IdentityUser? curentuser = await userManager.FindByIdAsync(Userid);

            Hotel? currentHotel = await Dbcontext.Hotels.FindAsync(idhotel);

            if (curentuser != null && currentHotel != null) /* && currentHotel.IDManager != curentuser.Id */
            {
                UserHotel CurrentHotel = await Dbcontext.UserHotels.SingleOrDefaultAsync(h => h.HotelID == currentHotel.IdHotel && h.UserId== curentuser.Id);


                if (CurrentHotel == null)
                {

                    CurrentHotel = new UserHotel()
                    {
                        UserId= curentuser.Id,
                        HotelID=idhotel
                       // HotelID = idhotel,
                        //UserId = curentuser.Id
                    };
                 await Dbcontext.UserHotels.AddAsync(CurrentHotel);
                }
             
                await this.Dbcontext.SaveChangesAsync();

                issuccessfavorite = true;
            }
          


            return issuccessfavorite;
        }

        public async Task<bool> RemoveFavorite(string Userid, int? id)
        {
            bool isdeleted = false;
            IdentityUser? curentuser = await userManager.FindByIdAsync(Userid);
            Hotel? hoteltoremove = await Dbcontext.Hotels.SingleOrDefaultAsync(h => h.IdHotel == id);
            if(curentuser != null && hoteltoremove != null)
            {
                hoteltoremove.IsDeleted = true;


                await this.Dbcontext.SaveChangesAsync();

                isdeleted = true;
            }



            return isdeleted;
            //throw new NotImplementedException();
        }


        //public async Task<IEnumerable<AllVacationsIndexViewModel>> GetAllVacationaAsync()
        //   {

        //     IEnumerable<AllVacationsIndexViewModel> allMovies = await this.Dbcontext
        //       .Vacations
        //       .AsNoTracking()
        //       .Select(m => new AllVacationsIndexViewModel()
        //       {
        //           Id = m.Id.ToString(),                                         
        //           Title = m.Title,
        //           ImgUrl = m.ImgUrl,
        //           Info = m.Info,
        //           Details = m.Details,
        //           StartDate = m.StartDate.ToString("yyyy-MM-dd"),
        //           EnddDate = m.EnddDate.ToString("yyyy-MM-dd")

        //       })
        //       .ToListAsync();


        //     return allMovies;
        //   }
    }
}
