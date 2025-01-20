using GenerationOutput.Interface;
using GenerationOutput.Model;
using GenerationOutput.Processor;
using GenerationOutput.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

FileSettings fileSettings = null;
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        fileSettings = context.Configuration.Get<FileSettings>();
        services.AddSingleton(fileSettings);
        services.AddSingleton<ReportCalculator>();
        services.AddTransient<IGenerationReportProcessor, GenerationReportProcessor>();
    })
    .Build();
ValidateFilePath(fileSettings);
SetFileSystemWatcher(fileSettings.InputReportFilePath);

void SetFileSystemWatcher(string InputReportFilePath)
{
    string directoryPath = Path.GetDirectoryName(InputReportFilePath);
    using var watcher = new FileSystemWatcher(directoryPath)
    {
        Filter = "*.xml",
        NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
    };

    watcher.Created += OnNewFileDetected;

    // Enable the watcher
    watcher.EnableRaisingEvents = true;

    Console.WriteLine(($"Drop generator input file in the path :{directoryPath}"));
    Console.ReadLine();
}

void OnNewFileDetected(object sender, FileSystemEventArgs e)
{
    Console.WriteLine($"New file detected: {e.FullPath}");

    try
    {
        var processor = host.Services.GetRequiredService<IGenerationReportProcessor>();
        processor.ProcessGenerationReport(e.FullPath);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error processing file: {ex.Message}");
    }
}
void ValidateFilePath(FileSettings fileSettings)
{
    var InputReportFilePath = fileSettings.InputReportFilePath;
    var OutputReportFilePath = fileSettings.OutputReportFilePath;
    Console.WriteLine($"Validating input filepath: {InputReportFilePath} and output filepath: {OutputReportFilePath}");
    if (!Path.Exists(InputReportFilePath) || !Path.Exists(OutputReportFilePath))
    {
        Console.Error.WriteLine("Invalid file path detected.");
        Environment.Exit(1);
    }
}