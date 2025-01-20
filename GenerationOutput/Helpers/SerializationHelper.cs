using Newtonsoft.Json;
using System.Xml.Serialization;

namespace GenerationOutput.Helpers
{
    public static class SerializationHelper
    {
        public static T DeserializeXmlFile<T>(string filePath)
        {
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during xml deserialization: {ex.Message}");
                throw;
            }
        }

        public static async Task SerializeXmlFileAsync<T>(T data, string filePath)
        {
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    await Task.Run(() => serializer.Serialize(stream, data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during xml Serialization: {ex.Message}");
                throw;
            }
        }

        public static T DeserializeJsonFile<T>(string filePath)
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    var json = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during json deserialization: {ex.Message}");
                throw;
            }
        }
    }
}
