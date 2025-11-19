using System;

namespace BPCalculator.Services
{
    public class BloodPressureService
    {
        public BloodPressureResult CalculateCategory(int systolic, int diastolic)
        {
            // Validate input ranges
            if (systolic < 70 || systolic > 190 || diastolic < 40 || diastolic > 100)
                return new BloodPressureResult { Category = "Invalid reading" };
            
            if (systolic <= diastolic)
                return new BloodPressureResult { Category = "Invalid reading - Systolic must be higher than Diastolic" };

            // Determine individual categories
            string systolicCategory = GetSystolicCategory(systolic);
            string diastolicCategory = GetDiastolicCategory(diastolic);
            
            // Return the worst category
            string finalCategory = GetWorstCategory(systolicCategory, diastolicCategory);
            
            return new BloodPressureResult 
            { 
                Category = finalCategory,
                Systolic = systolic,
                Diastolic = diastolic
            };
        }

        private string GetSystolicCategory(int systolic)
        {
            if (systolic < 90) return "Low";
            if (systolic <= 120) return "Ideal"; 
            if (systolic <= 139) return "Pre-high";
            return "High";
        }

        private string GetDiastolicCategory(int diastolic)
        {
            if (diastolic < 60) return "Low";
            if (diastolic <= 80) return "Ideal";
            if (diastolic <= 89) return "Pre-high";
            return "High";
        }

        private string GetWorstCategory(string systolicCat, string diastolicCat)
        {
            string[] severityOrder = { "Ideal", "Low", "Pre-high", "High" };
            
            int systolicSeverity = Array.IndexOf(severityOrder, systolicCat);
            int diastolicSeverity = Array.IndexOf(severityOrder, diastolicCat);
            
            return severityOrder[Math.Max(systolicSeverity, diastolicSeverity)];
        }
    }

    public class BloodPressureResult
    {
        public string Category { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
    }
}