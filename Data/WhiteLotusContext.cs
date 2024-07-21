using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WhiteLotus.Main.Service.Entities;

namespace WhiteLotus.Main.Service.Data;

public class WhiteLotusContext(DbContextOptions<WhiteLotusContext> options) : DbContext(options)
{
    public DbSet<Status> Statuses => Set<Status>();
    public DbSet<Staff> Staffs => Set<Staff>();
    public DbSet<RoomType> RoomTypes => Set<RoomType>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<Floor> Floors => Set<Floor>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        // Seed data

        modelBuilder.Entity<Status>().HasData(
            new Status { StatusId = 1, Name = "Active" },
            new Status { StatusId = 2, Name = "Inactive" },
            new Status { StatusId = 3, Name = "Pending" });


        modelBuilder.Entity<Floor>().HasData(
        new Floor { FloorId = 1, Name = "Ground Floor" },
        new Floor { FloorId = 2, Name = "First Floor" },
        new Floor { FloorId = 3, Name = "Second Floor" },
        new Floor { FloorId = 4, Name = "Third Floor" },
        new Floor { FloorId = 5, Name = "Fourth Floor" });

        modelBuilder.Entity<RoomType>().HasData(
                new RoomType { RoomTypeId = 1, Name = "Single",Price = 100.00M },
                new RoomType { RoomTypeId = 2, Name = "Double" ,Price=180.00M },
                new RoomType { RoomTypeId = 3, Name = "Triple",Price=250.00M },
                new RoomType { RoomTypeId = 4, Name = "Quadruple",Price=370.00M },
                new RoomType { RoomTypeId = 5, Name = "Queen",Price=450.00M },
                new RoomType { RoomTypeId = 6, Name = "King",Price=550.00M });
        ;

            modelBuilder.Entity<Room>().HasData(
                new Room { RoomId = 1,Number=1,  FloorId = 3, RoomTypeId = 5 ,StatusId = 1 },
                new Room { RoomId = 2, Number=2, FloorId = 1, RoomTypeId = 1 ,StatusId = 1 },
                new Room { RoomId = 3, Number=3, FloorId = 2, RoomTypeId = 2 ,StatusId=3},
                new Room { RoomId = 4, Number=4, FloorId = 2, RoomTypeId = 4 ,StatusId=2},
                new Room { RoomId = 5,Number=5,FloorId = 1, RoomTypeId = 2,StatusId=3 },
                new Room { RoomId = 6, Number=6, FloorId = 3, RoomTypeId = 1,StatusId=1 },
                new Room { RoomId = 7,Number=7,  FloorId = 1, RoomTypeId = 2 ,StatusId=2},
                new Room { RoomId = 8,Number=8,  FloorId = 2, RoomTypeId = 6,StatusId=1 },
                new Room { RoomId = 9, Number=9, FloorId = 2, RoomTypeId = 5,StatusId=1 },
                new Room { RoomId = 10,Number=10,  FloorId = 5, RoomTypeId = 4,StatusId=1 },
                new Room { RoomId = 11,Number=11,  FloorId = 1, RoomTypeId = 6,StatusId=1 },
                new Room { RoomId = 12,Number=12,  FloorId = 3, RoomTypeId = 1,StatusId=1 },
                new Room { RoomId = 13,Number=13,  FloorId = 1, RoomTypeId = 6,StatusId=1 },
                new Room { RoomId = 14,Number=14,  FloorId = 5, RoomTypeId = 2,StatusId=1 },
                new Room { RoomId = 15,Number=15,  FloorId = 2, RoomTypeId = 5,StatusId=1 },
                new Room { RoomId = 16,Number=16,  FloorId = 5, RoomTypeId = 4,StatusId=1 },
                new Room { RoomId = 17,Number=17,  FloorId = 3, RoomTypeId = 5,StatusId=1 },
                new Room { RoomId = 18,Number=18,  FloorId = 5, RoomTypeId = 3,StatusId=1 },
                new Room { RoomId = 19, Number=19, FloorId = 2, RoomTypeId = 4,StatusId=1 });

            modelBuilder.Entity<Position>().HasData(
                        new Position { PositionId = 1, Name = "Admin" },
                        new Position { PositionId = 2, Name = "Manager" },
                        new Position { PositionId = 3, Name = "Receptionist" });

            modelBuilder.Entity<Staff>().HasData(
                new Staff { StaffId = 1, Name = "Kutlo",Surname="Ranno", PositionId = 1 },
                new Staff { StaffId = 2, Name = "Dineo",Surname="Radikgaka", PositionId = 2 },
                new Staff { StaffId = 3, Name = "Maatla",Surname="Boima", PositionId = 1 },
                new Staff { StaffId = 4, Name = "Jeff",Surname="Hardy", PositionId = 3 },
                new Staff { StaffId = 5, Name = "Fikile",Surname= "Loeto",PositionId = 2 },
                new Staff { StaffId = 6, Name = "Katlo",Surname="Ranno", PositionId = 3 });

            modelBuilder.Entity<Booking>().HasData(
                new Booking { BookingId = 1, RoomId = 1, StaffId = 1, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1) },
                new Booking { BookingId = 2, RoomId = 2, StaffId = 3, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1) },
                new Booking { BookingId = 3, RoomId = 3, StaffId = 4, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1) },
                new Booking { BookingId = 4, RoomId = 4, StaffId = 5, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1) },
                new Booking { BookingId = 5, RoomId = 5, StaffId = 6, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1) },
                new Booking { BookingId = 6, RoomId = 6, StaffId = 1, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1) });
    }
}