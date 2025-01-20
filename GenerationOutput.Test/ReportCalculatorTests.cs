using GenerationOutput.Helpers;
using GenerationOutput.Model;
using GenerationOutput.Utils;

namespace GenerationOutput.Test
{
    public class ReportCalculatorTests
    {
        private readonly ReportCalculator _reportCalculator;

        public ReportCalculatorTests()
        {
            _reportCalculator = new ReportCalculator();
        }

        [Fact]
        public void TestCalculateReport()
        {
            // Arrange
            var generationReport = new GenerationReport
            {
                Wind = new Wind { WindGenerator = new List<WindGenerator> { new WindGenerator { Name = "Wind[Offshore]", Generation = new Generation { Day = new List<Day> { new Day { Date = DateTime.Now, Energy = 100, Price = 20 } } } } } },
                Gas = new Gas { GasGenerator = new List<GasGenerator> { new GasGenerator { Name = "Gas[1]", Generation = new Generation { Day = new List<Day> { new Day { Date = DateTime.Now, Energy = 259, Price = 15 } } }, EmissionsRating = 0.038 } } },
                Coal = new Coal { CoalGenerator = new List<CoalGenerator> { new CoalGenerator { Name = "Coal[1]", Generation = new Generation { Day = new List<Day> { new Day { Date = DateTime.Now, Energy = 350, Price = 10 } } }, EmissionsRating = 0.482, TotalHeatInput = 11.815, ActualNetGeneration = 11.815 } } }
            };

            // Act
            var result = _reportCalculator.CalculateReport(generationReport);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Totals.Generator);
            Assert.NotEmpty(result.MaxEmissionGenerators.Day);
            Assert.NotEmpty(result.ActualHeatRates.CoalGenerator);
        }

        [Fact]
        public void TestGetEmissionFactor()
        {
            // Arrange
            var range = "Low";
            _reportCalculator.referenceData = new ReferenceData { Factors = new Factors { EmissionsFactor = new EmissionsFactor { Low = 0.312, Medium = 0.562, High = 0.812 } } };

            // Act
            var result = _reportCalculator.GetEmissionFactor(range);

            // Assert
            Assert.Equal(0.312, result);
        }

        [Fact]
        public void TestGetValueFactor()
        {
            // Arrange
            var range = "High";
            _reportCalculator.referenceData = new ReferenceData { Factors = new Factors { ValueFactor = new ValueFactor { Low = 0.265, Medium = 0.696, High = 0.946 } } };

            // Act
            var result = _reportCalculator.GetValueFactor(range);

            // Assert
            Assert.Equal(0.946, result);
        }

        [Fact]
        public void TestReadGeneratorReferenceData()
        {
            // Act
            _reportCalculator.ReadGeneratorReferenceData();

            // Assert
            Assert.NotNull(_reportCalculator.referenceData);
            Assert.NotNull(_reportCalculator.generatorFactor);
        }

    }
}
