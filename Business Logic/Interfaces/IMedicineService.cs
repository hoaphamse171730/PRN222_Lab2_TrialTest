using Business_Logic.Utils;
using Data_Access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Interfaces
{
    public interface IMedicineService
    {
        Task<PagedResult<MedicineInformation>> GetListAsync(int pageSize, int pageNumber);
        Task<MedicineInformation> GetByIdAsync(string id);
        Task CreateAsync(MedicineInformation medicine);
        Task UpdateAsync(MedicineInformation medicine);
        Task DeleteAsync(string id);
    }

}
