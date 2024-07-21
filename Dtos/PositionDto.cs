using WhiteLotus.Main.Service.Dtos;
using WhiteLotus.Main.Service.Entities;

namespace WhiteLotus.Main.Service;

public record PositionDto
(
    int PositionId,
    string Name
);

public record PositionDtoRelational
(
    int PositionId,
    string Name,
    List<StaffDto> StaffDtos
);

public record CreatePositionDto
(
    int PositionId,
    string Name
);

public record UpdatePositionDto
(
    string Name
);

