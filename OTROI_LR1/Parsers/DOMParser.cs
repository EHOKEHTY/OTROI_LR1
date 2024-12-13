using System;
using System.Collections.Generic;
using System.Xml;

namespace OTROI_LR1
{
    public class DOMParser
    {
        public static void DeMarshaling()
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

        public static void Marshaling(Pizza pizza)
        {
            XmlDocument doc = new XmlDocument();


            doc.Load("pizzas.xml"); // Загружаем существующий файл

            // Создаем новый элемент Pizza
            XmlElement pizzaElement = doc.CreateElement("Pizza", "http://www.example.com/pizza");

            // Устанавливаем атрибут id для новой пиццы
            pizzaElement.SetAttribute("id", pizza.id);

            // Создаем элементы внутри Pizza
            XmlElement name = doc.CreateElement("Name", "http://www.example.com/pizza");
            name.InnerText = pizza.Name;
            pizzaElement.AppendChild(name);

            XmlElement price = doc.CreateElement("Price", "http://www.example.com/pizza");
            price.InnerText = pizza.Price.ToString("F2").Replace(",", ".");
            pizzaElement.AppendChild(price);

            XmlElement size = doc.CreateElement("Size", "http://www.example.com/pizza");
            size.InnerText = pizza.Size;
            pizzaElement.AppendChild(size);

            XmlElement ingredients = doc.CreateElement("Ingredients", "http://www.example.com/pizza");
            foreach (var ingredient in pizza.Ingredients)
            {
                XmlElement ingredientElement = doc.CreateElement("Ingredient", "http://www.example.com/pizza");
                ingredientElement.InnerText = ingredient;
                ingredients.AppendChild(ingredientElement);
            }
            pizzaElement.AppendChild(ingredients);

            // Добавляем новый элемент Pizza в корневой элемент Pizzas
            XmlElement rootElement = doc.DocumentElement;
            rootElement.AppendChild(pizzaElement);

            // Сохраняем обновленный документ
            doc.Save("pizzas.xml");
        }
    }
}
