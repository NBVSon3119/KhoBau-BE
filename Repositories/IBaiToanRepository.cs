using KhoBau_BE.Entities;
namespace KhoBau_BE.Repositories
{
    public interface IBaiToanRepository
    {
        Task<int> AddAsync(BaiToanModel entity);
        Task<BaiToanModel?> GetByIdAsync(int id);
        Task<List<BaiToanModel>> ListAllAsync();
        Task SaveKetQuaAsync(KetQuaModel kq);
    }
}