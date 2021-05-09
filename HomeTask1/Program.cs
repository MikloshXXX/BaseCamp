using System;
using System.Text;

namespace HomeTask1
{
    class Program
    {
        static void Main(string[] args)
        {

        }
        //По результатам семестра в 1-А классе 10 отличником, 14 хорошистов, 4 троечника.В 1-Б - 8 отличников,
        //12 хорошистов, 5 троечников. 1-В - 12 отличников, 7 хорошистов, 8 троечников.
        //Найти: 
        //Сколько отличников, хорошистов и троечников на всей параллели классов
        //% соотношение отличников, хорошистов и троечников в классах
        //% соотношение отличников, хорошистов и троечников на парллели
        private static void ShowInfoAboutClasses()
        {
            int[][] totalClasses = { new int[] { 10, 14, 4 }, new int[] { 8, 12, 5 }, new int[] { 12, 7, 8 } };
            char[] classnames = { 'А', 'Б', 'В' };
            string[] studentNames = { "отличников", "хорошистов", "троечников" };

            Console.WriteLine($"Всего в классах:" +
                $"\n\t{studentNames[0]}:{totalClasses[0][0] + totalClasses[1][0] + totalClasses[2][0]} человек" +
                $"\n\t{studentNames[1]}:{totalClasses[0][1] + totalClasses[1][1] + totalClasses[2][1]} человек" +
                $"\n\t{studentNames[2]}:{totalClasses[0][2] + totalClasses[1][2] + totalClasses[2][2]} человек");

            for (int i = 0; i < totalClasses.Length; i++)
            {
                float percent = (float)100 / (totalClasses[i][0] + totalClasses[i][1] + totalClasses[i][2]);
                Console.WriteLine($"\nСоотношение учеников в классе {classnames[i]}:");
                for (int j = 0; j < totalClasses[i].Length; j++)
                {
                    Console.Write($"\t{studentNames[j]}:{(float)totalClasses[i][j] * percent} %\n");
                }
            }

            Console.Write($"\nСоотношение учеников на параллели:");
            for (int i = 0; i < totalClasses.Length; i++)
            {
                for (int j = 0; j < totalClasses[i].Length; j++)
                {
                    float percent = (float)100 / (totalClasses[0][j] + totalClasses[1][j] + totalClasses[2][j]);
                    Console.Write($"\n\t{studentNames[j]} в классе {classnames[i]}:" +
                        $"{totalClasses[i][j] * percent} %");
                }
                Console.WriteLine();
            }
        }

        //Дано число, проверить какого оно типа: нечётное, чётное
        private static bool IsNumberEven(int number)
        {
            return number % 2 == 0 ? true : false;
        }

        //Дано 3 числа, найти число которое находится между. A < B < C
        private static int FindMiddleNumber(int a, int b, int c)
        {
            int[] arr = { a, b, c };
            Array.Sort(arr);
            if (arr[0] < arr[1] && arr[1] < arr[2])
                return arr[1];
            else
                return 0;
        }

        //Дан произвольный массив чисел, найти уникальные числа в нём. Использовать только циклы, условные операторы.
        private static int[] FindUniqueNumbers(int[] initial)
        {
            int[] uniqueNumbers = new int[initial.Length];
            int counterOfUniqueNumbers = 0;

            for (int i = 0; i < initial.Length; i++)
            {
                bool isUnique = true;
                for (int j = 0; j < initial.Length; j++)
                {
                    if (initial[i] == initial[j] && i != j)
                    {
                        isUnique = false;
                    }
                }
                if (isUnique)
                {
                    uniqueNumbers[counterOfUniqueNumbers++] = initial[i];
                }
            }

            int[] finalUniqueNumber = new int[counterOfUniqueNumbers];
            if (counterOfUniqueNumbers > 0)
            {

                for (int i = 0; i < finalUniqueNumber.Length; i++)
                {
                    finalUniqueNumber[i] = uniqueNumbers[i];
                }
                return finalUniqueNumber;
            }
            return null;
        }
        //Написать метод который сможет транспонировать матрицу.
        private static int[,] TransposeMatirx(int[,] initial)
        {
            int[,] transposed = new int[initial.GetLength(1), initial.GetLength(0)];
            for (int i = 0; i < initial.GetLength(0); i++)
            {
                for (int j = 0; j < initial.GetLength(1); j++)
                {
                    transposed[j, i] = initial[i, j];
                }
            }
            return transposed;
        }

        //Написать метод который будет округлять число до N символов
        private static double RoundTo(double number, int n)
        {
            return Math.Round(number * Math.Pow(10, n)) / (Math.Pow(10, n));
        }
        private static double RoundAsString(double number, int n)
        {
            return double.Parse(number.ToString().Substring(0, n));
        }
        //Найти y ():
        //Y = 100 * tg(x) * √x / x
        //Y = 2 * sin(x) - 4
        private static double Y1(double x)
        {
            return 100 * Math.Tan(x) * Math.Sqrt(x) / x;
        }
        private static double Y2(double x)
        {
            return 2 * Math.Sin(x) - 4;
        }
        //Дано произвольную строку, найти строку между указаным символом.Выводить только первое совпадение.
        //Например: 
        //Строка: “я-нехочу-делать-дз”.
        //Символ: -
        //Результат: нехочу
        private static string FindStringBetween(string source, char separator)
        {
            string[] separetedSource = source.Split(separator, int.MaxValue);
            if (separetedSource.Length > 2)
            {
                return separetedSource[1];
            }
            return null;
        }
        //Найти слово в произвольной строке и вывести индексы границ этого слова в строке.
        //Пример: “Lorem ipsum dolor sit amet”
        //Результат: 7-12
        private static void ShowWordIndexes(string source, string word)
        {
            int firstIndex = source.IndexOf(word) + 1;
            int lastIndex = firstIndex + word.Length;
            Console.WriteLine(firstIndex + "-" + lastIndex);
        }
        //Дан произвольный массив чисел, найти числа которые повторяются более 2-х раз. Использовать только циклы, условные операторы.
        private static int[] FindNotUniqueNumbers(int[] initial)
        {
            int[] notUniqueNumbers = new int[initial.Length];
            int counterOfUniqueNumbers = 0;

            for (int i = 0; i < initial.Length; i++)
            {
                int repeatCounter = 0;
                for (int j = 0; j < initial.Length; j++)
                {
                    if (initial[i] == initial[j])
                    {
                        repeatCounter++;
                    }
                }
                if (repeatCounter >= 2)
                {
                    bool isFinalArrayContainTheNumber = false;
                    for (int j = 0; j < counterOfUniqueNumbers; j++)
                    {
                        if (notUniqueNumbers[j] == initial[i])
                            isFinalArrayContainTheNumber = true;
                    }
                    if (!isFinalArrayContainTheNumber)
                        notUniqueNumbers[counterOfUniqueNumbers++] = initial[i];
                }
            }

            int[] finalNotUniqueNumbers = new int[counterOfUniqueNumbers];
            if (counterOfUniqueNumbers > 0)
            {

                for (int i = 0; i < finalNotUniqueNumbers.Length; i++)
                {
                    finalNotUniqueNumbers[i] = notUniqueNumbers[i];
                }
                return finalNotUniqueNumbers;
            }
            return null;
        }
    }
}


