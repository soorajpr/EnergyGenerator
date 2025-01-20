using System.Xml.Serialization;

namespace GenerationOutput.Model
{
    [XmlRoot("ReferenceData")]
    public class ReferenceData
    {
        [XmlElement("Factors")]
        public Factors Factors { get; set; }
    }

    [XmlRoot("Factors")]
    public class Factors
    {
        [XmlElement("ValueFactor")]
        public ValueFactor ValueFactor { get; set; }
        [XmlElement("EmissionsFactor")]
        public EmissionsFactor EmissionsFactor { get; set; }
    }

    [XmlRoot("ValueFactor")]
    public class ValueFactor
    {
        [XmlElement("High")]
        public double High { get; set; }
        [XmlElement("Medium")]
        public double Medium { get; set; }
        [XmlElement("Low")]
        public double Low { get; set; }
    }

    [XmlRoot("EmissionsFactor")]
    public class EmissionsFactor
    {
        [XmlElement("High")]
        public double High { get; set; }
        [XmlElement("Medium")]
        public double Medium { get; set; }
        [XmlElement("Low")]
        public double Low { get; set; }
    }
}
