using System;
using System.IO;

namespace DZ_2
{
    public class CombObjects//модифицирован конструктор из 1 задания и убраны методы, генерирующие ненужные в задаче объекты
    {
        static int n = 5;
        static int k;
        public char[] CombObj;
        public char[] alphabet;

        public CombObjects(bool pos, int TaskNum)
        {
            if (TaskNum == 1)
            {
                if (pos)
                {
                    alphabet = new char[5] { '1', '2', '3', '4', '5' };
                    CombObj = new char[2] { '1', '2' };
                }
                else
                {
                    alphabet = new char[5] { 'b', 'c', 'd', 'e', 'f' };
                    CombObj = new char[3] { 'b', 'b', 'b' };
                }
            }
            else
            {
                if (pos)
                {
                    alphabet = new char[5] { '1', '2', '3', '4', '5' };
                    CombObj = new char[2] { '1', '2' };
                }
                else
                {
                    k = 3;
                    alphabet = new char[5] { 'b', 'c', 'd', 'e', 'f' };
                    CombObj = new char[5] { 'b', 'c', 'd', 'e', 'f' };
                }
            }
        }
        private void Swap(int i, int j)
        {
            char s = CombObj[i];
            CombObj[i] = CombObj[j];
            CombObj[j] = s;
        }
        public bool HasNextVarRep(int k)//есть ли следующее размещение с повторениями
        {
            int kol = 0;
            for (int i = 0; i < k; i++)
                if (CombObj[i] == alphabet[n - 1]) kol++;
            return (kol != k);
        }
        public void NextVariationRep(int k)//генерация следующего размещения с повторениями
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
        public bool NextVariation()//генерация следующего размещения по k
        {
            int j;
            do //повторяем пока не будет найдено следующее размещение
            {
                j = n - 2;
                while (j != -1 && CombObj[j] >= CombObj[j + 1])
                    j--;

                if (j == -1)
                    return false;//все размещения сгенерированы

                int kk = n - 1;
                while (CombObj[j] >= CombObj[kk]) kk--;
                Swap(j, kk);

                int l = j + 1, r = n - 1;
                while (l < r)
                    Swap(l++, r--);
            } while (j > k - 1);

            return true;//генерируем дальше
        }
        public bool NextCombination(int p)//генерация следующего сочетания по k
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
    }


    class Program
    {
        static void Main(string[] args)
        {

            //
            //      Множество = {a, b, c, d, e, f}. Построить все слова длины 5, в которых ровно две буквы a.
            //                          2                2   _3
            //      Кол-во таких слов: C  * 5 * 5 * 5 = C  * A  = 1250
            //                          5                5    5

            CombObjects Object1 = new(false, 1); //для размещений с повторениями (для всех букв, кроме 'а')
            CombObjects Object2 = new(true, 1); //для сочетаний (для 'а')

            using (StreamWriter writer = new StreamWriter(@"C:\Users\almaz\source\repos\DM2021\DZ_2\Solution1.txt"))
            {
                do
                {
                    do
                    {
                        for (int i = 1, k = 0; i < 6; i++) //длина слова - 5, длина размещения - 3 проходим по всем индексам, если индекс присутствует в сочетании, то выводим 'a', иначе выводим член размещения
                        {
                            if (Object2.CombObj[0] == (i + 48) || Object2.CombObj[1] == (i + 48))
                            {
                                writer.Write('a'); //вывод 'a'
                            }
                            else
                            {
                                writer.Write(Object1.CombObj[k]); //вывод части размещения
                                k++;
                            }
                        }
                        writer.WriteLine();
                        Object1.NextVariationRep(3);
                    } while (Object1.HasNextVarRep(3));
                } while (Object2.NextCombination(2));

                for (int i = 1, k = 0; i < 6; i++) //длина слова - 5, длина размещения - 3 проходим по всем индексам, если индекс присутствует в сочетании, то выводим 'a', иначе выводим член размещения
                {
                    if (Object2.CombObj[0] == (i + 48) || Object2.CombObj[1] == (i + 48))
                    {
                        writer.Write('a'); //вывод 'a'
                    }
                    else
                    {
                        writer.Write(Object1.CombObj[k]); //вывод части размещения
                        k++;
                    }
                }
                writer.WriteLine();
            }
            Console.WriteLine("Задача 1 выполнена");

            //
            //      Множество = {a, b, c, d, e, f}. Построить все слова длины 5, в которых ровно две буквы a,
            //      остальные буквы не повторяются.
            //                          2                2    3
            //      Кол-во таких слов: C  * 5 * 4 * 3 = C  * A  = 600
            //                          5                5    5

            CombObjects Object3 = new(false, 2); //для размещений(для всех букв, кроме 'а')
            CombObjects Object4 = new(true, 2); //для сочетаний (для 'а')

            using (StreamWriter writer = new StreamWriter(@"C:\Users\almaz\source\repos\DM2021\DZ_2\Solution2.txt"))
            {
                do
                {
                    for (char i = 'b'; i <= 'f'; i++)
                        Object3.CombObj[i - 98] = i;
                    do
                    {
                        for (int i = 1, k = 0; i < 6; i++) //длина слова - 5, длина размещения - 3 проходим по всем индексам, если индекс присутствует в сочетании, то выводим 'a', иначе выводим член размещения
                        {
                            if (Object4.CombObj[0] == (i + 48) || Object4.CombObj[1] == (i + 48))
                            {
                                writer.Write('a'); //вывод 'a'
                            }
                            else
                            {
                                writer.Write(Object3.CombObj[k]); //вывод части размещения
                                k++;
                            }
                        }
                        writer.WriteLine();
                    } while (Object3.NextVariation());
                } while (Object4.NextCombination(2));
            }

            Console.WriteLine("Задача 2 выполнена");
        }
    }
}
//                                          🌟
//                                          🎄
//                                         🎄🎄
//                                        🎄⁣🎄🎄
//                                       🎄🎄🎄🎄
//                                      🎄🎄🎄🎄🎄
//                                     🎄🎄🎄🎄🎄🎄           𝓗𝓐𝓟𝓟𝓨 𝓝𝓔𝓦 𝓨𝓔𝓐𝓡
//                                    🎄🎄🎄🎄🎄🎄🎄
//                                   🎄🎄🎄🎄🎄🎄🎄🎄
//                                  🎄🎄🎄🎄🎄🎄🎄🎄🎄
//                                 🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄
//                                        🎁🎁🎁