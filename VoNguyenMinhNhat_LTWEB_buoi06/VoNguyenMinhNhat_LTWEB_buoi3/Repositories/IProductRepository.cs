using VoNguyenMinhNhat_LTWEB_buoi3.Models;

namespace VoNguyenMinhNhat_LTWEB_buoi3.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);

        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
    }
}
