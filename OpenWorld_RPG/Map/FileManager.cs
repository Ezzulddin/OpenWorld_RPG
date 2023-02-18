using OpenWorld_RPG.Perlin_Noise;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG.Map
{
    public class FileManager
    {
        private StreamWriter writer;
        private StreamReader reader;
        private PerlinNoise PerlinNoise = new PerlinNoise();
        private float[][] perlinNoise;

        public FileManager()
        {

        }

        public void WriteToFile(int tileAmount, int seed)
        {
            writer = File.CreateText("..\\PerlinMap.txt");

            perlinNoise = PerlinNoise.GeneratePerlinNoise(PerlinNoise.GenerateWhiteNoise(tileAmount, tileAmount, seed), 6);

            for (int i = 0; i < tileAmount; i++)
            {
                for (int j = 0; j < tileAmount; j++)
                {
                    if (j != tileAmount - 1)
                    {
                        writer.Write(perlinNoise[i][j] + ",");
                    }
                    else
                    {
                        writer.Write(perlinNoise[i][j]);
                    }
                }
                writer.WriteLine();
            }

            writer.Close();
        }

        public float[,] ReadFile(int tileAmount)
        {
            reader = new StreamReader("..\\PerlinMap.txt");

            string line = "";
            int counter = 0;
            float[,] tileType;

            tileType = new float[tileAmount, tileAmount];

            do
            {
                List<float> tileList = new List<float>();
                line = reader.ReadLine();
                string[] stringArray = line?.Split(',');

                foreach (string s in stringArray)
                {
                    tileList.Add(float.Parse(s));
                }

                float[] fArrary = tileList.ToArray();

                for (int i = 0; i < tileAmount; i++)
                {
                    tileType[counter, i] = fArrary[i];
                }

                if (counter < tileAmount - 1)
                {
                    counter++;
                }

            } while (!reader.EndOfStream);
            reader.Close();

            return tileType;
        }
    }
}
