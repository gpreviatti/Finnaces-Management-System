using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.Entrance;

namespace Domain.Interfaces.Services
{
    public interface IEntranceService : IBaseService
    {
        Task<EntranceResultDto> FindByIdAsync(Guid id);
        Task<EntranceUpdateDto> FindByIdUpdateAsync(Guid id);
        Task<IEnumerable<EntranceResultDto>> FindAllAsyncWithCategory();
        Task<IEnumerable<EntranceResultDto>> FindAsyncLastTenEntrancesWithCategories();
        Task<EntranceResultDto> CreateAsync(EntranceCreateDto entrace);
        Task<EntranceResultDto> UpdateAsync(EntranceUpdateDto entrace);
        Task<bool> DeleteAsync(Guid id);
    }
}
