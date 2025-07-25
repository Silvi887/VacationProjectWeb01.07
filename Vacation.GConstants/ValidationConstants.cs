﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacation.GConstants
{
    public static class ValidationConstants
    {
        public const int TownMaxLenght = 80;
        public const int TownMinLenght = 3;

        public const int HotelMaxLenght = 100;
        public const int HotelMinLenght = 10;

        public const int HotelAdressMaxLenght = 3000;
        public const int HotelAdressMinLenght = 10;

        public const int RoomMaxLenght = 100;
        public const int RoomMinLenght = 10;
        public const int MaxLenghtGuest = 450;



        public const int GuestMaxLenght = 100;
        public const int GuestMinLenght = 10;



        public const int DescriptionMinLenght = 10;
        public const int DescriptionMaxLenght = 3000;

        public const string DateFormat = "dd-MM-yyyy";
    }
}
