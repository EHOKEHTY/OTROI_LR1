using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace OTROI_LR1.Parsers
{
    public class JAXBParser
    {
        [XmlRoot("Pizzas", Namespace = "http://www.example.com/pizza")]
        public class Pizzas
        {
            [XmlElement("Pizza")]
            public List<Pizza> PizzaList { get; set; }
        }
        static public void DeMarshaling()
        {
            string xmlFilePath = "D://Study//HW//НУРЕ//Пятый семестр//ОТРОИ//OTROI_LR1//OTROI_LR1//bin//Debug//pizzas.xml";
            var serializer = new XmlSerializer(typeof(Pizzas));

            using (FileStream fs = new FileStream(xmlFilePath, FileMode.Open))
            {
                var pizzas = (Pizzas)serializer.Deserialize(fs);

                foreach (var pizza in pizzas.PizzaList)
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
        static public void Marshaling(Pizza pizza)
        {
            string xmlFilePath = "D://Study//HW//НУРЕ//Пятый семестр//ОТРОИ//OTROI_LR1//OTROI_LR1//bin//Debug//pizzas.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(Pizzas));
            Pizzas pizzas;

            if (File.Exists(xmlFilePath))
            {
                // Загрузить существующий XML файл
                using (FileStream fs = new FileStream(xmlFilePath, FileMode.Open))
                {
                    pizzas = (Pizzas)serializer.Deserialize(fs);
                }
            }
            else
            {
                // Создать новый список пицц, если файл отсутствует
                pizzas = new Pizzas { PizzaList = new List<Pizza>() };
            }

            // Добавить новую пиццу
            pizzas.PizzaList.Add(pizza);

            // Перезаписать файл с обновленными данными
            using (FileStream fs = new FileStream(xmlFilePath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, pizzas);
            }
        }
    }
}
