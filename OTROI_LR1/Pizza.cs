using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

[XmlRoot("Pizza")]
public class Pizza
{
    private string xmlFilePath = "pizzas.xml";
    private const string namespaceUri = "http://www.example.com/pizza";

    [XmlAttribute("id")]
    public string id
    {
        get
        {
            if (string.IsNullOrEmpty(idValue))
            {
                return FindNextId(xmlFilePath).ToString();
            }
            else
            {
                return idValue;
            }
        }
        set { idValue = value; }
    }
    private string idValue;

    [XmlElement("Name")]
    public string Name { get; set; }

    [XmlElement("Price")]
    public decimal Price { get; set; }

    [XmlElement("Size")]
    public string Size { get; set; }

    [XmlArray("Ingredients")]
    [XmlArrayItem("Ingredient")]
    public List<string> Ingredients { get; set; }

    public Pizza()
    {
        if (!File.Exists(xmlFilePath))
        {
            var newDoc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XProcessingInstruction("xml-stylesheet", $"type=\"text/xsl\" href=\"pizzas.xslt\""),
            new XElement(XName.Get("Pizzas", namespaceUri))
                    );

            newDoc.Save(xmlFilePath);
        }
    }
    private static int FindNextId(string xmlFilePath)
    {
        const string namespaceUri = "http://www.example.com/pizza";
        XNamespace ns = namespaceUri;

        // Открываем файл с разрешением совместного доступа
        using (FileStream fs = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            var document = XDocument.Load(fs);

            var ids = document
                .Descendants(ns + "Pizza")
                .Attributes("id")
                .Select(attr => int.TryParse(attr.Value, out var id) ? id : 0);

            return ids.Any() ? ids.Max() + 1 : 1;
        }
    }

}
