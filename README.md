A Simple But Powerful And Flexible Library For Genetics Algorithems 

Here Is A Simple Example Of How To Use The Library:

About The Example:
This Program Tries To Guess The Famous Quote From Rene Descartes "I think therefore I am" Or Any Other Quote For That Matter
But With Traditional Brute Forcing Techniques This Would Take Ages, This Is Where Genetics Algorithems Come In

The Genetic Algorithem Class Is A Generic Class That Takes The Requires The Follwing: 

     public GeneticAlgorithem(int popSize, int dnaSize, Random rnd, Func<T> GetRandomGene, Func<int, double> FitnessFunction, double mutationRate = 0.01)
     
     popSize = Population Size
     dnaSize = Size Of The Array Genes
     GetRandomGene = Mutation Function

Set Up:
```csharp
static int populationSize = 500;
static string target = "I think therfore I am.";
static string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.!?,'– ";
static double mutationRate = 0.01;
static Random rnd = new Random();
        
static GeneticAlgorithem<char> ga;

```
These Are The Var's That We Need In Order To Create The Population And The Needed Functions

Mutation Function Implementation:
```csharp
static char GetRandomChar()
{
    return validChars[rnd.Next(0, validChars.Length)];
}
```
Fitness Function Implementation:
```csharp
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
```

Main:
```csharp
ga = new GeneticAlgorithem<char>(populationSize, target.Length, rnd, GetRandomChar, FitnessFunction, mutationRate);
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

```
