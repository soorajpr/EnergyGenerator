using System.Xml.Serialization;

namespace GenerationOutput.Model
{
    [XmlRoot("GenerationReport")]
    public class GenerationReport
    {
        [XmlElement("Wind")]
        public Wind Wind { get; set; }
        [XmlElement("Gas")]
        public Gas Gas { get; set; }
        [XmlElement("Coal")]
        public Coal Coal { get; set; }
    }

    [XmlRoot("Wind")]
    public class Wind
    {
        [XmlElement("WindGenerator")]
        public List<WindGenerator> WindGenerator { get; set; }
    }
    public class Generator
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Generation")]
        public Generation Generation { get; set; }
        public virtual GeneratorType GeneratorType { get; set; } 
        public virtual string Location { get; set; }
    }

    [XmlRoot("WindGenerator")]
    public class WindGenerator : Generator
    {
        public override GeneratorType GeneratorType => GeneratorType.Wind; 
        [XmlElement("Location")]
        public override string Location { get; set; }
    }

    [XmlRoot("Gas")]
    public class Gas
    {
        [XmlElement("GasGenerator")]
        public List<GasGenerator> GasGenerator { get; set; }
    }

    [XmlRoot("GasGenerator")]
    public class GasGenerator : Generator
    {
        public override GeneratorType GeneratorType => GeneratorType.Gas;
        [XmlElement("EmissionsRating")]
        public double EmissionsRating { get; set; }
    }

    [XmlRoot("Coal")]
    public class Coal
    {
        [XmlElement("CoalGenerator")]
        public List<CoalGenerator> CoalGenerator { get; set; }
    }

    [XmlRoot("CoalGenerator")]
    public class CoalGenerator : Generator
    {
        public override GeneratorType GeneratorType => GeneratorType.Coal;
        [XmlElement("TotalHeatInput")]
        public double TotalHeatInput { get; set; }
        [XmlElement("ActualNetGeneration")]
        public double ActualNetGeneration { get; set; }
        [XmlElement("EmissionsRating")]
        public double EmissionsRating { get; set; }
    }

    [XmlRoot("Generation")]
    public class Generation
    {
        [XmlElement("Day")]
        public List<Day> Day { get; set; }
    }

    [XmlRoot("Day")]
    public class Day
    {
        [XmlElement("Date")]
        public DateTime Date { get; set; }
        [XmlElement("Energy")]
        public double Energy { get; set; }
        [XmlElement("Price")]
        public double Price { get; set; }
    }
    public enum GeneratorType
    {
        Wind,
        Gas,
        Coal
    }
}