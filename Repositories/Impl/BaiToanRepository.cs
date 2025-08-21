using KhoBau_BE.DBContext;
using KhoBau_BE.Entities;
using KhoBau_BE.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KhoBau_BE.Repositories.Impl
{
    public class BaiToanRepository : IBaiToanRepository
    {
        private readonly AppDbContext _db;
        public BaiToanRepository(AppDbContext db) { _db = db; }

        public async Task<int> AddAsync(BaiToanModel entity)
        {
            _db.BaiToans.Add(entity);
            await _db.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<BaiToanModel?> GetByIdAsync(int id)
        {
            return await _db.BaiToans.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<BaiToanModel>> ListAllAsync()
        {
            return await _db.BaiToans.OrderByDescending(b => b.Id).ToListAsync();
        }

        public async Task SaveKetQuaAsync(KetQuaModel kq)
        {
            _db.KetQuas.Add(kq);
            await _db.SaveChangesAsync();
        }
    }
}