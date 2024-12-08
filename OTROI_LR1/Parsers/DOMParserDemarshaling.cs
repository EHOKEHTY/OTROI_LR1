using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace OTROI_LR1.Parsers
{
    internal class DOMParserDemarshaling
    {
        public static void AddPizza(Pizza pizza)
        {
            XmlDocument doc = new XmlDocument();

            if (File.Exists("pizzas.xml"))
            {
                doc.Load("pizzas.xml"); // Загружаем существующий файл
            }
            else
            {
                XmlElement root = doc.CreateElement("Pizzas", "http://www.example.com/pizza");
                doc.AppendChild(root);
            }

            // Находим максимальный id среди всех существующих пицц
            int maxId = 0;
            XmlNodeList pizzaNodes = doc.GetElementsByTagName("Pizza");
            foreach (XmlElement pizzaElementik in pizzaNodes)
            {
                if (pizzaElementik.HasAttribute("id"))
                {
                    int id = int.Parse(pizzaElementik.GetAttribute("id"));
                    if (id > maxId)
                    {
                        maxId = id;
                    }
                }
            }

            // Увеличиваем id на 1 для новой пиццы
            string newId = (maxId + 1).ToString();

            // Создаем новый элемент Pizza
            XmlElement pizzaElement = doc.CreateElement("Pizza", "http://www.example.com/pizza");

            // Устанавливаем атрибут id для новой пиццы
            pizzaElement.SetAttribute("id", newId);

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
            Console.WriteLine("Pizza added and XML document saved in pizzas.xml");
        }


    }

}
