using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
namespace OTROI_LR1.Parsers
{
    internal class SAXParser
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
                string currentElement = string.Empty;

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        currentElement = reader.LocalName;

                        if (currentElement == "Pizza")
                        {
                            currentPizza = new Pizza
                            {

                                Ingredients = new List<string>()
                            };
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.Text)
                    {
                        if (currentElement == "Name")
                        {
                            currentPizza.Name = reader.Value;
                        }
                        else if (currentElement == "Price")
                        {
                            currentPizza.Price = decimal.Parse(reader.Value.Replace(".", ","));
                        }
                        else if (currentElement == "Size")
                        {
                            currentPizza.Size = reader.Value;
                        }
                        else if (currentElement == "Ingredient")
                        {
                            currentPizza.Ingredients.Add(reader.Value);
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
                    Console.Write(item + " ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        static public void Marshaling(Pizza pizza)
        {
            const string fileName = "pizzas.xml";
            const string namespaceUri = "http://www.example.com/pizza";


            // Загружаем существующий XML-файл
            XDocument doc = XDocument.Load(fileName);

            // Получаем корневой элемент
            XElement root = doc.Root;

            // Создаем новый элемент Pizza
            XElement newPizza = new XElement(XName.Get("Pizza", namespaceUri),
                new XAttribute("id", pizza.id),
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