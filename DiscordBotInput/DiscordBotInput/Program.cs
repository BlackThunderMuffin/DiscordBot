using System;

namespace DiscordBotInput
{
    
    class Program
    {
     
        static void Main(string[] args)
        {
            bool check = false;
            while (!check)
            {
                Console.WriteLine("Für OWO owo und für Custom c drücken.");
                string key = Console.ReadLine();

                //for OWO
                if (key == "owo")
                {
                    Console.WriteLine("Spezifiziere die Zeit die das Programm laufen soll in [min], wenn nichts spezifiziert dann 25min.");
                    string timeVerify = Console.ReadLine();
                    if (timeVerify.Length != 0)
                    {
                        bool worked = int.TryParse(timeVerify, out int resultVerify);
                        if (worked)
                        {
                            check = true;
                            resultVerify *= 60000;
                            Owo owo = new Owo(resultVerify);
                            owo.Start();
                        }
                        else
                        {
                            Console.WriteLine("Wrong Key! Try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong Key! Try again.");
                    }
                }
                else if (key == "c")
                {
                    //Text der gesagt werden soll
                    Console.WriteLine("Was soll geschrieben werden?");
                    string command = Console.ReadLine();


                    //intervall
                    Console.WriteLine("In welchem Intervall soll das geschrieben werden? [ms]");
                    string _intervall = Console.ReadLine();
                    int intervall = int.Parse(_intervall);

                    //dauer die das programm laufen soll
                    Console.WriteLine("Wie lange soll das Programm laufen? [min]");
                    string timeVerify = Console.ReadLine();
                    bool worked = int.TryParse(timeVerify, out int resultVerify);
                    if (worked)
                    {
                        check = true;
                        resultVerify *= 60000;
                        Custom c = new Custom(command, intervall, resultVerify);
                        c.Start();
                    }
                    else
                    {
                        Console.WriteLine("Wrong Key! Try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong Key! Try again.");
                }
                while (check)
                {

                }
            }
        }
    }
}
