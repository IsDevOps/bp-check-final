using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace BPCalculator
{
    // BP categories
    public enum BPCategory
    {
        [Display(Name="Low Blood Pressure")] Low,
        [Display(Name="Ideal Blood Pressure")]  Ideal,
        [Display(Name="Pre-High Blood Pressure")] PreHigh,
        [Display(Name ="High Blood Pressure")]  High
    };

    public class BloodPressure
    {
        public const int SystolicMin = 70;
        public const int SystolicMax = 190;
        public const int DiastolicMin = 40;
        public const int DiastolicMax = 100;

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }                       // mmHG

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }                      // mmHG

        // calculate BP category
        public BPCategory Category
        {
            get
            {
                // Get categories for systolic and diastolic
                var systolicCategory = GetSystolicCategory(Systolic);
                var diastolicCategory = GetDiastolicCategory(Diastolic);
                
                // Return the worst category
                return GetWorstCategory(systolicCategory, diastolicCategory);
            }
        }

        private BPCategory GetSystolicCategory(int systolic)
        {
            if (systolic < 90) return BPCategory.Low;
            if (systolic <= 120) return BPCategory.Ideal;
            if (systolic <= 139) return BPCategory.PreHigh;
            return BPCategory.High;
        }

        private BPCategory GetDiastolicCategory(int diastolic)
        {
            if (diastolic < 60) return BPCategory.Low;
            if (diastolic <= 80) return BPCategory.Ideal;
            if (diastolic <= 89) return BPCategory.PreHigh;
            return BPCategory.High;
        }

        private BPCategory GetWorstCategory(BPCategory systolicCat, BPCategory diastolicCat)
        {
            // Order of severity: Ideal < Low < PreHigh < High
            return (BPCategory)Math.Max((int)systolicCat, (int)diastolicCat);
        }
    }

    // NEW FEATURE: Blood Pressure History (under 30 lines)
    public class BloodPressureHistory
    {
        private readonly List<BloodPressureRecord> _records = new();
        
        public void AddRecord(int systolic, int diastolic, string user = "User")
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            _records.Add(new BloodPressureRecord(bp, user));
            if (_records.Count > 5) _records.RemoveAt(0); // Keep only last 5
        }
        
        public IReadOnlyList<BloodPressureRecord> GetHistory() => _records.AsReadOnly();
        public void ClearHistory() => _records.Clear();
        public int HistoryCount => _records.Count;
    }

    public class BloodPressureRecord
    {
        public int Systolic { get; }
        public int Diastolic { get; }
        public BPCategory Category { get; }
        public DateTime Timestamp { get; }
        public string User { get; }

        public BloodPressureRecord(BloodPressure bp, string user)
        {
            Systolic = bp.Systolic;
            Diastolic = bp.Diastolic;
            Category = bp.Category;
            Timestamp = DateTime.Now;
            User = user;
        }
        
        public override string ToString() => 
            $"{Timestamp:HH:mm} - {Systolic}/{Diastolic} ({Category}) - {User}";
    }
}