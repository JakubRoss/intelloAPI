using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace intelloAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KontrahentController : ControllerBase
    {
        private readonly IKontrahentService _kontrahentService;

        public KontrahentController(IKontrahentService kontrahentService)
        {
            _kontrahentService = kontrahentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var kontrahenci = await _kontrahentService.GetAllAsync();
            return Ok(kontrahenci);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var kontrahent = await _kontrahentService.GetByIdAsync(id);
            if (kontrahent == null)
                return NotFound();

            return Ok(kontrahent);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateKontrahentDto createKontrahentDto)
        {
            var id = await _kontrahentService.Create(createKontrahentDto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateKontrahentDto createKontrahentDto)
        {
            var result = await _kontrahentService.UpdateAsync(createKontrahentDto, id);
            if (result == 0)
                return NotFound("Kontrahent does not exist or has Dokument przyjecia assigned to them.");

            return Ok("Kontrahent has been updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _kontrahentService.DeleteAsync(id);
            if (!result)
                return NotFound("Kontrahent does not exist or has Dokument przyjecia assigned to them.");

            return Ok("Kontrahent has been removed");
        }
    }

}
