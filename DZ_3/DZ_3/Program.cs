using System;
using System.IO;

namespace DZ_3
{
    public class CombObjects//модифицирован конструктор из 1 задания и убраны методы, генерирующие ненужные в задаче объекты
    {
        static int n = 5;
        static int k;
        public char[] CombObj;
        public char[] alphabet;

        public CombObjects(bool pos)
        {
            if (pos)
            {
                alphabet = new char[5] { '1', '2', '3', '4', '5' };
                CombObj = new char[2] { '1', '2' };
            }
            else
            {
                k = 3;
                alphabet = new char[6] { 'a', 'b', 'c', 'd', 'e', 'f' };
                CombObj = new char[6] { 'a', 'b', 'c', 'd', 'e', 'f' };
            }
        }
        private void Swap(int i, int j)
        {
            char s = CombObj[i];
            CombObj[i] = CombObj[j];
            CombObj[j] = s;
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
        public bool NextCombination(char[] Obj, int n, int p)//генерация следующего сочетания по k
        {
            if ((int)Obj[0] >= 48 && (int)Obj[0] <= 57)
            {
                for (int i = p - 1; i > -1; i--)
                    if (Obj[i] - 48 < n - p + i + 1)
                    {
                        Obj[i]++;
                        for (int j = i + 1; j < p; ++j)
                        {
                            Obj[j] = Obj[j - 1];
                            Obj[j]++;
                        }
                        return true;//генерируем дальше
                    }
                return false;//все сочетания сгенерированы
            }
            else if ((int)Obj[0] >= 97)
            {
                for (int i = p - 1; i > -1; i--)
                    if (Obj[i] - 97 < n - p + i)
                    {
                        Obj[i]++;
                        for (int j = i + 1; j < p; ++j)
                        {
                            Obj[j] = Obj[j - 1];
                            Obj[j]++;
                        }
                        return true;//генерируем дальше
                    }
                return false;//все сочетания сгенерированы
            }
            return false;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            //
            //      Множество = {a, b, c, d, e, f}. Построить все слова длины 5, в которых ровно две буквы повторяются,
            //      остальные буквы не повторяются.
            //                          1     2                    2    3
            //      Кол-во таких слов: C  *  C  * 5 * 4 * 3 = 6 * C  * A  = 3600
            //                          6     5                    5    5

            CombObjects Object3 = new(false); //для размещений(для всех остальных букв)
            CombObjects Object4 = new(true); //для сочетаний (для расстановки повторяющейся буквы)
            CombObjects Object5 = new(false); //для выбора повторяющейся буквы

            using (StreamWriter writer = new StreamWriter(@"C:\Users\almaz\source\repos\DM2021\DZ_3\Solution1.txt"))
            {
                do
                {
                    Object4.CombObj[0] = '1'; Object4.CombObj[1] = '2';
                    do
                    {
                        for (char i = 'b'; i <= 'f'; i++)
                            Object3.CombObj[i - 98] = i;
                        do
                        {
                            for (int i = 1, k = 0; i < 6; i++) //длина слова - 5, длина размещения - 3 проходим по всем индексам, если индекс присутствует в сочетании, то выводим выбранную букву, иначе выводим член размещения
                            {
                                if (Object4.CombObj[0] == (i + 48) || Object4.CombObj[1] == (i + 48))
                                {
                                    writer.Write(Object5.alphabet[0]); //вывод выбранную букву
                                }
                                else
                                {
                                    writer.Write(Object3.CombObj[k]); //вывод части размещения
                                    k++;
                                }
                            }
                            writer.WriteLine();
                        } while (Object3.NextVariation());
                    } while (Object4.NextCombination(Object4.CombObj, 5, 2));
                } while (Object5.NextCombination(Object5.alphabet, 6, 1));
            }

            Console.WriteLine("Задача 1 выполнена");
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