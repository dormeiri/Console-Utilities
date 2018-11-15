using System;
using System.Collections.Generic;

namespace ConsoleUtilities
{
    public static class ConsoleU
    {
        private const ConsoleColor DATA_COLOR = ConsoleColor.Green;
        private const ConsoleColor LOG_COLOR = ConsoleColor.Gray;
        private const ConsoleColor ERROR_COLOR = ConsoleColor.Red;
        private const ConsoleColor INPUT_COLOR = ConsoleColor.Yellow;
        private const ConsoleColor NORMAL_COLOR = ConsoleColor.White;
        private const string INVALID_YN_INPUT = "Invalid answer, please insert Y/N";
        public const string EXIT_MESSAGE = "Press any key to exit...";

        private static Queue<string> log = new Queue<string>();
        public static Queue<string> GetLog() => log;

        /// <summary>
        /// Insert a line into the log and show this line in the console.
        /// </summary>
        /// <param name="addedLog"></param>
        public static void Log(string addedLog)
        {
            Console.ForegroundColor = LOG_COLOR;
            Console.WriteLine(addedLog);
            log.Enqueue(addedLog);
            ResetColor();
        }
        /// <summary>
        /// Show <paramref name="data"/> in the console and add it to the log.
        /// </summary>
        /// <param name="dataDescription">Short description the data represent</param>
        /// <param name="data">The data which will be shown</param>
        public static void Data(string dataDescription, object data)
        {
            dataDescription = dataDescription + ": ";
            Console.Write(dataDescription);
            Console.ForegroundColor = DATA_COLOR;
            Console.WriteLine(data.ToString());
            log.Enqueue(dataDescription + data.ToString());
            ResetColor();
        }
        /// <summary>
        /// Show error message in the console and insert it into the log.
        /// </summary>
        /// <param name="message">Error message</param>
        public static void Error(string message)
        {
            Console.ForegroundColor = ERROR_COLOR;
            Console.WriteLine(message);
            log.Enqueue(message);
            ResetColor();
        }
        public static string ReadLine()
        {
            string input;
            Console.ForegroundColor = INPUT_COLOR;
            input = Console.ReadLine();
            log.Enqueue(input);
            ResetColor();
            return input;
        }
        /// <summary>
        /// Reset the console foreground color to <see cref="NORMAL_COLOR"/>
        /// </summary>
        public static void ResetColor()
        {
            Console.ResetColor();
            Console.ForegroundColor = NORMAL_COLOR;
        }

        /// <summary>
        /// <para>Create Y/N <paramref name="question"/> that will appear in the console.</para>
        /// <para>
        /// If the input is:
        /// <list type="bullet">
        /// <item>Y => invoke <paramref name="yesMethod"/>.</item>
        /// <item>N => invoke <paramref name="noMethod"/>.</item>
        /// <item>Invalid input => try again.</item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="question">The question that will appear to the user (" Y/N: " will appear automatically)</param>
        /// <param name="yesMethod">The method that will be invoked if the user will choose 'Y'</param>
        /// <param name="noMethod">The method that will be invoked if the user will choose 'N'</param>
        /// <returns>true if yesMethod invoked, false if noMethod invoked</returns>
        public static bool ConsoleQuestion(string question, Action yesMethod = null, Action noMethod = null)
        {
            question = question + " Y/N: ";
            Console.WriteLine(question);
            log.Enqueue(question);
        tryagain:
            Console.ForegroundColor = INPUT_COLOR;
            string input;
            input = Console.ReadLine();
            ResetColor();
            if (input == "Y" || input == "y" || input.ToUpper() == "YES")
            {
                yesMethod?.Invoke();
                return true;
            }
            else if (input == "N" || input == "n" || input.ToUpper() == "NO")
            {
                noMethod?.Invoke();
                return false;
            }
            Console.ForegroundColor = ERROR_COLOR;
            Console.WriteLine(INVALID_YN_INPUT);
            log.Enqueue(INVALID_YN_INPUT);
            ResetColor();
            goto tryagain;
        }
    }
}
