using System.Xml.Serialization;

namespace GenerationOutput.Model
{
    [XmlRoot("GenerationOutput")]
    public class GenerationReportOutput
    {
        [XmlElement("Totals")]
        public Totals Totals { get; set; }
        [XmlElement("MaxEmissionGenerators")]
        public MaxEmissionGenerators MaxEmissionGenerators { get; set; }
        [XmlElement("ActualHeatRates")]
        public ActualHeatRates ActualHeatRates { get; set; }
    }

    [XmlRoot("Totals")]
    public class Totals
    {
        [XmlElement("Generator")]
        public List<OutPutGenerator> Generator { get; set; }
    }

    [XmlRoot("Generator")]
    public class OutPutGenerator
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Total")]
        public double Total { get; set; }
    }

    [XmlRoot("MaxEmissionGenerators")]
    public class MaxEmissionGenerators
    {
        [XmlElement("Day")]
        public List<OutputDay> Day { get; set; }
    }

    [XmlRoot(ElementName = "Day")]
    public class OutputDay
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Date")]
        public DateTime Date { get; set; }
        [XmlElement(ElementName = "Emission")]
        public double Emission { get; set; }
    }

    [XmlRoot("ActualHeatRates")]
    public class ActualHeatRates
    {
        [XmlElement("CoalGenerator")]
        public List<OutputCoalGenerator> CoalGenerator { get; set; }
    }

    [XmlRoot(ElementName = "CoalGenerator")]
    public class OutputCoalGenerator
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "HeatRate")]
        public double HeatRate { get; set; }
    }
}
