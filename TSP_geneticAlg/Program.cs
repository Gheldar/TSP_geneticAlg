using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_geneticAlg
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Table of distances:

               0A  1B  2C  3D  4E
           0A   0   4   4   7   3
           1B   4   0   2   3   5
           2C   4   2   0   2   3
           3D   7   3   2   0   6
           4E   3   5   3   6   0   */

            int populationCount = 16;
            int repeat = 200;
            int resultCount = populationCount;

            Console.WriteLine("Running...");
            Console.WriteLine("(will print top " + resultCount + " results, after " + repeat + " generations)\n");

            // Initialize a population
            Path[] population = new Path[populationCount];
            for (int i = 0; i < populationCount; i++) // fill only half of the array, the rest will default to all-A's.
            {
                if (i < populationCount / 2)
                    population[i] = new Path();
                else
                    population[i] = new Path(6, 6, 6, 6, 6); // fill the second half with false values, to check for errors.
            }

            for (int times = 0; times < repeat; times++)
            {
                // Create children (using CrossOver)
                for (int i = 0; i < populationCount / 2; i += 2)
                {
                    for (int j = 2; j < 5; j++) // Fill children 1 and 2 with selection 2 to 4 of parents
                    {
                        population[populationCount / 2 + i].selection[j] = population[i].selection[j];
                        population[populationCount / 2 + i + 1].selection[j] = population[i + 1].selection[j];
                    }

                    bool doubleEntry;

                    //1st child
                    for (int j = 0; j < 5; j++)
                    {
                        doubleEntry = false;
                        for (int k = 2; k < 5; k++)
                        {
                            if (population[i + 1].selection[j] == population[populationCount / 2 + i + 1].selection[k])
                            {
                                k = 4;
                                doubleEntry = true;
                            }
                        }
                        if (!doubleEntry)
                        {
                            population[populationCount / 2 + i].selection[0] = population[i + 1].selection[j];
                            j = 4;
                        }
                    }
                    for (int j = 0; j < 5; j++)
                    {
                        doubleEntry = false;
                        for (int k = 0; k < 5; k++)
                        {
                            if (population[i + 1].selection[j] == population[populationCount / 2 + i].selection[k])
                            {
                                k = 4;
                                doubleEntry = true;
                            }
                        }
                        if (!doubleEntry)
                        {
                            population[populationCount / 2 + i].selection[1] = population[i + 1].selection[j];
                            j = 4;
                        }
                    }
                    population[populationCount / 2 + i] = new Path(population[populationCount / 2 + i].selection[0], population[populationCount / 2 + i].selection[1], population[populationCount / 2 + i].selection[2], population[populationCount / 2 + i].selection[3], population[populationCount / 2 + i].selection[4]);

                    //2nd child
                    for (int j = 0; j < 5; j++)
                    {
                        doubleEntry = false;
                        for (int k = 2; k < 5; k++)
                        {
                            if (population[i].selection[j] == population[populationCount / 2 + i + 1].selection[k])
                            {
                                k = 4;
                                doubleEntry = true;
                            }
                        }
                        if (!doubleEntry)
                        {
                            population[populationCount / 2 + i + 1].selection[0] = population[i].selection[j];
                            j = 4;
                        }
                    }
                    for (int j = 0; j < 5; j++)
                    {
                        doubleEntry = false;
                        for (int k = 0; k < 5; k++)
                        {
                            if (population[i].selection[j] == population[populationCount / 2 + i + 1].selection[k])
                            {
                                k = 4;
                                doubleEntry = true;
                            }
                        }
                        if (!doubleEntry)
                        {
                            population[populationCount / 2 + i + 1].selection[1] = population[i].selection[j];
                            j = 4;
                        }
                    }
                    population[populationCount / 2 + i + 1] = new Path(population[populationCount / 2 + i + 1].selection[0], population[populationCount / 2 + i + 1].selection[1], population[populationCount / 2 + i + 1].selection[2], population[populationCount / 2 + i + 1].selection[3], population[populationCount / 2 + i + 1].selection[4]);
                }

                // Sort population
                Array.Sort(population, delegate (Path path1, Path path2)
                {
                    return path1.length.CompareTo(path2.length);
                });

                // Mutate the two last paths (switch 1st and last cities)
                for (int i = populationCount - 2; i < populationCount; i++)
                {
                    int temp = population[i].selection[0];
                    population[i].selection[0] = population[i].selection[4];
                    population[i].selection[4] = temp;

                    population[i] = new Path(population[i].selection[0], population[i].selection[1], 
                        population[i].selection[2], population[i].selection[3], population[i].selection[4]);
                }

                // Sort population again
                Array.Sort(population, delegate (Path path1, Path path2)
                {
                    return path1.length.CompareTo(path2.length);
                });
            }

            // Results
            for (int i = 0; i < resultCount; i++)
            {
                Console.Write(i + 1 + ". ");
                for (int j = 0; j < 5; j++)
                {
                    switch (population[i].selection[j])
                    {
                        case 0:
                            Console.Write("A");
                            break;
                        case 1:
                            Console.Write("B");
                            break;
                        case 2:
                            Console.Write("C");
                            break;
                        case 3:
                            Console.Write("D");
                            break;
                        case 4:
                            Console.Write("E");
                            break;
                    }
                    //Console.Write(population[i].selection[j]);
                }
                Console.WriteLine("\nLength: " + population[i].length + "\n");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            /*// Error Check
            int hundreds = 0;
            for(int i=0; i < populationCount; i++)
            {
                if (population[i].length == 100)
                    hundreds++;
            }
            Console.WriteLine(hundreds);*/
        }
    }
}