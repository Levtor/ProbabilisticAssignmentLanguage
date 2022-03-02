using System;

namespace ProbabilisticAssignmentLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}



/*
CONCRETE SYNTAX

G : Program
C : Command
B : Boolean Expression
E : Integer Expression
P : Probabilistic Distribution Expression
A : Arithmetic Operator

O::= true | false
N::= strings of digits
IB, IE, and IP::= strings of letters or digits or underscore,
                  starting with a letter,
                  not including keywords

G ::= C

C::=
skip
| C1 ; C2
| if B then C1 else C2
| while B do C
| observe PB
| bool IB = B
| int IE = E
| prob IP = P
| out IP

A::= + | - | * | / | %

B::= O | (B) | B1 and B2 | B1 or B2 | not B | E1 > E2 | E1 < E2

PB::= B | (PB) | PB1 and PB2 | PB1 or PB2 | not PB |
      P > E | P < E | E > P | E < P | P1 > P2 | P1 < P2

E::= N | (E) | E1 A E2

P::= R | (P) | P1 x P2 | P1 # P2 | P1 A P2 | P A E | E A P

R::= [ RR ]

RR::= E | E , RR

*/


/* Example Program:
 * 
 * prob x = [0, 1];
 * prob y = [0, 1];
 * prob z = x + y;
 * observe not (z < 1)
 * out z
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
 * 
 * (except actually I think it might be better to have probability distributions be represented by tuples of value and weight)
 * (so it would look more like z: {(1, 2), (2, 1)}
 */