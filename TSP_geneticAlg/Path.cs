using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_geneticAlg
{
    class Path
    {
        public int[] selection = new int[5];
        public int length = 0;

        public Path()
        {
            // Fill path array with unique to each other cities
            for (int i = 0; i < 5; i++)
            {
                Random random = new Random();
                selection[i] = random.Next(0, 5);
                
                for (int j = 0; j < 5; j++)
                {
                    if (selection[i] == selection[j] && i != j)
                    {
                        i--;
                        j = 5;
                    }
                }
            }

            length = CalculateDistance();
        }


        public Path(int i0, int i1, int i2, int i3, int i4)
        {
            selection[0] = i0;
            selection[1] = i1;
            selection[2] = i2;
            selection[3] = i3;
            selection[4] = i4;

            length = CalculateDistance();
        }


        // Find the path's distance (fitness)
        public int CalculateDistance()
        {
            int[,] distances = new int[5, 5];
            // A (0)
            distances[0, 0] = 0;
            distances[0, 1] = 4;
            distances[0, 2] = 4;
            distances[0, 3] = 7;
            distances[0, 4] = 3;
            // B (1)
            distances[1, 0] = 4;
            distances[1, 1] = 0;
            distances[1, 2] = 2;
            distances[1, 3] = 3;
            distances[1, 4] = 5;
            // C (2)
            distances[2, 0] = 4;
            distances[2, 1] = 2;
            distances[2, 2] = 0;
            distances[2, 3] = 2;
            distances[2, 4] = 3;
            // D (3)
            distances[3, 0] = 7;
            distances[3, 1] = 3;
            distances[3, 2] = 2;
            distances[3, 3] = 0;
            distances[3, 4] = 6;
            // E (4)
            distances[4, 0] = 3;
            distances[4, 1] = 5;
            distances[4, 2] = 3;
            distances[4, 3] = 6;
            distances[4, 4] = 0;

            bool eliminateLength = false;
            for(int i = 0; i < 5; i++) // Check if all cities are unique
            {
                for(int j = 0; j < 5; j++)
                {
                    if (i != j)
                    {
                        if (selection[i] == selection[j])
                            eliminateLength = true;
                    }
                }
            }

            if (!eliminateLength)
            {
                for (int i = 0; i < 5; i++)
                {
                    int currentCity = selection[i];
                    int nextCity;
                    if (i == 4)
                        nextCity = selection[0]; // if we are at the last city, next city is the first one
                    else
                        nextCity = selection[i + 1];

                    length += distances[currentCity, nextCity];
                }
            }
            else
                length = 100; // Length if we have duplicate cities is a big number.

            return length;
        }
    }
}
