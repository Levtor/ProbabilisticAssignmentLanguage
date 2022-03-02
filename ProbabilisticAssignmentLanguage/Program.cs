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

A::= + | - | * | / | %

B::= O | (B) | B1 and B2 | B1 or B2 | not B | E1 > E2 | E1 < E2

PB::= B | (PB) | PB1 and PB2 | PB1 or PB2 | not PB |
      P > E | P < E | E > P | E < P | P1 > P2 | P1 < P2

E::= N | (E) | E1 A E2

P::= R | (P) | P1 x P2 | P1 # P2 | P1 A P2 | P A E | E A P

R::= [ RR ]

RR::= E | E , RR

*/

/*
Example Program:

prob x = [0, 1];
prob y = [0, 1];
prob z = x + y;
observe not (z < 1)

*/