using KhoBau_BE.Repositories;
using KhoBau_BE.Entities;
using System.Text.Json;
using KhoBau_BE.Application;

namespace KhoBau_BE.Services
{
    public class BaiToanService
    {
        private readonly IBaiToanRepository _repo;
        public BaiToanService(IBaiToanRepository repo) { _repo = repo; }

        public async Task<int> TaoBaiToanAsync(BaiToanModel b) => await _repo.AddAsync(b);
        public async Task<BaiToanModel?> LayBaiToanAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task<List<BaiToanModel>> DanhSachAsync() => await _repo.ListAllAsync();

        public async Task<KetQuaModel> GiaiBaiToanAsync(int id)
        {
            var bai = await _repo.GetByIdAsync(id);
            if (bai == null) throw new Exception("Không tìm thấy bài toán");

            var matrix = JsonSerializer.Deserialize<int[][]>(bai.MaTranJson) ?? Array.Empty<int[]>();

            var solver = new TreasureSolver();
            var res = solver.Solve(matrix, bai.SoHang, bai.SoCot, bai.SoP);

            var kq = new KetQuaModel
            {
                BaiToanId = id,
                NhienLieu = res.TotalFuel,
                PathJson = JsonSerializer.Serialize(res.Path)
            };

            await _repo.SaveKetQuaAsync(kq);
            return kq;
        }
    }
}