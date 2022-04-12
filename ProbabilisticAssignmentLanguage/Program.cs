using System;
using System.Collections.Generic;
using System.Text;

namespace ProbabilisticAssignmentLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string s = "prob x = [(0, 1), (1, 1)]; prob y = [(0, 1), (1, 1)]; prob z = x + y; observe !(z < 1); out [z]";
            Language l = new Language();
            var tre = l.TestTokenizerWithExampleProgram(s);
            var tree = l.TestParserWithExampleProgram(s);
            var treee = l.TestInterpreterWithExampleProgram(s);
            Console.WriteLine("Hello World!");
        }
    }
}

/* Example Program:
 * 
 * prob x = [(0, 1), (1, 1)];
 * prob y = [(0, 1), (1, 1)];
 * prob z = x + y;
 * observe !(z < 1);
 * out z
 */

/* Example of Disjoint Union using already implemented functions
 * (may still be worth implementing a shortcut)
 * 
 * prob A = [(0, 3), (1, 2)];        //weighting between B0 and B1
 * prob B0 = [(0, 2), (1, 3)];       //first set in the union
 * prob B1 = [(0, 4), (1, 1)];       //second set in the union
 * prob mult = [(0, 1), (1, 1)];     //helper variable
 * observe (A=0 & mult=1) | (A=1 & mult=0);
 * prob B = mult*B0 + (1-mult)*B1;   //result of the union
 * 
 * This also implements a Bayesian Chain, specifically where:
 * A is 0.6 for 0 and 0.4 for 1
 * B is:
 * 0.4 for 0 and 0.6 for 1
 * when A = 0
 * 0.8 for 0 and 0.2 for 1
 * when A = 1
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