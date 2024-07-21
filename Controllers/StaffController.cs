using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhiteLotus.Main.Service.Data;
using WhiteLotus.Main.Service.Dtos;
using WhiteLotus.Main.Service.Entities;
using WhiteLotus.Main.Service.Repositories;

namespace WhiteLotus.Main.Service.Controllers;

[ApiController]
[Route("staff")]
public class StaffController(IRepository<Staff> staff,WhiteLotusContext context) : Controller
{
    private WhiteLotusContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<StaffDto>>> GetAll()
    {
        var staff = await _context.Staffs.ToListAsync();

        if (staff is null) return NotFound();

        return Ok(staff.Select(s=>s.AsDto()));
    }

    [HttpGet("fat")]
    public async Task<ActionResult<IReadOnlyCollection<StaffDtoRelational>>> GetAllFat()
    {
        var staff = await _context.Staffs.ToListAsync();
        var positions = await _context.Positions.ToListAsync();
        var bookings = await _context.Bookings.ToListAsync();

        var staffRelational = staff.Select(s => s.AsDtoRelational(
            positions.FirstOrDefault(p => p.PositionId == s.PositionId),
            bookings.Where(b => b.StaffId == s.StaffId).ToList()
        ));

        if (staffRelational is null) return NotFound();

        return Ok(staffRelational);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StaffDto>> GetById(int id)
    {
        var staff = await _context.Staffs.FirstOrDefaultAsync(s => s.StaffId == id);

        if(staff is null) return NotFound();

        return Ok(staff.AsDto());

    }

    [HttpGet("fat/{id}")]
    public async Task<ActionResult<StaffDtoRelational>> GetByIdFat(int id)
    {
        var staff = await _context.Staffs.FirstOrDefaultAsync(s => s.StaffId == id);
        var positions = await _context.Positions.ToListAsync();
        var bookings = await _context.Bookings.ToListAsync();

        if(staff is null) return NotFound();

        var staffRelational = staff.AsDtoRelational(
            positions.FirstOrDefault(p => p.PositionId == staff.PositionId),
            bookings.Where(b => b.StaffId == staff.StaffId).ToList()
        );

        return Ok(staffRelational);
    }

    [HttpPost]
    public async Task<ActionResult<StaffDto>> Create(CreateStaffDto staffDto)
    {
        var staff = new Staff
        {
            Name = staffDto.Name,
            Surname = staffDto.Surname,
            PositionId = staffDto.PositionId
        };

        await _context.Staffs.AddAsync(staff);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = staff.StaffId }, staff.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateStaffDto staffDto)
    {
        var staff = await _context.Staffs.FirstOrDefaultAsync(s => s.StaffId == id);

        if(staff is null) return NotFound();

        staff.Name = staffDto.Name;
        staff.Surname = staffDto.Surname;
        staff.PositionId = staffDto.PositionId;

        _context.Staffs.Update(staff);

        await _context.SaveChangesAsync();

        return Ok(staff.AsDto());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var staff = await _context.Staffs.FirstOrDefaultAsync(s => s.StaffId == id);

        if(staff is null) return NotFound();

        _context.Staffs.Remove(staff);

        await _context.SaveChangesAsync();

        return NoContent();
    }

}