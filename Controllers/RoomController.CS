using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhiteLotus.Main.Service.Data;
using WhiteLotus.Main.Service.Dtos;
using WhiteLotus.Main.Service.Entities;

namespace WhiteLotus.Main.Service.Controllers;

[ApiController]
[Route("rooms")]
public class RoomController(WhiteLotusContext context) : Controller
{
    private WhiteLotusContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<RoomDto>>> GetAll(){
        var rooms = (await _context.Rooms.ToListAsync())
                                            .Select(room => room.AsDto());

        return Ok(rooms);
    }


    [HttpGet("fat")]
    public async Task<ActionResult<IReadOnlyCollection<RoomDtoRelational>>> GetAllFat()
    {
        var bookings = await _context.Bookings.ToListAsync();
        var rooms = (await _context.Rooms.ToListAsync())
                                            .Select(room => room
                                            .AsDtoRelational(
                                                _context.RoomTypes.FirstOrDefault(rt=>rt.RoomTypeId==room.RoomTypeId),
                                                _context.Statuses.FirstOrDefault(s=>s.StatusId==room.StatusId),
                                                _context.Floors.FirstOrDefault(f=>f.FloorId==room.FloorId),
                                                bookings.Where(b=>b.RoomId==room.RoomId).ToList()
                                                ));
    
        if (rooms is null) return NotFound();

        return Ok(rooms);;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<RoomDto>> GetById(int id)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(r=>r.RoomId==id);

        if(room is null)return NotFound();

        return Ok(room.AsDto());
    }

    [HttpGet("fat/{id}")]
    public async Task<ActionResult<RoomDtoRelational>> GetByIdFat(int id)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(r=>r.RoomId==id);
        var roomType = await _context.RoomTypes.FirstOrDefaultAsync(rt=>rt.RoomTypeId==room.RoomTypeId);
        var status = await _context.Statuses.FirstOrDefaultAsync(s=>s.StatusId==room.StatusId);
        var floor = await _context.Floors.FirstOrDefaultAsync(f => f.FloorId == room.FloorId);
        var bookings = ((await _context.Bookings.ToListAsync()).Where(b=>b.RoomId==room.RoomId)).ToList();


        if(room is null)return NotFound();

        return Ok(room.AsDtoRelational(roomType,status,floor,bookings));
    } 

    [HttpPost]
    public async Task<ActionResult<RoomDto>> Post(CreateRoomDto roomDto)
    {
        var room = new Room
        {
            RoomId = roomDto.RoomId,
            Number = roomDto.Number,
            FloorId = roomDto.FloorId,
            RoomTypeId = roomDto.RoomTypeId,
            StatusId = roomDto.StatusId
        };

        await _context.AddAsync(room);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = room.RoomId }, room.AsDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(r=>r.RoomId==id);
        
        if(room is null)return NotFound();

        _context.Rooms.Remove(room);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RoomDto>> Update(int id, UpdateRoom updateRoom)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(r=>r.RoomId==id);

        if(room is null)return NotFound();

        room.Number = updateRoom.Number;
        room.FloorId = updateRoom.FloorId;
        room.RoomTypeId = updateRoom.RoomTypeId;
        room.StatusId = updateRoom.StatusId;

        _context.Rooms.Update(room);

        await _context.SaveChangesAsync();

        return Ok(room.AsDto());
    }
}
