using System;
using System.Text;
using static System.Console;

namespace WorkingWithEncoding
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Encodings");
            WriteLine("[1] ASCII");
            WriteLine("[2] UTF-7");
            WriteLine("[3] UTF-8");
            WriteLine("[4] UTF-16 (Unicode)");
            WriteLine("[5] UTF-32");
            WriteLine("[any other key] Default");

            // Выбор кодировки
            Write("Press a number to chose an encoding: ");
            ConsoleKey number = ReadKey(false).Key;
            WriteLine();
            WriteLine();

            Encoding encoder;
            switch (number)
            {
                case ConsoleKey.D1:
                    encoder = Encoding.ASCII;
                    break;
                case ConsoleKey.D2:
                    encoder = Encoding.UTF7;
                    break;
                case ConsoleKey.D3:
                    encoder = Encoding.UTF8;
                    break;
                case ConsoleKey.D4:
                    encoder = Encoding.Unicode;
                    break;
                case ConsoleKey.D5:
                    encoder = Encoding.UTF32;
                    break;
                default:
                    encoder = Encoding.Default;
                    break;
            }

            // Определение строки для кодирования
            string message = "A pint of milk is $1.99";

            // Кодирование строки в последовательность байтов
            byte[] encoded = encoder.GetBytes(message);

            // Проверка количества байтов, необходимого для кодирования
            WriteLine($"{encoder.GetType().Name} uses {encoded.Length} bytes.");
            WriteLine("Byte   Hex   Char");

            // Перечисление каждого байта
            foreach (byte b in encoded)
            {
                WriteLine($"{b,4} {b.ToString("X"),4} {(char)b,5}");
            }

            // Декодирование последовательности байтов обратно в строку и её вывод
            string decoded = encoder.GetString(encoded);
            WriteLine(decoded);

        }
    }
}
