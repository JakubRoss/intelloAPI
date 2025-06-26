namespace intelloAPI.Controllers
{
    using Application.DTOs;
    using Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class TowaryController : ControllerBase
    {
        private readonly ITowarServices towarServices;
        private readonly IPozycjaDokumentuService pozycjaDokumentuService;

        public TowaryController(ITowarServices towarServices, IPozycjaDokumentuService pozycjaDokumentuService)
        {
            this.towarServices = towarServices;
            this.pozycjaDokumentuService = pozycjaDokumentuService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TowarDto>>> GetTowary()
        {
            var towary = await towarServices.GetAllAsync();
            var towaryDto = towary.Select(t => new TowarDto
            {
                Id = t.Id,
                NazwaTowaru = t.NazwaTowaru,
                JednostkaMiary = t.JednostkaMiary
            });

            return Ok(towaryDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TowarDto>> GetTowar(int id)
        {
            var towar = await towarServices.GetByIdAsync(id);

            if (towar == null)
                return NotFound();

            return new TowarDto
            {
                Id = towar.Id,
                NazwaTowaru = towar.NazwaTowaru,
                JednostkaMiary = towar.JednostkaMiary
            };
        }

        [HttpPost]
        public async Task<ActionResult<TowarDto>> CreateTowar([FromBody] CreateTowarDto towarDto)
        {
            if(towarDto == null || string.IsNullOrEmpty(towarDto.Nazwa) || string.IsNullOrEmpty(towarDto.JednostkaMiary))
                return BadRequest();

            var creratedTowar = await towarServices.CreateAsync(towarDto);
            if (creratedTowar == null) 
                return NotFound();

            return CreatedAtAction(nameof(GetTowar), new { id = creratedTowar.Id }, creratedTowar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTowar(int id, [FromBody] UpdateTowarDto dto)
        {
            var towar = await towarServices.GetByIdAsync(id);

            if (towar == null)
                return NotFound();

            towar.NazwaTowaru = dto.Nazwa;
            towar.JednostkaMiary = dto.JednostkaMiary;

            await towarServices.UpdateAsync(new TowarDto 
            {
                Id = towar.Id,
                NazwaTowaru = towar.NazwaTowaru,
                JednostkaMiary = towar.JednostkaMiary
            });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTowar(int id)
        {
            var towar = await towarServices.GetByIdAsync(id);

            if (towar == null)
                return NotFound();

            // Sprawdzenie, czy towar występuje w pozycjach dokumentów
            var pozycjeDokumentu = await pozycjaDokumentuService.GetAllAsync();
            if (pozycjeDokumentu.Any(p => p.TowarId == towar.Id))
                return BadRequest("Nie można usunąć towaru, który jest przypisany do dokumentów.");

            await towarServices.DeleteAsync(id);

            return NoContent();
        }
    }

}
