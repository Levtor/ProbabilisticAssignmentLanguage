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



/*CONCRETE SYNTAX

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
| observe B
| bool IB = B
| int IE = E
| prob IP = P

E::= + | - | * | / | %

B::= O | (B1 and B2) | (B1 or B2) | (not B)

E::= N | (E1 A E2)

P::= R | (P1 x P2) | (P1 A P2) | (P A E) | (E A P)

R::= [ RR ]

RR::= E | E , RR

*/