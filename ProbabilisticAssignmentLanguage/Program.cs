using System;
using System.Collections.Generic;
using System.Text;

namespace ProbabilisticAssignmentLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            string figure1 = "prob x = [(0, 1), (1, 1)]; prob y = [(0, 1), (1, 1)]; prob z = x + y; observe !(z < 1); out [z]";
            string figure6a = "prob I = [(0, 7), (1, 3)]; prob D = [(0, 3), (1, 2)]; prob Si0 = [(0, 19), (1, 1)]; prob Si1 = [(0, 1), (1, 4)]; prob SmultI = [(0, 1), (1, 1)]; observe((I = 0) & (SmultI = 1)) | ((I = 1) & (SmultI = 0)); prob S = (SmultI * Si0) + ((1 - SmultI) * Si1); prob Gi0d0 = [(0, 3), (1, 7)]; prob Gi0d1 = [(0, 1), (1, 19)]; prob Gi1d0 = [(0, 9), (1, 1)]; prob Gi1d1 = [(0, 1), (1, 1)]; prob GmultI = [(0, 1), (1, 1)]; prob GmultD = [(0, 1), (1, 1)]; observe((I = 0) & (GmultI = 1)) | ((I = 1) & (GmultI = 0)); observe((D = 0) & (GmultD = 1)) | ((D = 1) & (GmultD = 0)); prob G = (GmultI * GmultD * Gi0d0) + (GmultI * (1 - GmultD) * Gi0d1) + ((1 - GmultI) * GmultD * Gi1d0) + ((1 - GmultI) * (1 - GmultD) * Gi1d1); prob Lg0 = [(0, 9), (1, 1)]; prob Lg1 = [(0, 2), (1, 3)]; prob LmultG = [(0, 1), (1, 1)]; observe((G = 0) & (LmultG = 1)) | ((G = 1) & (LmultG = 0)); prob L = (LmultG * Lg0) + ((1 - LmultG) * Lg1); out [I, D, S, G, L]; out [I]; out [D]; out [S, I]; out [G, I, D]; out [L, G]";
            string figure6b = "prob I = [(0, 7), (1, 3)]; prob D = [(0, 3), (1, 2)]; prob Si0 = [(0, 19), (1, 1)]; prob Si1 = [(0, 1), (1, 4)]; prob SmultI = [(0, 1), (1, 1)]; observe((I = 0) & (SmultI = 1)) | ((I = 1) & (SmultI = 0)); prob S = (SmultI * Si0) + ((1 - SmultI) * Si1); prob Gi0d0 = [(0, 3), (1, 7)]; prob Gi0d1 = [(0, 1), (1, 19)]; prob Gi1d0 = [(0, 9), (1, 1)]; prob Gi1d1 = [(0, 1), (1, 1)]; prob GmultI = [(0, 1), (1, 1)]; prob GmultD = [(0, 1), (1, 1)]; observe((I = 0) & (GmultI = 1)) | ((I = 1) & (GmultI = 0)); observe((D = 0) & (GmultD = 1)) | ((D = 1) & (GmultD = 0)); prob G = (GmultI * GmultD * Gi0d0) + (GmultI * (1 - GmultD) * Gi0d1) + ((1 - GmultI) * GmultD * Gi1d0) + ((1 - GmultI) * (1 - GmultD) * Gi1d1); prob Lg0 = [(0, 9), (1, 1)]; prob Lg1 = [(0, 2), (1, 3)]; prob LmultG = [(0, 1), (1, 1)]; observe((G = 0) & (LmultG = 1)) | ((G = 1) & (LmultG = 0)); prob L = (LmultG * Lg0) + ((1 - LmultG) * Lg1); observe G=1; out [L]";
            string figure15 = "prob Earthquake = [(0, 1), (1, 999)]; prob Burglary = [(0, 1), (1, 99)]; prob Alarm = Earthquake + Burglary; prob Pe0 = [(0, 1), (1, 99)]; prob Pe1 = [(0, 2), (1, 3)]; prob PmultE = [(0, 1), (1, 1)]; observe((Earthquake = 0) & (PmultE = 1)) | ((Earthquake = 1) & (PmultE = 0)); prob PhoneWorking = (PmultE * Pe0) + ((1 - PmultE) * Pe1); prob Me0a0 = [(0, 4), (1, 1)]; prob Me0a1 = [(0, 2), (1, 3)]; prob Me1a0 = [(0, 4), (1, 1)]; prob Me1a1 = [(0, 1), (1, 4)]; prob MmultE = [(0, 1), (1, 1)]; prob MmultA = [(0, 1), (1, 1)]; observe((Earthquake = 0) & (MmultE = 1)) | ((Earthquake = 1) & (MmultE = 0)); observe((Alarm = 0) & (MmultA = 1)) | ((Alarm = 1) & (MmultA = 0)); prob MaryWakes = (MmultE * MmultA * Me0a0) + (MmultE * (1 - MmultA) * Me0a1) + ((1 - MmultE) * MmultA * Me1a0) + ((1 - MmultE) * (1 - MmultA) * Me1a1); prob Called = MaryWakes * PhoneWorking; observe(Called = 1); out [Burglary]";

            Language l = new Language();
            Console.WriteLine("Program from Fig1:");
            Dictionary<string, int>[] dicts = l.TestInterpreterWithExampleProgram(figure1);
            foreach(Dictionary<string, int> dict in dicts)
            {
                foreach (string key in dict.Keys)
                {
                    Console.WriteLine(key + ": " + dict[key]);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Program from Fig6a:");
            dicts = l.TestInterpreterWithExampleProgram(figure6a);
            foreach (Dictionary<string, int> dict in dicts)
            {
                foreach (string key in dict.Keys)
                {
                    Console.WriteLine(key + ": " + dict[key]);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Program from Fig6b:");
            dicts = l.TestInterpreterWithExampleProgram(figure6b);
            foreach (Dictionary<string, int> dict in dicts)
            {
                foreach (string key in dict.Keys)
                {
                    Console.WriteLine(key + ": " + dict[key]);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Program from Fig15:");
            dicts = l.TestInterpreterWithExampleProgram(figure15);
            foreach (Dictionary<string, int> dict in dicts)
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
 * implementation of a program similar to those in figure 1:
 * prob x = [(0, 1), (1, 1)];
 * prob y = [(0, 1), (1, 1)];
 * prob z = x + y;
 * observe !(z < 1);
 * out [z]
 * 
 * figure 2 cannot be implemented without significant changes because they
 * contain while loops that check the value of a probability distribution
 * 
 * implementation of a program similar to those in figure 6a:
 * prob I = [(0, 7), (1, 3)];
 * prob D = [(0, 3), (1, 2)];
 * 
 * prob Si0 = [(0, 19), (1, 1)];
 * prob Si1 = [(0, 1), (1, 4)];
 * prob SmultI = [(0, 1), (1, 1)];
 * observe ((I=0) & (SmultI=1)) | ((I=1) & (SmultI=0));
 * prob S = (SmultI*Si0) + ((1-SmultI)*Si1);
 * 
 * prob Gi0d0 = [(0, 3), (1, 7)];
 * prob Gi0d1 = [(0, 1), (1, 19)];
 * prob Gi1d0 = [(0, 9), (1, 1)];
 * prob Gi1d1 = [(0, 1), (1, 1)];
 * prob GmultI = [(0, 1), (1, 1)];
 * prob GmultD = [(0, 1), (1, 1)];
 * observe ((I=0) & (GmultI=1)) | ((I=1) & (GmultI=0));
 * observe ((D=0) & (GmultD=1)) | ((D=1) & (GmultD=0));
 * prob G = (GmultI*GmultD*Gi0d0) + (GmultI*(1-GmultD)*Gi0d1) + ((1-GmultI)*GmultD*Gi1d0) + ((1-GmultI)*(1-GmultD)*Gi1d1);
 * 
 * prob Lg0 = [(0, 9), (1, 1)];
 * prob Lg1 = [(0, 2), (1, 3)];
 * prob LmultG = [(0, 1), (1, 1)];
 * observe ((G=0) & (LmultG=1)) | ((G=1) & (LmultG=0));
 * prob L = (LmultG*Lg0) + ((1-LmultG)*Lg1);
 * 
 * out [I, D, S, G, L];
 * out [I];
 * out [D];
 * out [S, I];
 * out [G, I, D];
 * out [L, G]
 * 
 * implementation of a program similar to those in figure 6b:
 * prob I = [(0, 7), (1, 3)];
 * prob D = [(0, 3), (1, 2)];
 * 
 * prob Si0 = [(0, 19), (1, 1)];
 * prob Si1 = [(0, 1), (1, 4)];
 * prob SmultI = [(0, 1), (1, 1)];
 * observe ((I=0) & (SmultI=1)) | ((I=1) & (SmultI=0));
 * prob S = (SmultI*Si0) + ((1-SmultI)*Si1);
 * 
 * prob Gi0d0 = [(0, 3), (1, 7)];
 * prob Gi0d1 = [(0, 1), (1, 19)];
 * prob Gi1d0 = [(0, 9), (1, 1)];
 * prob Gi1d1 = [(0, 1), (1, 1)];
 * prob GmultI = [(0, 1), (1, 1)];
 * prob GmultD = [(0, 1), (1, 1)];
 * observe ((I=0) & (GmultI=1)) | ((I=1) & (GmultI=0));
 * observe ((D=0) & (GmultD=1)) | ((D=1) & (GmultD=0));
 * prob G = (GmultI*GmultD*Gi0d0) + (GmultI*(1-GmultD)*Gi0d1) + ((1-GmultI)*GmultD*Gi1d0) + ((1-GmultI)*(1-GmultD)*Gi1d1);
 * 
 * prob Lg0 = [(0, 9), (1, 1)];
 * prob Lg1 = [(0, 2), (1, 3)];
 * prob LmultG = [(0, 1), (1, 1)];
 * observe ((G=0) & (LmultG=1)) | ((G=1) & (LmultG=0));
 * prob L = (LmultG*Lg0) + ((1-LmultG)*Lg1);
 * 
 * observe G=1;
 * out [L]
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
 * implementation of a program similar to those in figure 15 (and 16):
 * prob Earthquake = [(0, 1), (1, 999)];
 * prob Burglary = [(0, 1), (1, 99)];
 * prob Alarm = Earthquake + Burglary;
 * 
 * prob Pe0 = [(0, 1), (1, 99)];
 * prob Pe1 = [(0, 2), (1, 3)];
 * prob PmultE = [(0, 1), (1, 1)];
 * observe ((Earthquake=0) & (PmultE=1)) | ((Earthquake=1) & (PmultE=0));
 * prob PhoneWorking = (PmultE*Pe0) + ((1-PmultE)*Pe1);
 * 
 * prob Me0a0 = [(0, 4), (1, 1)];
 * prob Me0a1 = [(0, 2), (1, 3)];
 * prob Me1a0 = [(0, 4), (1, 1)];
 * prob Me1a1 = [(0, 1), (1, 4)];
 * prob MmultE = [(0, 1), (1, 1)];
 * prob MmultA = [(0, 1), (1, 1)];
 * observe ((Earthquake=0) & (MmultE=1)) | ((Earthquake=1) & (MmultE=0));
 * observe ((Alarm=0) & (MmultA=1)) | ((Alarm=1) & (MmultA=0));
 * prob MaryWakes = (MmultE*MmultA*Me0a0) + (MmultE*(1-MmultA)*Me0a1) + ((1-MmultE)*MmultA*Me1a0) + ((1-MmultE)*(1-MmultA)*Me1a1);
 * 
 * prob Called = MaryWakes * PhoneWorking;
 * observe(Called = 1);
 * out [Burglary]
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