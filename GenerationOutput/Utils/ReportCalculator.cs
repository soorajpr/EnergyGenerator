using GenerationOutput.Helpers;
using GenerationOutput.Model;

namespace GenerationOutput.Utils
{
    public class ReportCalculator
    {
        public ReferenceData referenceData;
        public List<GeneratorFactor> generatorFactor;

        public ReportCalculator()
        {
            ReadGeneratorReferenceData();
        }

        public GenerationReportOutput CalculateReport(GenerationReport generationReport)
        {
            Console.WriteLine("Calculate and process generation report");
            List<Generator> generators = new List<Generator>();
            generators.AddRange(generationReport.Wind.WindGenerator);
            generators.AddRange(generationReport.Gas.GasGenerator);
            generators.AddRange(generationReport.Coal.CoalGenerator);
            return ProcessGenerators(generators);
        }

        private GenerationReportOutput ProcessGenerators<T>(IEnumerable<T> generators) where T : Generator
        {
            var generationOutput = new GenerationReportOutput();
            var generatorList = new List<OutPutGenerator>();
            var coalGeneratorList = new List<OutputCoalGenerator>();
            var dailyEmissionList = new List<OutputDay>();
            foreach (var generator in generators)
            {
                var generatorFactor = GetGeneratorFactor(generator);
                var emissionFactor = GetEmissionFactor(generatorFactor.EmissionFactor);
                var totalValue = generator.Generation.Day.Sum(x => x.Energy * x.Price) * GetValueFactor(generatorFactor.ValueFactor);
                generatorList.Add(new OutPutGenerator { Name = generator.Name, Total = totalValue });
                if (generator is GasGenerator gas)
                {
                    CalculateDailyEmission(dailyEmissionList, emissionFactor, gas.Name,gas.Generation.Day,gas.EmissionsRating);
                }
                else if (generator is CoalGenerator coal)
                {
                    var heatRate = coal.TotalHeatInput / coal.ActualNetGeneration;
                    coalGeneratorList.Add(new OutputCoalGenerator { Name = coal.Name, HeatRate = heatRate });

                    CalculateDailyEmission(dailyEmissionList, emissionFactor, coal.Name, coal.Generation.Day, coal.EmissionsRating);
                }
            }
            generationOutput.Totals = new Totals { Generator = generatorList };
            generationOutput.MaxEmissionGenerators = new MaxEmissionGenerators { Day = dailyEmissionList };

            if (coalGeneratorList.Any())
            {
                generationOutput.ActualHeatRates = new ActualHeatRates { CoalGenerator = coalGeneratorList };
            }
            return generationOutput;
        }

        private static void CalculateDailyEmission(List<OutputDay> dailyEmissionList, double emissionFactor, string generatorName, List<Day> days, double emissionsRating)
        {
            foreach (var daily in days)
            {
                var dailyEmission = daily.Energy * emissionsRating * emissionFactor;
                dailyEmissionList.Add(new OutputDay { Name = generatorName, Date = daily.Date, Emission = dailyEmission });
            }
        }

        private GeneratorFactor GetGeneratorFactor<T>(T generator) where T : Generator
        {
            return generatorFactor
                .First(x => x.Type == generator.GeneratorType && (generator.Location == null || x.Location == generator.Location));
            
        }

        public double GetEmissionFactor(string range)
        {
           return range switch
            {
                "Low" => referenceData.Factors.EmissionsFactor.Low,
                "Medium" => referenceData.Factors.EmissionsFactor.Medium,
                "High" => referenceData.Factors.EmissionsFactor.High,
                _ => 0
            };
        }
        public double GetValueFactor(string range)
        {
            return range switch
            {
                "Low" => referenceData.Factors.ValueFactor.Low,
                "Medium" => referenceData.Factors.ValueFactor.Medium,
                "High" => referenceData.Factors.ValueFactor.High,
                _ => 0
            };
        }

        public void ReadGeneratorReferenceData()
        {
            string referenceDatafilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReferenceData.xml");
            referenceData = SerializationHelper.DeserializeXmlFile<ReferenceData>(referenceDatafilePath);
            string generatorFactorfilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "generators.json");
            generatorFactor = SerializationHelper.DeserializeJsonFile<List<GeneratorFactor>>(generatorFactorfilePath);
        }
    }
}
