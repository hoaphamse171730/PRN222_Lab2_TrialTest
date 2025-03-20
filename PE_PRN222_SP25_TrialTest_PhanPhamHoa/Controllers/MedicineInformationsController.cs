using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data_Access.Entities;
using Business_Logic.Interfaces;
using Business_Logic.Utils;

namespace PE_PRN222_SP25_TrialTest_PhanPhamHoa.Controllers
{
    public class MedicineInformationsController : Controller
    {
        private readonly IMedicineService _medicineService;

        public MedicineInformationsController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        // GET: MedicineInformations
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 3)
        {
            PagedResult<Data_Access.Entities.MedicineInformation> pagedMedicines =
                await _medicineService.GetListAsync(pageSize, pageNumber);
            return View(pagedMedicines);
        }

        // GET: MedicineInformations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicineInformation = await _medicineService.GetByIdAsync(id);
            if (medicineInformation == null)
            {
                return NotFound();
            }

            return View(medicineInformation);
        }

        // GET: MedicineInformations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MedicineInformations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicineName,ActiveIngredients,ExpirationDate,DosageForm,WarningsAndPrecautions,ManufacturerId")] MedicineInformation medicineInformation)
        {
            if (ModelState.IsValid)
            {
                await _medicineService.CreateAsync(medicineInformation);
                return RedirectToAction(nameof(Index));
            }
            return View(medicineInformation);
        }

        // GET: MedicineInformations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicineInformation = await _medicineService.GetByIdAsync(id);
            if (medicineInformation == null)
            {
                return NotFound();
            }
            return View(medicineInformation);
        }

        // POST: MedicineInformations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MedicineId,MedicineName,ActiveIngredients,ExpirationDate,DosageForm,WarningsAndPrecautions,ManufacturerId")] MedicineInformation medicineInformation)
        {
            if (id != medicineInformation.MedicineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _medicineService.UpdateAsync(medicineInformation);
                }
                catch
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medicineInformation);
        }

        // GET: MedicineInformations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicineInformation = await _medicineService.GetByIdAsync(id);
            if (medicineInformation == null)
            {
                return NotFound();
            }

            return View(medicineInformation);
        }

        // POST: MedicineInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _medicineService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
