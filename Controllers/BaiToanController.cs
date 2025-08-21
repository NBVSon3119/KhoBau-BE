using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using KhoBau_BE.Entities;
using KhoBau_BE.DTOs;
using KhoBau_BE.Services;
using KhoBau_BE.Application;

namespace KhoBau_BE.Controllers;

[ApiController]
[Route("api/bai-toan")]
public class BaiToanController : ControllerBase
{
    private readonly BaiToanService _service;

    public BaiToanController(BaiToanService service) { _service = service; }

    [HttpPost]
    public async Task<IActionResult> Tao([FromBody] BaiToanDto dto)
    {
        if (dto.MaTran.Length != dto.SoHang || dto.MaTran.Any(r => r.Length != dto.SoCot))
            return BadRequest("Kích thước ma trận không khớp n và m.");

        var entity = new BaiToanModel
        {
            SoHang = dto.SoHang,
            SoCot = dto.SoCot,
            SoP = dto.SoP,
            MaTranJson = JsonSerializer.Serialize(dto.MaTran)
        };
        var id = await _service.TaoBaiToanAsync(entity);

        var kq = await _service.GiaiBaiToanAsync(id);

        return CreatedAtAction(nameof(Lay), new { id }, new { id, kq.NhienLieu });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Lay(int id)
    {
        var b = await _service.LayBaiToanAsync(id);
        if (b == null) return NotFound();
        var matrix = JsonSerializer.Deserialize<int[][]>(b.MaTranJson) ?? Array.Empty<int[]>();
        return Ok(new { b.Id, b.SoHang, b.SoCot, b.SoP, matrix });
    }

    [HttpGet("danh-sach-input")]
    public async Task<IActionResult> DanhSach()
    {
        var list = await _service.DanhSachAsync();
        return Ok(list);
    }

    [HttpPost("ket-qua/{id}")]
    public async Task<IActionResult> KetQua(int id)
    {
        var kq = await _service.GiaiBaiToanAsync(id);
        return Ok(new { kq.Id, kq.BaiToanId, kq.NhienLieu });
    }
}
