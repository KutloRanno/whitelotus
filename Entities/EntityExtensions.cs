using System.Reflection.Metadata.Ecma335;
using WhiteLotus.Main.Service.Dtos;

namespace WhiteLotus.Main.Service.Entities;

public static class EntityExtensions
{
    public static RoomDto AsDto(this Room room)
    {
        return new RoomDto
        (
            room.RoomId,
            room.Number,
            room.FloorId,
            room.RoomTypeId,
            room.StatusId
        );
    }

    public static RoomDtoRelational AsDtoRelational(this Room room, RoomType roomType, Status status, Floor floor)
    {
        return new RoomDtoRelational
        (
            room.RoomId,
            room.Number,
            room.RoomTypeId,
            room.StatusId,
            roomType.Name,
            roomType.Price,
            status.Name,
            room.FloorId,
            floor.Name
        );
    }

    public static StaffDto AsDto(this Staff staff)
    {
        return new StaffDto
        (
            staff.StaffId,
            staff.Name,
            staff.Surname,
            staff.PositionId
        );
    }

    public static StaffDtoRelational AsDtoRelational(this Staff staff, Position position,List<Booking> bookings)
    {

        var bookingsDtos = new List<BookingDto>();

        foreach (var booking in bookings)
        {
            bookingsDtos.Add(booking.AsDto());
        }

        return new StaffDtoRelational
        (
            staff.StaffId,
            staff.Name,
            staff.Surname,
            staff.PositionId,
            position.Name,
            bookingsDtos
        );
    }
    public static BookingDto AsDto(this Booking booking)
    {
        return new BookingDto
        (
            booking.BookingId,
            booking.RoomId,
            booking.StaffId,
            booking.StartDate,
            booking.EndDate
        );
    }

    public static BookingDtoRelational AsDtoRelational(this Booking booking, Room room, Staff staff)
    {
        return new BookingDtoRelational
        (
            booking.BookingId,
            booking.RoomId,
            booking.StaffId,
            booking.StartDate,
            booking.EndDate,
            room.Number,
            staff.Name,
            staff.Surname
        );
    }

    public static PositionDto AsDto(this Position position)
    {
        return new PositionDto
        (
            position.PositionId,
            position.Name
        );
    }

    public static PositionDtoRelational AsDtoRelational(this Position position, List<Staff> staff)
    {
        var staffDtos = new List<StaffDto>();

        foreach(var staffer in staff)
        {
            staffDtos.Add(staffer.AsDto());
        }

        return new PositionDtoRelational
        (
            position.PositionId,
            position.Name,
            staffDtos
        );
    }
}