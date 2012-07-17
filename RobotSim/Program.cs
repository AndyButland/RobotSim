using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotSim
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayWelcome();

            var driver = new RobotDriver(new Robot());

            while (true)
            {
                string command = PromptForCommand();
                if (command.ToUpper() == "EXIT" || command.ToUpper() == "QUIT")
                {
                    Environment.Exit(0);
                }
                Console.WriteLine(driver.Command(command));
            }
        }

        private static void DisplayWelcome()
        {
            Console.WriteLine("Robit Sim");
            Console.WriteLine("---------");
            Console.WriteLine("");
        }

        private static string PromptForCommand()
        {
            Console.WriteLine("#: ");
            return Console.ReadLine();
        }


    }
}
