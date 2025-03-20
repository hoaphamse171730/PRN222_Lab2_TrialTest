using Business_Logic.Interfaces;
using Business_Logic.Utils;
using Data_Access.Entities;
using Data_Access.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Services
{

    public class MedicineService : IMedicineService
    {
        private readonly IUOW _unitOfWork;

        public MedicineService(IUOW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<MedicineInformation>> GetListAsync(int pageSize, int pageNumber)
        {
            var query = _unitOfWork.GetRepository<MedicineInformation>().Entities
                        .Include(m => m.Manufacturer);

            var totalCount = await query.CountAsync();

            var items = await query
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

            return new PagedResult<MedicineInformation>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<MedicineInformation> GetByIdAsync(string id)
        {
            return await _unitOfWork.GetRepository<MedicineInformation>().Entities
                        .Include(m => m.Manufacturer)
                        .FirstOrDefaultAsync(m => m.MedicineId == id);
        }

        public async Task CreateAsync(MedicineInformation medicine)
        {
            await _unitOfWork.GetRepository<MedicineInformation>().InsertAsync(medicine);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(MedicineInformation medicine)
        {
            _unitOfWork.GetRepository<MedicineInformation>().Update(medicine);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var medicine = await _unitOfWork.GetRepository<MedicineInformation>().GetByIdAsync(id);
            if (medicine != null)
            {
                await _unitOfWork.GetRepository<MedicineInformation>().DeleteAsync(medicine);
                await _unitOfWork.SaveAsync();
            }
        }
    }

}
