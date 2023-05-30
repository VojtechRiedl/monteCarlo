using System;
using System.Diagnostics;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch s = new();
            s.Start();
            parralelMonteCarlo(10000000000);
            s.Stop();
            Console.WriteLine("Time: " + s.ElapsedMilliseconds);

        }


        
      

        public static void parralelMonteCarlo(long numPoints)
        {      
            long numThreads = Environment.ProcessorCount;
            Console.WriteLine(numThreads);// Number of threads to use
            double sum = 0;
            long max = 0;

            Parallel.For(0, numThreads, i =>
            {
                // Calculate the number of points to generate for this thread
                long threadPoints = numPoints / numThreads;
                if (i == numThreads - 1)
                    threadPoints += numPoints % numThreads;
                
                // Generate random points and count the number inside the unit circle
                Random rand = new Random();
                long count = 0;
                long pointsInCircle = 0;
                for (long j = 0; j < threadPoints; j++)
                {
                    double x = rand.NextDouble();
                    double y = rand.NextDouble();
                    if (x * x + y * y <= 1)
                    {
                        count++;
                        pointsInCircle++;

                    }
                }

                // Add this thread's contribution to the sum
                sum += (double)count / threadPoints;
                max += pointsInCircle;
            });

            // Estimate pi
            double pi = 4 * sum / numThreads;
            Console.WriteLine("Estimated pi: " + pi);
            Console.WriteLine("Interaction: " + max);
        }

        public static void serialMonteCarlo(long numPoints)
        {
            Random rand = new Random();
            long pointsInCircle = 0;


            for (long j = 0; j < numPoints; j++)
            {
                double x = rand.NextDouble();
                double y = rand.NextDouble();
                if (x * x + y * y <= 1)
                {
                    pointsInCircle++;

                }
            }


            // Estimate pi
            double pi = 4 * (double)pointsInCircle / numPoints;
            Console.WriteLine("Estimated pi: " + pi);
            Console.WriteLine("Interaction: " + pointsInCircle);
        }

    }
}