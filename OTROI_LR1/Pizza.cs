using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("Pizza")]
public class Pizza
{
    [XmlElement("Name")]
    public string Name { get; set; }

    [XmlElement("Price")]
    public decimal Price { get; set; }

    [XmlElement("Size")]
    public string Size { get; set; }

    [XmlArray("Ingredients")]
    [XmlArrayItem("Ingredient")]
    public List<string> Ingredients { get; set; }
}
