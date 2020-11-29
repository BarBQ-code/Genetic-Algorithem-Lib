using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticLib
{
    public class DNA<T>
    {
        public T[] Genes { get; private set; }

        public double Fitness { get; private set; }

        private Random rnd;
        private Func<T> GetRandomGene;
        private Func<int, double> FitnessFunction;
        public DNA(int size, Random rnd, Func<T> GetRandomGene, Func<int, double> FitnessFunction, bool shouldInititalizeGenes = true)
        {
            Genes = new T[size];
            this.rnd = rnd;
            this.GetRandomGene = GetRandomGene;
            this.FitnessFunction = FitnessFunction;
            if (shouldInititalizeGenes)
            {
                for (int i = 0; i < Genes.Length; i++)
                {
                    Genes[i] = GetRandomGene();
                }
            }
        }

        public double CalcFitness(int index)
        {
            Fitness = FitnessFunction(index);
            return Fitness;
        }

        public DNA<T> CrossOver(DNA<T> partner)
        {
            DNA<T> child = new DNA<T>(Genes.Length, rnd, GetRandomGene, FitnessFunction, false);
            
            for (int i = 0; i < Genes.Length; i++)
            {
                child.Genes[i] = rnd.NextDouble() < 0.5 ? Genes[i] : partner.Genes[i];
            }
            return child;
        }

        public void Mutate(double mutationRate)
        {
            for (int i = 0; i < Genes.Length; i++)
            {
                if(rnd.NextDouble() < mutationRate)
                {
                    Genes[i] = GetRandomGene();
                }
            }
        }
    }
}
