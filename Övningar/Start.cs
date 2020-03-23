using System;
using System.Collections.Generic;
using System.Linq;

namespace Övningar
{
    partial class Program
    {
        class Start
        {
            public string Choice { get; set; }
            public List<Recipe> Recipelist = new List<Recipe>()
            {
                    new Recipe() {ID = 1, Name = "Muffin", EstimatedTime = "45 min", RecipeType = "Efterrätt" },
                    new Recipe() {ID = 2, Name = "Tårta", EstimatedTime = "60 min", RecipeType = "Efterrätt" },
                    new Recipe() {ID = 3, Name = "Hamburgare", EstimatedTime = "45 min", RecipeType = "Lunch" }
            };
            public void Mainpage()
            {
                Console.SetCursorPosition(50, 0);
                Console.WriteLine("Våra recept");
                var query = from e in Recipelist
                            select e;
                foreach (var item in query)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(item.ID + ". " + item.Name);
                    Console.ResetColor();
                }
                Console.WriteLine("Tryck på 4 för att gå till 'Om oss'");
                Console.WriteLine("");
                Console.WriteLine("Tryck på siffran på receptet för se detaljerna");
                Choice = Console.ReadLine();
            }
            public void Övning1()
            {
                Övning2 ö2 = new Övning2();
                do
                {
                    Mainpage();

                    do
                    {
                        int number;
                        while (!int.TryParse(Choice, out number) && Choice != "H")
                        {
                            Console.WriteLine("Felaktigt inmatning!");
                            Choice = Console.ReadLine();
                        }

                        if (number == 4)
                        {
                            Console.Clear();
                            Console.WriteLine("Information");
                            Console.WriteLine("Detta är en sida där du kan se våra recept");
                            Console.WriteLine("Tryck på valfri knapp för att gå till hemsidan");
                            Console.ReadKey();
                            Console.Clear();
                            Mainpage();

                            while (!int.TryParse(Choice, out number) && Choice != "H")
                            {
                                Console.WriteLine("Felaktigt inmatning!");
                                Choice = Console.ReadLine();
                            }
                        }

                        Console.Clear();
                        Console.Write("Detaljer på receptet ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(Recipelist[number - 1].Name);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("- Tillagningstid: " + Recipelist[number - 1].EstimatedTime);
                        Console.WriteLine("- Typ: " + Recipelist[number - 1].RecipeType);
                        Console.ResetColor();
                        ö2.LineDivide();

                        var query2 = from w in Recipelist
                                     where w.ID != number
                                     select w;

                        foreach (var item in query2)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(item.ID + " " + item.Name);
                            Console.ResetColor();
                        }
                        Console.WriteLine("Tryck på ett annat nummer för att gå till ett annat recept");
                        Console.WriteLine("Tryck på 'h' för att gå till hemsidan");
                        Choice = Console.ReadLine().ToUpper();

                        Console.Clear();
                    } while (Choice != "H");

                } while (Choice == "H");
            }
        }
    }
}
