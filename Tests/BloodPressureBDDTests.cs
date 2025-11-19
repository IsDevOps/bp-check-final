using Xunit;
using BPCalculator;

namespace BPCalculator.Tests
{
    public class BloodPressureBDDTests
    {
        [Fact]
        public void Given_IdealBloodPressure_When_CalculatingCategory_Then_ReturnsIdeal()
        {
            // Given
            var bp = new BloodPressure { Systolic = 110, Diastolic = 70 };
            
            // When
            var category = bp.Category;
            
            // Then
            Assert.Equal(BPCategory.Ideal, category);
        }

        [Fact]
        public void Given_HighSystolicAndIdealDiastolic_When_CalculatingCategory_Then_ReturnsHigh()
        {
            // Given
            var bp = new BloodPressure { Systolic = 150, Diastolic = 80 };
            
            // When
            var category = bp.Category;
            
            // Then
            Assert.Equal(BPCategory.High, category);
        }

        [Fact]
        public void Given_PreHighReadings_When_CalculatingCategory_Then_ReturnsPreHigh()
        {
            // Given
            var bp = new BloodPressure { Systolic = 135, Diastolic = 88 };
            
            // When
            var category = bp.Category;
            
            // Then
            Assert.Equal(BPCategory.PreHigh, category);
        }

        [Fact]
        public void Given_LowBloodPressure_When_CalculatingCategory_Then_ReturnsLow()
        {
            // Given
            var bp = new BloodPressure { Systolic = 85, Diastolic = 55 };
            
            // When
            var category = bp.Category;
            
            // Then
            Assert.Equal(BPCategory.Low, category);
        }
    }
}