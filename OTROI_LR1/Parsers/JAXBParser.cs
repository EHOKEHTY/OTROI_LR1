using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
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
            string xmlFilePath = "pizzas.xml";
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
            string xmlFilePath = "pizzas.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(Pizzas));
            Pizzas pizzas;

            if (File.Exists(xmlFilePath))
            {
                // Зчитати наявний XML файл
                using (FileStream fs = new FileStream(xmlFilePath, FileMode.Open))
                {
                    pizzas = (Pizzas)serializer.Deserialize(fs);
                }
            }
            else
            {
                // Створити новий список піц, якщо файл відсутній
                pizzas = new Pizzas { PizzaList = new List<Pizza>() };
            }

            // Додати нову піцу
            pizzas.PizzaList.Add(pizza);

            // Серіалізуємо XML у тимчасовий файл
            string tempFilePath = xmlFilePath + ".tmp";
            using (var writer = new StreamWriter(tempFilePath, false, Encoding.UTF8))
            {
                serializer.Serialize(writer, pizzas);
            }

            // Додаємо декларацію стилю у файл
            string stylesheetLine = "<?xml-stylesheet type=\"text/xsl\" href=\"pizzas.xslt\"?>";
            var lines = File.ReadAllLines(tempFilePath).ToList();
            lines.Insert(1, stylesheetLine); // Вставляємо після заголовку XML
            File.WriteAllLines(xmlFilePath, lines, Encoding.UTF8);

            // Видаляємо тимчасовий файл
            File.Delete(tempFilePath);
        }
    }
}