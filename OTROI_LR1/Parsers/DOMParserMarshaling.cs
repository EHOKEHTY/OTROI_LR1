using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace OTROI_LR1
{
    public class DOMParserMarshaling
    {
        public static void Parse()
        {
            string xmlFilePath = "D://Study//HW//НУРЕ//Пятый семестр//ОТРОИ//OTROI_LR1//OTROI_LR1//bin//Debug//pizzas.xml";
            string xsdFilePath = "D://Study//HW//НУРЕ//Пятый семестр//ОТРОИ//OTROI_LR1//OTROI_LR1//bin//Debug//pizzas.xsd";

            // Валидация XML-документа с XSD
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add("http://www.example.com/pizza", xsdFilePath);
            settings.ValidationType = ValidationType.Schema;

            List<Pizza> pizzas = new List<Pizza>();

            using (XmlReader reader = XmlReader.Create(xmlFilePath, settings))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);

                XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
                ns.AddNamespace("pizza", "http://www.example.com/pizza");

                foreach (XmlNode node in doc.SelectNodes("//pizza:Pizza", ns))
                {
                    var pizza = new Pizza
                    {
                        Name = node.SelectSingleNode("pizza:Name", ns)?.InnerText,
                        Price = decimal.Parse(node.SelectSingleNode("pizza:Price", ns)?.InnerText.Replace(".", ",")),
                        Size = node.SelectSingleNode("pizza:Size", ns)?.InnerText,
                        Ingredients = new List<string>()
                    };

                    foreach (XmlNode ingredientNode in node.SelectNodes("pizza:Ingredients/pizza:Ingredient", ns))
                    {
                        pizza.Ingredients.Add(ingredientNode.InnerText);
                    }

                    pizzas.Add(pizza);
                }
            }

            foreach (var pizza in pizzas)
            {
                Console.WriteLine($"{pizza.Name}: {pizza.Price} {pizza.Size}");
                foreach (var item in pizza.Ingredients)
                {
                    Console.Write(item + ", ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }


}
