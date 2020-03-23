using System;
using System.Collections.Generic;
using System.Linq;

namespace Övningar
{
    class Övning2
    {
        public List<User> Userlist = new List<User>();
        public List<Sport> Sportslist;
        public void AddDataToSportTable()
        {
            using (LabbContext c = new LabbContext())
            {
                Sport s1 = new Sport() { Name = "Fotboll", Votes = 0 };
                Sport s2 = new Sport() { Name = "Hockey", Votes = 0 };
                Sport s3 = new Sport() { Name = "Basket", Votes = 0 };
                Sport s4 = new Sport() { Name = "Bandy", Votes = 0 };
                Sport s5 = new Sport() { Name = "Annan aktivetet", Votes = 0 };

                if (!c.Sports.Any(S => s1.Name == "Fotboll"))
                {
                    c.Sports.Add(s1);
                    c.Sports.Add(s2);
                    c.Sports.Add(s3);
                    c.Sports.Add(s4);
                    c.Sports.Add(s5);
                    c.SaveChanges();
                }
            }
        }
        public void ShowUser()
        {
            using (LabbContext c = new LabbContext())
            {
                var query = from e in c.Users
                            select e;

                foreach (var item in query)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{"ID:",-10} {"Namn:",-15} {"Ålder:",-10}");
                    Console.ResetColor();
                    Console.WriteLine($"{item.ID,-10} {item.Name,-15} {item.Age,-10}");
                    LineDivide();
                }
            }
        }
        public void LineDivide()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.ResetColor();
        }
        public void Begin()
        {
            Övning2 ö = new Övning2();

            Console.WriteLine("Tryck på 1 för att skapa användare");
            Console.WriteLine("Tryck på 2 för att rösta");
            Console.WriteLine("Tryck på 3 för att se alla som begått brott");
            Console.WriteLine("Tryck på 4 för att se resultaten (antal röster)");
            Console.WriteLine("Tryck på 5 för att radera en Användare (rösten försvinner om hen hade röstat)");
            ö.UserDecision();
        }
        public void UserDecision()
        {
            Övning2 ö = new Övning2();
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
                ö.Skapa();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("");
                Console.WriteLine("Tryck på valfri knapp för att fortätta");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                ö.Begin();
            }
            else if (choice == 2)
            {
                Rösta();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("");
                Console.WriteLine("Tryck på valfri knapp för att fortätta");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                ö.Begin();
            }
            else if (choice == 3)
            {
                Crime();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("");
                Console.WriteLine("Tryck på valfri knapp för att fortätta");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                ö.Begin();
            }
            else if (choice == 4)
            {
                Result();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("");
                Console.WriteLine("Tryck på valfri knapp för att fortätta");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                ö.Begin();
            }
            else if (choice == 5)
            {
                Console.Clear();
                ShowUser();
                Console.WriteLine("Skriv användarens ID du vill radera");
                int removeID = Convert.ToInt32(Console.ReadLine());

                Remove(removeID);
                Console.WriteLine("Tryck på valfri knapp för att fortätta");
                Console.ReadKey();
                Console.Clear();
                ö.Begin();
            }
        }
        public void Skapa()
        {
            Console.Clear();
            using (LabbContext c = new LabbContext())
            {
                User u = new User();
                Random rnd = new Random();

                Console.WriteLine("Skriv in ett namn");
                u.Name = Console.ReadLine();

                Console.WriteLine("Skriv in ålder");
                u.Age = Convert.ToInt32(Console.ReadLine());

                u.CrimeCommitted = rnd.Next(2) == 1;

                if (u.CrimeCommitted == false)
                {
                    u.CrimeDate = null;
                }
                c.Users.Add(u);
                c.SaveChanges();

                Console.WriteLine("");
                Console.WriteLine("Användare sparad!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("ID: " + u.ID);
                Console.WriteLine("Namn " + u.Name);
                Console.WriteLine("Brott: " + u.CrimeCommitted);
                Console.ResetColor();
            }
        }
        public void Rösta()
        {
            Console.Clear();
            using (LabbContext c = new LabbContext())
            {
                AddDataToSportTable();
                ShowUser();

                Console.WriteLine("Vilken användare ska rösta? (Skriv in dens ID)");
                int pick = Convert.ToInt32(Console.ReadLine());

                var check = from e in c.Users
                            where e.ID == pick
                            select e;

                bool criminal = check.ToList()[0].CrimeCommitted;
                int checkAge = check.ToList()[0].Age;
                int? checkvote = check.ToList()[0].SportID;


                if (criminal == true || checkAge < 18)
                {
                    Console.WriteLine("Oops, personen har begått ett brott/har ej fyll 18 och får inte rösta!");
                    Console.WriteLine("Tryck på valfri knapp för att fortsätta");
                    Console.ReadKey();
                    Console.Clear();
                    Begin();
                }

                Console.Clear();
                var showsports = from s in c.Sports
                                 select s;

                foreach (var item in showsports)
                {
                    Console.WriteLine(item.ID + " " + item.Name);
                }

                Console.WriteLine("Vad vill du rösta på? (Skriv in siffran)");
                int vote = int.Parse(Console.ReadLine());

                var addvotes = from a in c.Sports
                               where a.ID == vote
                               select a;


                if (checkvote != null)
                {
                    Console.WriteLine("Opps, personen har redan röstat, en person kan endast rösta EN gång.");
                    Console.WriteLine("Tryck på knapp för att fortsätta");
                    Console.ReadKey();
                    Begin();
                }
                else if (checkvote == null)
                {
                    foreach (var item in check)
                    {
                        item.SportID = vote;
                    }
                }

                foreach (var item in addvotes)
                {
                    item.Votes += 1;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Din röst är registrerad!");
                Console.ResetColor();
                c.SaveChanges();
            }
        }
        public void Crime()
        {
            Console.Clear();
            using (LabbContext c = new LabbContext())
            {

                var query = from e in c.Users
                            where e.CrimeCommitted == true
                            orderby e.CrimeDate descending
                            select e;

                if (query.Count() == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Inga brottslingar finns i registret");
                    Console.ResetColor();
                }
                else
                {
                    foreach (var item in query)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(item.ID + " " + item.Name + " " + item.Age + " " + item.CrimeDate);
                        Console.ResetColor();
                    }
                }
            }
        }
        public void Result()
        {
            Console.Clear();
            using (LabbContext c = new LabbContext())
            {
                var Viewresult = from v in c.Sports
                                 select v;

                foreach (var item in Viewresult)
                {
                    Console.WriteLine(item.Name + " - " + item.Votes + " röster");
                }
            }
        }
        public void Remove(int RemoveID)
        {
            using (LabbContext c = new LabbContext())
            {
                var query = (from e in c.Users
                             where e.ID == RemoveID
                             select e).FirstOrDefault();

                int? uservote = query.SportID;

                var removevote = from rv in c.Sports
                                 where rv.ID == uservote
                                 select rv;

                foreach (var item in removevote)
                {
                    item.Votes -= 1;
                }
                c.Users.Remove(query);
                c.SaveChanges();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Användare raderad!");
            Console.ResetColor();
        }
    }
}
