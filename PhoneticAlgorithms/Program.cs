using System;

namespace PhoneticAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите слово:");
                string word = Console.ReadLine();
                word = PolyphonAlgorythm.Polyphone(word);
                Console.WriteLine("Код слова: " + word);
                Console.WriteLine("Нечеткий код: " + PolyphonAlgorythm.FuzzyPolyphone(word));
            }
        }
    }
}