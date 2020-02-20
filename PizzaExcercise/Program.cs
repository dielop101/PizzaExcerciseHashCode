using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PizzaExcercise
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessFile("a_example");
            ProcessFile("b_small");
            ProcessFile("c_medium");
            ProcessFile("d_quite_big");
            ProcessFile("e_also_big");
        }

        static void ProcessFile(string name)
        {
            var lines = File.ReadLines("Files/" + name + ".in");
            var firstLine = true;

            var piecesPizzaGoal = 0;
            var structure = new List<int>();

            foreach (var line in lines)
            {
                Console.WriteLine(line);

                if (firstLine)
                {
                    piecesPizzaGoal = int.Parse(line.Split(' ')[0]);
                    firstLine = false;
                }
                else
                {
                    foreach (var numOfTrozosByPizza in line.Split(' '))
                    {
                        structure.Add(int.Parse(numOfTrozosByPizza));
                    }
                }
            }

            structure = structure.OrderByDescending(x => x).ToList();

            var result = new List<int>();

            for (int i = 0; i < structure.Count; i++)
            {
                var pizza = structure[i];

                if (piecesPizzaGoal > pizza)
                {
                    piecesPizzaGoal -= pizza;
                    result.Add(structure.Count - i - 1);
                }
            }

            result = result.OrderBy(x => x).ToList();

            var joinedResult = string.Join(" ", result.ToArray());

            var fileNameResult = name + ".out";

            if (File.Exists(fileNameResult))
            {
                File.Delete(fileNameResult);
            }

            using (StreamWriter sw = File.AppendText(fileNameResult))
            {
                sw.WriteLine(result.Count());
                sw.Write(joinedResult);
            }
        }
    }
}
