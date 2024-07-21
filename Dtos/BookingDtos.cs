namespace WhiteLotus.Main.Service.Dtos;

public record BookingDto
(
    int BookingId,
    int RoomId,
    int StaffId,
    DateTime StartDate,
    DateTime EndDate
);

public record BookingDtoRelational
(
    int BookingId,
    int RoomId,
    int StaffId,
    DateTime StartDate,
    DateTime EndDate,
    int RoomNumber,
    string StaffName,
    string StaffSurname
);

public record CreateBookingDto
(
    int BookingId,
    int RoomId,
    int StaffId,
    DateTime StartDate,
    DateTime EndDate
);

public record UpdateBookingDto
(
    int RoomId,
    int RoomNumber,
    DateTime StartDate,
    DateTime EndDate
);
