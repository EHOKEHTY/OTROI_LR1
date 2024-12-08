using System.IO;
using System.Xml.Serialization;

public static class XmlHelper<T>
{
    public static void Serialize(string filePath, T data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(fs, data);
        }
    }

    public static T Deserialize(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            return (T)serializer.Deserialize(fs);
        }
    }
}
