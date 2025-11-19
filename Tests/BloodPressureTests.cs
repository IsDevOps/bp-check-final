using Xunit;
using BPCalculator;
using System.ComponentModel.DataAnnotations;

namespace BPCalculator.Tests
{
    public class BloodPressureTests
    {
        [Theory]
        [InlineData(120, 80, BPCategory.Ideal)]
        [InlineData(140, 90, BPCategory.High)]
        [InlineData(85, 55, BPCategory.Low)]
        [InlineData(130, 85, BPCategory.PreHigh)]
        [InlineData(100, 70, BPCategory.Ideal)]
        [Trait("Category", "Unit")]
        public void CalculateCategory_ValidInputs_ReturnsCorrectCategory(int systolic, int diastolic, BPCategory expected)
        {
            // Arrange
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            
            // Act
            var result = bp.Category;
            
            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        [Trait("Category", "BDD")]
        public void Given_IdealBloodPressure_When_CalculatingCategory_Then_ReturnsIdeal()
        {
            // Given
            var bp = new BloodPressure { Systolic = 110, Diastolic = 70 };
            
            // When
            var category = bp.Category;
            
            // Then
            Assert.Equal(BPCategory.Ideal, category);
        }

        // ... (keep your existing tests)

        // NEW FEATURE TESTS
        [Fact]
        [Trait("Category", "Unit")]
        public void BloodPressureHistory_AddRecord_ShouldStoreRecord()
        {
            // Arrange
            var history = new BloodPressureHistory();
            
            // Act
            history.AddRecord(120, 80, "TestUser");
            
            // Assert
            Assert.Equal(1, history.HistoryCount);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void BloodPressureHistory_GetHistory_ShouldReturnReadOnlyList()
        {
            // Arrange
            var history = new BloodPressureHistory();
            history.AddRecord(120, 80);
            
            // Act
            var records = history.GetHistory();
            
            // Assert
            Assert.NotNull(records);
            Assert.Single(records);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void BloodPressureHistory_ShouldKeepOnlyLast5Records()
        {
            // Arrange
            var history = new BloodPressureHistory();
            
            // Act - Add 6 records
            for (int i = 0; i < 6; i++)
            {
                history.AddRecord(100 + i, 70 + i);
            }
            
            // Assert - Should only keep last 5
            Assert.Equal(5, history.HistoryCount);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void BloodPressureRecord_ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var bp = new BloodPressure { Systolic = 120, Diastolic = 80 };
            var record = new BloodPressureRecord(bp, "TestUser");
            
            // Act
            var result = record.ToString();
            
            // Assert
            Assert.Contains("120/80", result);
            Assert.Contains("Ideal", result);
            Assert.Contains("TestUser", result);
        }

        [Fact]
        [Trait("Category", "BDD")]
        public void Given_NewBloodPressureReading_When_AddedToHistory_Then_HistoryShouldContainRecord()
        {
            // Given
            var history = new BloodPressureHistory();
            
            // When
            history.AddRecord(130, 85, "Doctor");
            
            // Then
            Assert.Equal(1, history.HistoryCount);
            var records = history.GetHistory();
            Assert.Equal(130, records[0].Systolic);
            Assert.Equal(85, records[0].Diastolic);
            Assert.Equal("Doctor", records[0].User);
        }
    }
}