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
        static void Swap(int i, int j)
        {
            char s = CombObj[i];
            CombObj[i] = CombObj[j];
            CombObj[j] = s;
        }
        static void PrintHeader(StreamWriter writer, bool inc_k)
        {
            if (inc_k)
                writer.Write("n = " + n + ", k = " + k + ", alphabet = {");
            else
                writer.Write("n = " + n + ", alphabet = {");
            for (int i = 0; i < n; i++)
            {
                if (i == n - 1) writer.Write(alphabet[i]);
                else writer.Write(alphabet[i] + ", ");
            }
            writer.WriteLine("}"); writer.WriteLine();
        }
        static void PrintCombObject(StreamWriter writer, int kk)
        {
            for (int i = 0; i < kk; i++)
            {
                if (i == 0) writer.Write("[");
                if (i == kk - 1) writer.Write(CombObj[i] + "]");
                else writer.Write(CombObj[i] + ", ");
            }
            writer.WriteLine();
        }
        public class Variations_with_Repetitions//размещения с повторениями
        {
            public Variations_with_Repetitions(int k)
            {
                CombObj = new char[k];
                CombObjects.k = k;
            }
            public void Process()
            {
                for (int i = 0; i < k; i++)
                    CombObj[i] = alphabet[0];

                using (StreamWriter writer = new StreamWriter(@"C:\Users\almaz\source\repos\DM2021\DZ_1\Variations_with_Repetitions.txt"))
                {
                    PrintHeader(writer, true);
                    while (HasNextCombObj())
                    {
                        PrintCombObject(writer, k);
                        NextCombObj();
                    }
                    PrintCombObject(writer, k);
                }
            }
            private bool HasNextCombObj()//есть ли следующий объект
            {
                int kol = 0;
                for (int i = 0; i < k; i++)
                    if (CombObj[i] == alphabet[n - 1]) kol++;
                return (kol != k);
            }
            private void NextCombObj()//генерация следующего объекта
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
        public class Permutations//перестановки
        {
            public Permutations()
            {
                CombObj = new char[n];
            }
            public void Process()
            {
                for (int i = 0; i < n; i++)
                    CombObj[i] = alphabet[i];

                using (StreamWriter writer = new StreamWriter(@"C:\Users\almaz\source\repos\DM2021\DZ_1\Permutations.txt"))
                {
                    PrintHeader(writer, false);
                    PrintCombObject(writer, n);

                    while (NextCombObj())
                        PrintCombObject(writer, n);
                }
            }
            private bool NextCombObj()
            {
                int i = n - 2;
                while (i > -1 && CombObj[i] > CombObj[i + 1])
                    i--;

                if (i == -1)
                    return false;//все перестановки сгенерированы

                int j;
                for (j = i + 1, k = n - 1; j < k; j++, k--)
                    Swap(k, j);
                j = i + 1;
                while (CombObj[j] < CombObj[i])
                    j++;
                Swap(j, i);

                return true;//генерируем дальше
            }
        }
        public class K_Variations//размещения по k
        {
            public K_Variations(int k)
            {
                CombObj = new char[n];
                CombObjects.k = k;
            }
            public void Process()
            {
                for (int i = 0; i < n; i++)
                    CombObj[i] = alphabet[i];

                using (StreamWriter writer = new StreamWriter(@"C:\Users\almaz\source\repos\DM2021\DZ_1\K_Variations.txt"))
                {
                    PrintHeader(writer, true);
                    PrintCombObject(writer, k);

                    while (NextCombObj())
                        PrintCombObject(writer, k);
                }
            }
            private bool NextCombObj()
            {
                int j;
                do //повторяем пока не будет найдено следующее размещение
                {
                    j = n - 2;
                    while (j != -1 && CombObj[j] >= CombObj[j + 1])
                        j--;

                    if (j == -1)
                        return false;//все размещения сгенерированы

                    int k = n - 1;
                    while (CombObj[j] >= CombObj[k]) k--;
                    Swap(j, k);

                    int l = j + 1, r = n - 1;
                    while (l < r)
                        Swap(l++, r--);
                } while (j > k - 1);

                return true;//генерируем дальше
            }
        }
        public class K_Combinations//сочетания по k
        {
            public K_Combinations(int k)
            {
                CombObj = new char[k];
                CombObjects.k = k;
            }
            public virtual void Process()
            {
                for (int i = 0; i < k; i++)
                    CombObj[i] = alphabet[i];

                using (StreamWriter writer = new StreamWriter(@"C:\Users\almaz\source\repos\DM2021\DZ_1\K_Combinations.txt"))
                {
                    PrintHeader(writer, true);
                    PrintCombObject(writer, k);

                    while (NextCombObj(k))
                        PrintCombObject(writer, k);
                }
            }
            protected bool NextCombObj(int p)//генерация следующего объекта
            {
                if ((int)CombObj[0] >= 48 && (int)CombObj[0] <= 57)
                {
                    for (int i = p - 1; i > -1; i--)
                        if (CombObj[i] - 48 < n - p + i + 1)
                        {
                            CombObj[i]++;
                            for (int j = i + 1; j < p; ++j)
                            {
                                CombObj[j] = CombObj[j - 1];
                                CombObj[j]++;
                            }
                            return true;//генерируем дальше
                        }
                    return false;//все сочетания сгенерированы
                }
                else if ((int)CombObj[0] >= 97)
                {
                    for (int i = p - 1; i > -1; i--)
                        if (CombObj[i] - 97 < n - p + i)
                        {
                            CombObj[i]++;
                            for (int j = i + 1; j < p; ++j)
                            {
                                CombObj[j] = CombObj[j - 1];
                                CombObj[j]++;
                            }
                            return true;//генерируем дальше
                        }
                    return false;//все сочетания сгенерированы
                }
                return false;
            }
        }
        public class Subsets/*подмножества*/ : K_Combinations
        {
            public Subsets() : base(n)
            { }
            public override void Process()
            {
                using (StreamWriter writer = new StreamWriter(@"C:\Users\almaz\source\repos\DM2021\DZ_1\Subsets.txt"))
                {
                    PrintHeader(writer, false);
                    writer.Write("[ ]");

                    for (int c = 0; c < n + 1; c++)
                    {
                        for (int i = 0; i < c; i++)
                            CombObj[i] = alphabet[i];
                        PrintCombObject(writer, c);

                        while (NextCombObj(c))
                            PrintCombObject(writer, c);
                    }
                }
            }
        }
        public class Combinations_with_Repetitions//сочетания с повторенями
        {
            public Combinations_with_Repetitions(int k)
            {
                CombObj = new char[k];
                CombObjects.k = k;
            }
            public virtual void Process()
            {
                for (int i = 0; i < k; i++)
                    CombObj[i] = alphabet[0];

                using (StreamWriter writer = new StreamWriter(@"C:\Users\almaz\source\repos\DM2021\DZ_1\Combinations_with_Repetitions.txt"))
                {
                    PrintHeader(writer, true);
                    PrintCombObject(writer, k);

                    while (NextCombObj())
                        PrintCombObject(writer, k);
                }
            }
            private bool NextCombObj()//генерация следующего объекта
            {
                if ((int)CombObj[0] >= 48 && (int)CombObj[0] <= 57)
                {
                    int i = k - 1;
                    while (i > -1 && CombObj[i] - 48 >= n) i--;

                    if (i < 0) return false;//все сочетания с повторениями сгенерированы
                    CombObj[i]++;

                    for (int j = i + 1; j < k; j++)
                        CombObj[j] = CombObj[i];

                    return true;//генерируем дальше
                }
                else if ((int)CombObj[0] >= 97)
                {
                    int i = k - 1;
                    while (i > -1 && CombObj[i] - 97 > n - 2) i--;

                    if (i < 0) return false;//все сочетания с повторениями сгенерированы
                    CombObj[i]++;

                    for (int j = i + 1; j < k; j++)
                        CombObj[j] = CombObj[i];

                    return true;//генерируем дальше
                }
                return false;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CombObjects CombObj = new CombObjects();

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
                    CombObjects.Variations_with_Repetitions n1 = new CombObjects.Variations_with_Repetitions(k);
                    n1.Process();
                    Console.WriteLine("Задача выполнена");
                    break;
                case (2)://перестановки
                    CombObjects.Permutations n2 = new CombObjects.Permutations();
                    n2.Process();
                    Console.WriteLine("Задача выполнена");
                    break;
                case (3)://размещения по k
                    Console.Write("Введите длину слова: ");
                    k = int.Parse(Console.ReadLine());
                    CombObjects.K_Variations n3 = new CombObjects.K_Variations(k);
                    n3.Process();
                    Console.WriteLine("Задача выполнена");
                    break;
                case (4)://подмножества
                    CombObjects.Subsets n4 = new CombObjects.Subsets();
                    n4.Process();
                    Console.WriteLine("Задача выполнена");
                    break;
                case (5)://сочетания по k
                    Console.Write("Введите длину слова: ");
                    k = int.Parse(Console.ReadLine());
                    CombObjects.K_Combinations n5 = new CombObjects.K_Combinations(k);
                    n5.Process();
                    Console.WriteLine("Задача выполнена");
                    break;
                case (6)://сочетания с повторениями
                    Console.Write("Введите длину слова: ");
                    k = int.Parse(Console.ReadLine());
                    CombObjects.Combinations_with_Repetitions n6 = new CombObjects.Combinations_with_Repetitions(k);
                    n6.Process();
                    Console.WriteLine("Задача выполнена");
                    break;
            }
        }
    }
}