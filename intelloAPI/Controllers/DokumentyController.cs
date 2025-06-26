using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace intelloAPI.Controllers
{
    [ApiController]
    [Route("api/dokumenty")]
    public class DokumentyController : ControllerBase
    {
        private readonly IDokumentPrzyjeciaService _dokumentPrzyjeciaService;
        private readonly IPozycjaDokumentuService _pozycjaDokumentuService;

        public DokumentyController(
            IDokumentPrzyjeciaService dokumentPrzyjeciaService, 
            IPozycjaDokumentuService pozycjaDokumentuService, 
            IKontrahentService kontrahentService)
        {
            _dokumentPrzyjeciaService = dokumentPrzyjeciaService;
            _pozycjaDokumentuService = pozycjaDokumentuService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dokumenty = await _dokumentPrzyjeciaService.GetAllAsync();
            return Ok(dokumenty);
        }

        [HttpGet("{dokumentId}")]
        public async Task<IActionResult> GetById(int dokumentId)
        {
            var dokument = await _dokumentPrzyjeciaService.GetByIdAsync(dokumentId);
            if (dokument == null)
                return NotFound();

            return Ok(dokument);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDokumentPrzyjeciaDto createDokumentPrzyjeciaDto)
        {
            var dokumentId = await _dokumentPrzyjeciaService.Create(createDokumentPrzyjeciaDto);
            return CreatedAtAction(nameof(GetById), new { dokumentId }, null);
        }

        [HttpPut("{dokumentId}")]
        public async Task<IActionResult> UpdateDokumentPrzyjecia(int dokumentId, UpdateDokumentPrzyjeciaDto dokumentPrzyjeciaDto)
        {

            var dokumentPrzyjecia = await _dokumentPrzyjeciaService.UpdateAsync((new UpdateDokumentPrzyjeciaDto
            {
                Symbol = dokumentPrzyjeciaDto.Symbol,
                KontrahentId = dokumentPrzyjeciaDto.KontrahentId,
            }), dokumentId);

            return Ok(dokumentPrzyjecia);
        }

        [HttpDelete("{dokumentId}")]
        public async Task<IActionResult> Delete(int dokumentId)
        {
            var deleted = await _dokumentPrzyjeciaService.DeleteAsync(dokumentId);
            return deleted ? NoContent() : NotFound();
        }

        [HttpPost("{dokumentId}/pozycje/")]
        public async Task<IActionResult> CreatePozycja([FromBody] CreatePozycjaDokumentuDto createPozycjaDokumentuDto, int dokumentId)
        {
            var dokumentPrzyjecia = await _dokumentPrzyjeciaService.GetByIdAsync(dokumentId);
            var id = await _pozycjaDokumentuService.CreateAsync(createPozycjaDokumentuDto, dokumentPrzyjecia);
            return CreatedAtAction(nameof(GetById), new { dokumentId }, null);
        }

        [HttpPut("{dokumentId}/pozycje/{pozycjaId}")]
        public async Task<IActionResult> UpdatePozycjeDokumentow(int dokumentId, int pozycjaId, [FromBody] UpdatePozycjaDokumentuDto updatePozycjaDokumentuDto)
        {
            var pozycjaDokumentu = await _pozycjaDokumentuService.GetByIdAsync(pozycjaId);
            if (pozycjaDokumentu == null || pozycjaDokumentu.DokumentPrzyjeciaId != dokumentId)
                return NoContent();
            pozycjaDokumentu.Ilosc = updatePozycjaDokumentuDto.Ilosc;
            var updatedPozycja = await _pozycjaDokumentuService.UpdateAsync(pozycjaDokumentu);

            if (updatedPozycja == null)
                return NoContent();

            return CreatedAtAction(
                actionName: "GetById",
                controllerName: "PozycjaDokumentu",
                routeValues: new { id = updatedPozycja.Id },
                value: updatedPozycja);
        }

        [HttpDelete("{dokumentId}/pozycje/{pozycjaId}")]
        public async Task<IActionResult> DeletePozycjaFromDokument(int dokumentId, int pozycjaId)
        {
            var dokument = await _dokumentPrzyjeciaService.GetByIdAsync(dokumentId);

            if (dokument == null)
                return NotFound("Dokument nie istnieje.");

            var pozycja = dokument.Pozycje.FirstOrDefault(p => p.Id == pozycjaId);
            if (pozycja == null)
                return NotFound("Pozycja nie istnieje w tym dokumencie.");

            if (dokument.Pozycje.Count <= 1)
                return BadRequest("Nie można usunąć ostatniej pozycji z dokumentu.");

            await _pozycjaDokumentuService.DeleteAsync(pozycjaId);

            return NoContent();
        }
    }

}
