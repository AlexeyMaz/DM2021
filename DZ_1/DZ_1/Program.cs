using System;
using System.IO;

namespace DZ_1
{
    public class CombObjects
    {
        static int n;
        static int k;
        static char[] CombObj;
        static char[] alphabet;

        public CombObjects()
        {
            Console.Write("Введите размер алфавита: ");
            n = int.Parse(Console.ReadLine());
            alphabet = new char[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write("Введите символ алфавита: ");
                alphabet[i] = char.Parse(Console.ReadLine());
            }
            Console.WriteLine();
        }
        public class Variations_with_Repetition//размещения с повторениями
        {
            public Variations_with_Repetition(int k)
            {
                CombObj = new char[k];
                CombObjects.k = k;
            }
            public void Process()
            {
                for (int i = 0; i < k; i++)
                    CombObj[i] = alphabet[0];

                using (StreamWriter writer = new StreamWriter(@"C:\Users\almaz\source\repos\DM2021\DZ_1\1.1.txt"))
                {
                    writer.Write("n = " + n + ", k = " + k + ", alphabet = { ");
                    for (int i = 0; i < n; i++)
                        writer.Write(alphabet[i] + " ");
                    writer.WriteLine("}");

                    while (HasNextCombObj())
                    {
                        for (int i = 0; i < k; i++)
                            writer.Write(CombObj[i]);

                        writer.WriteLine();
                        NextCombObj();
                    }
                    for (int i = 0; i < k; i++)
                        writer.Write(CombObj[i]);
                }
            }
            private bool HasNextCombObj()//есть ли следующий объект
            {
                int kol = 0;
                for (int i = 0; i < k; i++)
                    if (CombObj[i] == alphabet[n - 1]) kol++;
                return (kol != k);
            }
            public void NextCombObj()//генерация следующего объекта
            {
                for (int i = k - 1; i > -1; i--)
                {
                    if (CombObj[i] == alphabet[n - 1])
                    {
                        CombObj[i] = alphabet[0];
                        continue;
                    }
                    CombObj[i]++;
                    break;
                }
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            CombObjects CombObject = new CombObjects();

            Console.Write("Введите номер задачи, которую необходимо выполнить; введите 0 для выхода: ");
            int TaskNum = int.Parse(Console.ReadLine());
            int k;
            switch (TaskNum)
            {
                case (0):
                    break;
                case (1)://размещения с повторениями
                    Console.Write("Введите длину слова: ");
                    k = int.Parse(Console.ReadLine());
                    CombObjects.Variations_with_Repetition n1 = new CombObjects.Variations_with_Repetition(k);
                    n1.Process();
                    Console.WriteLine("Задача выполнена");
                    break;
                case (2)://перестановки
                    Console.WriteLine("Задача находится в разработке");
                    break;
                case (3)://размещения по k
                    Console.WriteLine("Задача находится в разработке");
                    break;
                case (4)://подмножества
                    Console.WriteLine("Задача находится в разработке");
                    break;
                case (5)://сочетания по k
                    Console.WriteLine("Задача находится в разработке");
                    break;
                case (6)://сочетания с повторениями
                    Console.WriteLine("Задача находится в разработке");
                    break;
            }
        }
    }
}