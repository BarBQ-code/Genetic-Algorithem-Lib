using GeneticLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Rene_Example
{
    class Program
    {
        static int populationSize = 500;
        static string target = "To be or not to be, that is the question.";
        static string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.!?,'– ";
        static double mutationRate = 0.01;
        static Random rnd = new Random();
        
        static GeneticAlgorithem<char> ga;


        static void Main(string[] args)
        {

            ga = new GeneticAlgorithem<char>(populationSize, target.Length, rnd, GetRandomChar, FitnessFunction, mutationRate);
            var start = DateTime.Now;
            while(true)
            {
                ga.NewGeneration();

                string bestPhrase = "";
                foreach (char c in ga.BestGenes)
                    bestPhrase += c;
                Console.WriteLine($"Gen: {ga.Generation} \t AvgFit: {ga.FitnessSum / populationSize} \t Current Best: {bestPhrase}");

                if (ga.BestFitness == 1)
                    break;
            }
            var end = DateTime.Now;

            Console.WriteLine($"Total time for answer: {end - start}");

            Console.ReadKey();

        }

        static char GetRandomChar()
        {
            return validChars[rnd.Next(0, validChars.Length)];
        }

        static double FitnessFunction(int index)
        {
            double score = 0;
            DNA<char> dna = ga.Population[index];

            for (int i = 0; i < dna.Genes.Length; i++)
            {
                if (dna.Genes[i] == target[i])
                    score++;
            }
            score = score / target.Length;
            score = Math.Pow(score, 200);
            return score;
        }
    }
}
