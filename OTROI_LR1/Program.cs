using OTROI_LR1;
using OTROI_LR1.Parsers;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {

        JAXBParser.Marshaling(new Pizza
        {
            Name = "Бєкон",
            Price = 20.99m,
            Size = "Маленька",
            Ingredients = new List<string> { "Тісто", "Бекон" }
        });

        //DOM    
        DOMParser.Marshaling(new Pizza
        {
            Name = "4 Сири",
            Price = 200.00m,
            Size = "Екстра",
            Ingredients = new List<string> { "Багато сира", "Ще багато сира", "Олівка" }
        });
        //DOMParser.DeMarshaling();

        //SAX    
        SAXParser.Marshaling(new Pizza
        {
            Name = "Кабанчик",
            Price = 180.40m,
            Size = "Велика",
            Ingredients = new List<string> { "Копченості", "Моцарела", "Яблука" }
        });
        //SAXParser.DeMarshaling();

        //StAX
        StAXParser.Marshaling(new Pizza
        {
            Name = "Гори Московія",
            Price = 130.00m,
            Size = "Екстра",
            Ingredients = new List<string> { "Чілі", "Табаско", "Жарене м'ясо" }
        });
        //StAXParser.DeMarshaling();

        JAXBParser.Marshaling(new Pizza
        {
            Name = "Теріякі",
            Price = 199.99m,
            Size = "Маленька",
            Ingredients = new List<string> { "Теріякі", "Бекон", "Пармізан" }
        });
        JAXBParser.DeMarshaling();
        Console.ReadLine();
    }
}