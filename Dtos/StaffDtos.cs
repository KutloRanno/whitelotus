using WhiteLotus.Main.Service.Entities;

namespace WhiteLotus.Main.Service.Dtos;

public record StaffDto
(
    int StaffId,
     string Name,
     string Surname,
     int PositionId
);

public record CreateStaffDto
(
    int StaffId,
    string Name,
    string Surname,
    int PositionId
);

public record UpdateStaffDto
(
    string Name,
    string Surname,
    int PositionId
);

public record StaffDtoRelational
(
    int StaffId,
    string Name,
    string Surname,
    int PositionId,
    string PositionName,
    List<BookingDto> Bookings
);
