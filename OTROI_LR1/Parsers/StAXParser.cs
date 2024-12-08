using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace OTROI_LR1.Parsers
{
    internal class StAXParser
    {
        static public void DeMarshaling()
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
                Pizza currentPizza = null;

                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.LocalName)
                        {
                            case "Pizza":
                                currentPizza = new Pizza
                                {
                                    Ingredients = new List<string>()
                                };
                                break;

                            case "Name":
                                currentPizza.Name = reader.ReadElementContentAsString();
                                break;

                            case "Price":
                                currentPizza.Price = reader.ReadElementContentAsDecimal();
                                break;

                            case "Size":
                                currentPizza.Size = reader.ReadElementContentAsString();
                                break;

                            case "Ingredient":
                                currentPizza.Ingredients.Add(reader.ReadElementContentAsString());
                                break;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == "Pizza")
                    {
                        pizzas.Add(currentPizza);
                    }
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
        static public void Marshaling(Pizza pizza)
        {
            const string fileName = "pizzas.xml";
            const string namespaceUri = "http://www.example.com/pizza";

            XDocument doc;

            // Проверяем, существует ли файл
            if (File.Exists(fileName))
            {
                // Загружаем существующий XML-файл
                doc = XDocument.Load(fileName);
            }
            else
            {
                // Создаем новый документ с корневым элементом
                doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement(XName.Get("Pizzas", namespaceUri)));
            }

            // Получаем корневой элемент
            XElement root = doc.Root;

            // Определяем максимальный id среди существующих пицц
            int maxId = root.Elements(XName.Get("Pizza", namespaceUri))
                            .Attributes("id")
                            .Select(attr => int.Parse(attr.Value))
                            .DefaultIfEmpty(0)
                            .Max();

            // Создаем новый элемент Pizza
            XElement newPizza = new XElement(XName.Get("Pizza", namespaceUri),
                new XAttribute("id", (maxId + 1).ToString()),
                new XElement(XName.Get("Name", namespaceUri), pizza.Name),
                new XElement(XName.Get("Price", namespaceUri), pizza.Price.ToString("F2").Replace(",", ".")),
                new XElement(XName.Get("Size", namespaceUri), pizza.Size),
                new XElement(XName.Get("Ingredients", namespaceUri),
                    pizza.Ingredients.Select(ingredient =>
                        new XElement(XName.Get("Ingredient", namespaceUri), ingredient)))
            );

            // Добавляем новую пиццу в корневой элемент
            root.Add(newPizza);

            // Сохраняем документ
            doc.Save(fileName);
        }

    }
}
