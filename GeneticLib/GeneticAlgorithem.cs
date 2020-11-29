using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticLib
{
    public class GeneticAlgorithem<T>
    {
        public List<DNA<T>> Population { get; private set; }
        public int Generation { get; private set; }
        public double BestFitness { get; private set; }
        public T[] BestGenes { get; private set; } 
        public double FitnessSum { get; private set; } = 0;

        public double MutationRate;

        private Random rnd;

        public GeneticAlgorithem(int popSize, int dnaSize, Random rnd, Func<T> GetRandomGene, Func<int, double> FitnessFunction, double mutationRate = 0.01)
        {
            Generation = 1;
            MutationRate = mutationRate;
            Population = new List<DNA<T>>();
            this.rnd = rnd;
            BestGenes = new T[dnaSize];

            for (int i = 0; i < popSize; i++)
            {
                Population.Add(new DNA<T>(dnaSize, rnd, GetRandomGene, FitnessFunction));
            }
        }

        public void NewGeneration()
        {
            if(Population.Count == 0)
            {
                return;
            }

            CalcFitness();

            List<DNA<T>> newPopulation = new List<DNA<T>>();

            for (int i = 0; i < Population.Count; i++)
            {
                DNA<T> parent1 = ChooseParent();
                DNA<T> parent2 = ChooseParent();

                DNA<T> child = parent1.CrossOver(parent2);

                child.Mutate(MutationRate);

                newPopulation.Add(child);

            }

            Population = newPopulation;
            Generation++;
        }


        private void CalcFitness()
        {
            FitnessSum = 0;
            DNA<T> best = Population[0];
            

            for (int i = 0; i < Population.Count; i++)
            {
                FitnessSum += Population[i].CalcFitness(i);

                if (Population[i].Fitness > best.Fitness)
                    best = Population[i];
            }

            BestFitness = best.Fitness;
            best.Genes.CopyTo(BestGenes, 0);
        }
        private DNA<T> ChooseParent()
        {
            double randomNum = rnd.NextDouble() * FitnessSum;

            for (int i = 0; i < Population.Count; i++)
            {
                if (randomNum < Population[i].Fitness)
                    return Population[i];

                randomNum -= Population[i].Fitness;
            }

            return null;
        }
    }
}
