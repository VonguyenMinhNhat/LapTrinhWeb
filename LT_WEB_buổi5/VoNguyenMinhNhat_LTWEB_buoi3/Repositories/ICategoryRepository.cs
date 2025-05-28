﻿using VoNguyenMinhNhat_LTWEB_buoi3.Models;

namespace VoNguyenMinhNhat_LTWEB_buoi3.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }

}
