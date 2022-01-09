using System;
using System.IO;

namespace DZ_8
{
    class Program
    {
        static void Main(string[] args)
        {
            string graph1 = "Graph1.txt";
            string graph2 = "Graph2.txt";
            string[] matr1;
            string[] matr2;
            matr1 = File.ReadAllLines(graph1);
            matr2 = File.ReadAllLines(graph2);

            bool Aftomorfizm = true;
            for (int i = 0; i < matr1.Length; i++)
                if (String.Compare(matr1[i], matr2[i]) != 0)
                {
                    Aftomorfizm = false;
                    break;
                }
            if (Aftomorfizm == false)
                Console.WriteLine("Преобразование является автоморфизмом.");
            else 
                Console.WriteLine("Преобразование не является автоморфизмом.");
        }
    }
}