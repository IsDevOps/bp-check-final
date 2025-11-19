using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BPCalculator.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public BloodPressure BP { get; set; } = new BloodPressure();
        
        public BloodPressureHistory History { get; set; } = new BloodPressureHistory();

        public void OnGet()
        {
            BP = new BloodPressure { Systolic = 100, Diastolic = 60 };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (BP.Systolic <= BP.Diastolic)
            {
                ModelState.AddModelError("", "Systolic must be greater than Diastolic");
            }
            else
            {
                // NEW FEATURE: Add to history
                History.AddRecord(BP.Systolic, BP.Diastolic, "Web User");
            }

            return Page();
        }
    }
}