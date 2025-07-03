using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationApp.ViewModels.Hotel;
using VacationApp.ViewModels.Vacation;

namespace VacationApp.Services.Core.Interface
{
    public interface IHotelService
    {

        Task<bool> AddHotelModel(string Userid, AddHotel hotelmodel);

       
        Task<IEnumerable<TownModel>> TownViewDataAsync();

        Task<EditHotelModel> GetForEditHotel(int? id, string? Userid);
        Task<bool> EditHotel(string UserId, EditHotelModel edithotelmodel);

        //Task<bool> AddHotel(string Userid, AddHotel hotelmodel);

        //Task<bool> EditHotelModel(string Userid, AddReservation reservationmodel);


        //Task<bool> DeleteHotelModel(string Userid, AddReservation reservationmodel);
    }
}
