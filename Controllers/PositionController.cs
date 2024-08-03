using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhiteLotus.Main.Service.Data;
using WhiteLotus.Main.Service.Entities;

namespace WhiteLotus.Main.Service.Controllers;

[ApiController]
[Route("positions")]
public class PositionController(WhiteLotusContext context):ControllerBase
{
    private WhiteLotusContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<PositionDto>>> GetAll()
    {
        var positions = (await _context.Positions.ToListAsync()).Select(p=>p.AsDto());

        if(positions is null) return NotFound();

        return Ok(positions);
    }

    [HttpGet("fat")]
    public async Task<ActionResult<IReadOnlyCollection<PositionDtoRelational>>> GetAllFat()
    {
        var positions = await _context.Positions.ToListAsync();
        var staff = await _context.Staffs.ToListAsync();


        if(positions is null) return NotFound();

        var positionsFat = positions.
                                    Select(pos => pos.AsDtoRelational(staff.Where(staff => staff.PositionId == pos.PositionId).ToList()));

        return Ok(positionsFat);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PositionDto>> GetById(int id)
    {
        var position = await _context.Positions.FindAsync(id);

        if (position  is null) return NotFound();

        return Ok(position.AsDto());
    }

    [HttpGet("fat/{id}")]
    public async Task<ActionResult<PositionDtoRelational>> GetByIdFat(int id)
    {
        var position = await _context.Positions.FindAsync(id);

        if(position is null)return NotFound();

        var staff = await _context.Staffs.Where(st=> st.PositionId==id).ToListAsync();

        return Ok(position.AsDtoRelational(staff));
    }

    [HttpPost]
    public async Task<ActionResult<PositionDto>> Create(CreatePositionDto createPos)
    {

        if(createPos is null) return null;

        var pos = new Position
        {
            PositionId=createPos.PositionId,
            Name=createPos.Name
        };

         _context.Positions.Add(pos);

        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new{id=pos.PositionId}, pos.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PositionDto>> Update(int id,UpdatePositionDto updateDto)
    {
        var pos = await _context.Positions.FindAsync(id);

        if(pos is null) return NotFound();

        pos.Name = updateDto.Name;

        _context.Positions.Update(pos);

        return Ok(pos.AsDto());

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var pos =await _context.Positions.FirstOrDefaultAsync(pos=>pos.PositionId==id);

        if(pos is null)return NotFound();

        _context.Positions.Remove(pos);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}