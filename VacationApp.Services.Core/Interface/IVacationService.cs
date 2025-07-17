using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationApp.ViewModels.Hotel;
using VacationApp.ViewModels.Vacation;

namespace VacationApp.Services.Core.Interface
{
    public interface IVacationService
    {

        Task<IEnumerable<AllVillasIndexViewModel>> GetAllHotelsAsync(string? UserId);


        Task<DetailsHotelIndexViewModel> GetHotelDetailsAsync(int? id, string? UserId);


        Task<IEnumerable<PlaceViewModel>> RoomViewDataAsync();

        Task<bool> AddReservationModel(string Userid, AddReservation reservationmodel);

        Task<IEnumerable<AllReservationsViewModel>> GetAllReservations(string? Userid);


        Task<EditReservation> GetForEditReservation(int? id, string? Userid);
        Task<bool> EditReservation(string UserId, EditReservation editreservation);

        Task<DeleteReservationIndexModel> GetForDeleteReservation(int? id, string? Userid);
       

        Task<bool> DeleteReservation(string Userid, int? id);
        Task<bool> RemoveFavorite(string Userid, int? id);
        
        Task<IEnumerable<FavoriteHotelIndexViewModel>> GetFavoriteHotels(string? Userid);


        Task<bool> FavoriteHotels(string Userid, int favoritehotelid);
    }
}
