using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VacationAdd.Data.Models;

namespace VacationAdd.Data
{
    public class VacationAddDbContext: IdentityDbContext
    {
        public VacationAddDbContext(DbContextOptions<VacationAddDbContext> options)
         : base(options)
        {
        }


        public DbSet<Town> Towns { get; set; } = null!;
        public DbSet<VillaPenthhouse> VillaPenthHouse { get; set; } = null!;

        //public DbSet<Room> Rooms { get; set; } =  null!;

        public DbSet<Reservation> Reservations { get; set; } = null!;

        public DbSet<UserVilla> UsersVillas { get; set; } = null!;

        public DbSet<TypePlace> TypePlaces { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.Entity<VillaPenthhouse>(entity =>
            {

                entity.HasKey(h => h.IdVilla);

                entity
                .HasOne(e => e.Town)
                .WithMany(e => e.VillaPenthhouses)
                .HasForeignKey(e => e.TownId)
                .OnDelete(DeleteBehavior.Restrict);

                entity
             .HasOne(e => e.TypePlace)
             .WithMany(e => e.VillaPenthhouses)
             .HasForeignKey(e => e.IdPlace)
             .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne(h => h.Manager)
                .WithMany()
                .HasForeignKey(h => h.IDManager)
                .OnDelete(DeleteBehavior.Restrict);

              //  entity
              //.HasOne(e => e.Rooms)
              //.WithMany(e => e.Hotels)
              //.HasForeignKey(e => e.TownId)
              //.OnDelete(DeleteBehavior.Restrict);


            });

            builder.Entity<UserVilla>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.VillaId });

                entity
             .HasOne(e => e.VillaPenthhouse)
             .WithMany(e => e.UserVillas)
             .HasForeignKey(e => e.VillaId)
             .OnDelete(DeleteBehavior.Restrict);

                entity
             .HasOne(e => e.User)
             .WithMany()
             .HasForeignKey(e => e.UserId)
             .OnDelete(DeleteBehavior.Restrict);

            });

            //builder.Entity<HotelRooms>(entity =>
            //{

            //    entity.HasKey(hr=> new {hr.RoomID,hr.HotelID});

            //    entity.HasOne(hr => hr.Hotel)
            //    .WithMany(hr => hr.hotelRooms)
            //    .HasForeignKey(hr => hr.HotelID);

            //  //  entity.HasOne(hr => hr.Room)
            //  //.WithMany(hr => hr.hotelRooms)
            //  //.HasForeignKey(hr => hr.HotelID);

            //});

            builder.Entity<Reservation>(entityres =>
            {

                entityres.HasKey(r => r.IdReservation);

                entityres.HasOne(g => g.Guest)
                        .WithMany()
                        .HasForeignKey(g => g.GuestId);

                //entityres.HasOne(r => r.Room)
                //      .WithMany(r => r.Reservations)
                //      .HasForeignKey(r => r.RoomId);

                entityres.HasOne(h => h.VillaPenthhouse)
                      .WithMany(h => h.Reservations)
                      .HasForeignKey(h => h.VillaId);


            });

            var defaultUser = new IdentityUser
            {
                Id = "7699db7d-964f-4782-8209-d76562e0fece",
                UserName = "admin@horizons.com",
                NormalizedUserName = "ADMIN@HORIZONS.COM",
                Email = "admin@horizons.com",
                NormalizedEmail = "ADMIN@HORIZONS.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(
           new IdentityUser { UserName = "admin@horizons.com" },
           "Admin123!")
            };
            builder.Entity<IdentityUser>().HasData(defaultUser);


            //builder.Entity<Vacation>().HasData(
            //   new Vacation {Title = "Bodrum", ImgUrl = "https://content.tui.co.uk/adamtui/2023_8/11_9/8731a3c8-4b27-4fc8-9dfb-b05b00a3b5c2/ACC_040057_TUR_01WebOriginalCompressed.jpg?i10c=img.resize(width:1080);img.resize(height:608);img.crop(width:1080%2Cheight:608)", Info = "Phasellus quam turpis, feugiat sit amet in, hendrerit in lectus. ", Details = "", StartDate = null, EnddDate = null },
            //   new Vacation {  Title = "Corfu",ImgUrl= "https://content.tui.co.uk/adamtui/2019_12/17_11/61d6945a-4392-4994-9d5c-ab2600be5197/LOC_000450_TUR_GUM_09_GUMBET_BEACHWebOriginalCompressed.jpg?i10c=img.resize(width:1080);img.crop(width:1080%2Cheight:608)", Info= "Phasellus quam turpis, feugiat sit amet in, hendrerit in lectus. ", Details = "", StartDate = null, EnddDate = null },
            //   new Vacation {  Title = "Greece", ImgUrl = "https://content.tui.co.uk/adamtui/2023_8/11_9/aacea1c8-bf73-4207-8f31-b05b00a3c879/ACC_040057_TUR_08WebOriginalCompressed.jpg?i10c=img.resize(width:488);img.crop(width:488%2Cheight:274)", Info = "Phasellus quam turpis, feugiat sit amet in, hendrerit in lectus. ", Details = "", StartDate = null, EnddDate = null }
            //   );
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
