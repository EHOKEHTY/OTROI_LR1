using OTROI_LR1;
using OTROI_LR1.Parsers;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

class Program
{
    static void Main()
    {
        /*//XmlDocument xmlDocument = new XmlDocument();
        //xmlDocument.Load("pizzas.xml");

        //xmlDocument.Schemas.Add(null, "pizzas.xsd");
        //xmlDocument.Validate(ValidationEventHandler);
        //Console.WriteLine("XML validation is late.");
        //Console.ReadLine();

        //var pizzas = new List<Pizza>
        //{
        //    new Pizza
        //    {
        //        Name = "Маргарита",
        //        Price = 100.00m,
        //        Size = "Medium",
        //        Ingredients = new List<string> { "Сыр", "Томаты" }
        //    },
        //    new Pizza
        //    {
        //        Name = "Пепперони",
        //        Price = 120.50m,
        //        Size = "Large",
        //        Ingredients = new List<string> { "Сыр", "Пепперони" }
        //    }
        //};

        // Сериализация
        //XmlHelper<List<Pizza>>.Serialize("pizzas.xml", pizzas);
        //Console.WriteLine("Данные сериализованы!");

        //// Десериализация
        //var deserializedPizzas = XmlHelper<List<Pizza>>.Deserialize("pizzas.xml");
        //foreach (var pizza in deserializedPizzas)
        //{
        //    Console.WriteLine($"Название: {pizza.Name}, Цена: {pizza.Price}, Размер: {pizza.Size}");
        //}

    }

    private static void ValidationEventHandler(object sender, ValidationEventArgs e)
    {
        if (e.Severity == XmlSeverityType.Error)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        else
        {
            Console.WriteLine($"Warning: {e.Message}");
        }
    }*/
        DOMParserDemarshaling.AddPizza(new Pizza
        {
            Name = "4 Сири",
            Price = 200.00m,
            Size = "Екстра",
            Ingredients = new List<string> { "Багато сира", "Ще багато сира", "Олівка" }
        });
        DOMParserMarshaling.Parse();

        Console.ReadLine();
    }
}