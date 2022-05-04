using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProbabilisticAssignmentLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\Joseph\\source\\repos\\ProbabilisticAssignmentLanguage\\ProbabilisticAssignmentLanguage\\ProgramTextfiles\\";
            string figure1 = File.ReadAllText(path + "figure1.txt");
            string figure6a = File.ReadAllText(path + "figure6a.txt");
            string figure6b = File.ReadAllText(path + "figure6b.txt");
            string figure15 = File.ReadAllText(path + "figure15.txt");

            string testerProgram = File.ReadAllText(path + "testerProgram.txt");

            PrintOutput(figure1, "figure1");
            PrintOutput(figure6a, "figure6a");
            PrintOutput(figure6b, "figure6b");
            PrintOutput(figure15, "figure15");
            (Queue<Token>, Queue<long>, Queue<string>) llllllll = new Language().RunTokenizerWithExampleProgram(testerProgram);
            PrintOutput(testerProgram, "testerProgram");
        }

        private static void PrintOutput(string program, string programName)
        {
            Console.WriteLine("Program " + programName + "'s output:");
            Dictionary<string, ulong>[] dicts = new Language().RunInterpreterWithExampleProgram(program);
            foreach (Dictionary<string, ulong> dict in dicts)
            {
                foreach (string key in dict.Keys)
                {
                    Console.WriteLine(key + ": " + dict[key]);
                }
                Console.WriteLine("");
            }
        }
    }
}

/* Example Programs:
 * 
 * implementation of a program similar to those in figure 1 can be found in figure1.txt
 * 
 * figure 2 cannot be implemented without significant changes because they
 * contain while loops that check the value of a probability distribution
 * 
 * implementation of a program similar to those in figure 6a can be found in figure6a.txt
 * 
 * implementation of a program similar to those in figure 6b can be found in figure6b.txt
 * 
 * figure 8 (and any markov chain) cannot be implemented without significant changes 
 * because they contain while loops that check the value of a probability distribution
 * 
 * figure 9 cannot be implemented without significant changes because
 * it contains continuous probability distributions
 * 
 * figure 10 (and any markov chain) cannot be implemented without significant changes 
 * because they contain while loops that check the value of a probability distribution
 * 
 * figure 11 cannot be implemented without significant changes because
 * it contains continuous probability distributions
 * 
 * implementation of a program similar to those in figure 15 (and 16) can be found in figure15.txt
 */

/* Example of Disjoint Union using already implemented functions
 * (may still be worth implementing a shortcut)
 * 
 * prob A = [(0, 3), (1, 2)];           //weighting between B0 and B1
 * prob B0 = [(0, 2), (1, 3)];          //first set in the union
 * prob B1 = [(0, 4), (1, 1)];          //second set in the union
 * prob mult = [(0, 1), (1, 1)];        //helper variable
 * observe ((A=0) & (mult=1)) | ((A=1) & (mult=0));
 * prob B = (mult*B0) + ((1-mult)*B1);  //result of the union
 * 
 * This also implements a Bayesian Chain, specifically where:
 * A is 0.6 for 0 and 0.4 for 1
 * B is:
 * 0.4 for 0 and 0.6 for 1 when A = 0
 * 0.8 for 0 and 0.2 for 1 when A = 1
 */


/* Plan:
 * 
 * an IP will work like a function: it will remember a recipe for constructing a probability distribution from a collection of R's.
 * observe will work similarly: it will remember a recipe for constructing a boolean expression from a collection of R's
 * every R introduced over the course of the program will be saved to a big basic distribution list.
 * every observe called will be saved to a big observe list.
 * every IP called with out will be saved to a big output list.
 * at the end of the program, every tuple in the cartesian product of the entire basic distribution list will be looped through;
 * each tuple will be checked against every boolean expression in the trimming list;
 * the ones that are kept will be plugged into every recipe for constructing a probability in the output list;
 * the resulting probabilities will be printed in some fashion.
 * 
 * for the example program it would look like:
 * distribution = {[0, 1] , [0, 1]}
 * observe = {!(distribution[0] + distribution[1] < 1)}
 * output = distribution[0] + distribution[1]
 * 
 * cartesian product of distribution: {(0, 0), (0, 1), (1, 0), (1, 1)}
 * after trimming: {(0, 1), (1, 0), (1, 1)}
 * turned into z: {1, 1, 2}
 */