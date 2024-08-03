using Microsoft.AspNetCore.Razor.Hosting;
using WhiteLotus.Main.Service.Entities;

namespace WhiteLotus.Main.Service.Dtos;

public record RoomDto
(
     int RoomId,
     int Number,
     int FloorId,
     int RoomTypeId,
     int StatusId
);

public record RoomDtoRelational
(
     int RoomId,
     int Number,
     int RoomTypeId,
     int StatusId,
     string RoomTypeName,
     decimal Price,
     string StatusName,
     int FloorId,
     string FloorName,
     List<BookingDto> Bookings
);

public record CreateRoomDto
(
     int RoomId,
     int Number,
     int FloorId,
     int RoomTypeId,
     int StatusId
);

public record UpdateRoom
(
     int Number,
     int FloorId,
     int RoomTypeId,
     int StatusId
);

