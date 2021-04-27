using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.Category;

namespace Domain.Interfaces.Services
{
    public interface ICategoryService : IBaseService
    {
        Task<CategoryResultDto> FindByIdAsync(Guid id);
        Task<IEnumerable<CategoryResultDto>> FindAllAsync();
        Task<CategoryUpdateDto> FindByIdUpdateAsync(Guid id);
        Task<IEnumerable<CategoryResultDto>> FindAsyncAllCommonAndUserCategories();
        Task<CategoryResultDto> CreateAsync(CategoryCreateDto category);
        Task<CategoryResultDto> UpdateAsync(CategoryUpdateDto category);
        Task<bool> DeleteAsync(Guid id);
    }
}
