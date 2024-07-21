using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhiteLotus.Main.Service.Data;
using WhiteLotus.Main.Service.Dtos;
using WhiteLotus.Main.Service.Entities;
using WhiteLotus.Main.Service.Repositories;

namespace WhiteLotus.Main.Service.Controllers;

[ApiController]
[Route("bookings")]
public class BookingController(IRepository<Booking> repository,WhiteLotusContext context) : Controller
{
    private WhiteLotusContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<BookingDto>>> GetAll()
    {
        var bookings = (await _context.Bookings.ToListAsync()).Select(b=>b.AsDto());


        if(bookings is null) return NotFound();

        return Ok(bookings);

    }

    [HttpGet("fat")]
    public async Task<ActionResult<IReadOnlyCollection<BookingDtoRelational>>> GetAllFat()
    {

        var bookings = (await _context.Bookings.ToListAsync());
        var rooms = (await _context.Rooms.ToListAsync());
        var staffs = (await _context.Staffs.ToListAsync());

        var bookingsRelational = bookings.Select(b=>b.AsDtoRelational(
            rooms.FirstOrDefault(r=>r.RoomId==b.RoomId),
            staffs.FirstOrDefault(s=>s.StaffId==b.StaffId)
        ));

        return Ok(bookingsRelational);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingDto>> GetById(int id)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);

        if(booking is null) return NotFound();

        return Ok(booking.AsDto());
    }

    [HttpGet("fat/{id}")]
    public async Task<ActionResult<BookingDtoRelational>> GetByIdFat(int id)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == booking.RoomId);
        var staff = await _context.Staffs.FirstOrDefaultAsync(s => s.StaffId == booking.StaffId);

        if(booking is null) return NotFound();

        return Ok(booking.AsDtoRelational(room,staff));
    }

    [HttpPost]
    public async Task<ActionResult<BookingDto>> Create(CreateBookingDto createBookingDto)
    {

        if(createBookingDto is null) return BadRequest(); 

        var booking = new Booking
        {
            BookingId = createBookingDto.BookingId,
            RoomId = createBookingDto.RoomId,
            StaffId = createBookingDto.StaffId,
            StartDate = createBookingDto.StartDate,
            EndDate = createBookingDto.EndDate
        };

        await _context.Bookings.AddAsync(booking);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new {id = booking.BookingId}, booking.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BookingDto>> Update(int id, UpdateBookingDto updateBookingDto)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);

        if(booking is null) return NotFound();

        booking.RoomId = updateBookingDto.RoomId;
        booking.StartDate = updateBookingDto.StartDate;
        booking.EndDate = updateBookingDto.EndDate;

        _context.Update(booking);

        await _context.SaveChangesAsync();

        return Ok(booking.AsDto());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);

        if(booking is null) return NotFound();

        _context.Bookings.Remove(booking);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}

