using GenerationOutput.Helpers;
using GenerationOutput.Interface;
using GenerationOutput.Model;
using GenerationOutput.Utils;
using System.Xml.Serialization;

namespace GenerationOutput.Processor
{
    public class GenerationReportProcessor : IGenerationReportProcessor
    {
        private readonly FileSettings _settings;
        private readonly ReportCalculator _calculator;

        public GenerationReportProcessor(FileSettings settings,ReportCalculator calculator)
        {
            _settings = settings;
            _calculator = calculator;
        }
        public async void ProcessGenerationReport(string filePath)
        {
            try
            {
                var generationReport = SerializationHelper.DeserializeXmlFile<GenerationReport>(filePath);
                var generationOutput = _calculator.CalculateReport(generationReport);
                string outfileName = $"GenerationOutput_{DateTime.Now:yyyyMMddHHmmssffff}.xml";
                string outputFilepath = Path.Combine(_settings.OutputReportFilePath, outfileName);
                await SerializationHelper.SerializeXmlFileAsync(generationOutput, outputFilepath);
                Console.WriteLine($"Generate output xml and saved into :{outputFilepath} for the inputfile : {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during processing generation report ({filePath}) : {ex.Message}");
            }

        }
    }
}
