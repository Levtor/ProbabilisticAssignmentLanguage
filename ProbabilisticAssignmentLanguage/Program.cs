using System;
using System.Collections.Generic;
using System.Text;

namespace ProbabilisticAssignmentLanguage
{
    enum Token
    {
        Skip,
        Semi,
        Observe,
        Out,

        If,
        Then,
        Else,
        While,
        Do,

        BoolDecl,
        IntDecl,
        ProbDecl,

        VarName,
        IntVal,
        BoolTrue,
        BoolFalse,

        Plus,
        Minus,
        Mult,
        Div,
        Mod,

        And,
        Or,
        Not,
        Greater,
        Lesser,
        Equal,

        LParen,
        RParen,
        LBrack,
        RBrack,
        Comma
    }

    class Program
    {
        /// <summary>
        /// List of tokens interpreted from the input string
        /// </summary>
        List<Token> Tokens;

        /// <summary>
        /// List of numbers interpreted from the input string
        /// </summary>
        List<int> NumberFromParser;

        /// <summary>
        /// List of variable name strings interpreted from the input string
        /// </summary>
        List<string> VariableNamesFromParser;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// Takes a string and turns it into 3 lists, one for tokens, numbers, and variable names
        /// </summary>
        /// <param name="input">the string of input code</param>
        private void LexicalAnalysis(string input)
        {
            Tokens = new List<Token>();
            NumberFromParser = new List<int>();
            VariableNamesFromParser = new List<string>();
            bool midNum = false;
            int numToAdd = 0;
            bool midString = false;
            StringBuilder sb = new StringBuilder();

            // Loops through each character in the string
            foreach (char c in input)
            {
                bool addedToString = false;
                //if the character is a digit, extend the saved number, but don't add it to the list
                if (char.IsDigit(c))
                {
                    //and if the parser is in the middle of a string, extend but don't end the string
                    if (midString)
                    {
                        sb.Append(c);
                        midString = true;
                        addedToString = true;
                    }
                    //otherwise extend the saved number and begin or continue a number
                    else
                    {
                        numToAdd *= 10;
                        numToAdd += c - '0';
                        midNum = true;
                    }
                }
                else
                {
                    //if the last character was a number, and this one isn't, the number is ended and added to the
                    //numbers list. Also, the appropriate token is added to the token list
                    if (midNum)
                    {
                        Tokens.Add(Token.IntVal);
                        NumberFromParser.Add(numToAdd);
                        //ends the number and stops tracking it
                        numToAdd = 0;
                        midNum = false;
                    }
                    //adds the appropriate single-character token
                    switch (c)
                    {
                        case ';':
                            Tokens.Add(Token.Semi);
                            break;
                        case '+':
                            Tokens.Add(Token.Plus);
                            break;
                        case '-':
                            Tokens.Add(Token.Minus);
                            break;
                        case '*':
                            Tokens.Add(Token.Mult);
                            break;
                        case '/':
                            Tokens.Add(Token.Div);
                            break;
                        case '%':
                            Tokens.Add(Token.Mod);
                            break;
                        case '&':
                            Tokens.Add(Token.And);
                            break;
                        case '|':
                            Tokens.Add(Token.Or);
                            break;
                        case '!':
                            Tokens.Add(Token.Not);
                            break;
                        case '>':
                            Tokens.Add(Token.Greater);
                            break;
                        case '<':
                            Tokens.Add(Token.Lesser);
                            break;
                        case '=':
                            Tokens.Add(Token.Equal);
                            break;
                        case '(':
                            Tokens.Add(Token.LParen);
                            break;
                        case ')':
                            Tokens.Add(Token.RParen);
                            break;
                        case '[':
                            Tokens.Add(Token.LBrack);
                            break;
                        case ']':
                            Tokens.Add(Token.RBrack);
                            break;
                        case ',':
                            Tokens.Add(Token.Comma);
                            break;
                        //if no appropriate one-character token is detected, begin or continue a string and add the character to said string
                        default:
                            sb.Append(c);
                            midString = true;
                            addedToString = true;
                            break;
                    }
                    //if the last character was part of a string, and this one isn't,
                    if (midString && !addedToString)
                    {
                        //the string is ended
                        string nonSymbol = sb.ToString();
                        //and adds the appropriate multi-character token
                        switch (nonSymbol)
                        {
                            case "observe":
                                Tokens.Add(Token.Observe);
                                break;
                            case "out":
                                Tokens.Add(Token.Out);
                                break;
                            case "if":
                                Tokens.Add(Token.If);
                                break;
                            case "then":
                                Tokens.Add(Token.Then);
                                break;
                            case "else":
                                Tokens.Add(Token.Else);
                                break;
                            case "while":
                                Tokens.Add(Token.While);
                                break;
                            case "do":
                                Tokens.Add(Token.Do);
                                break;
                            case "bool":
                                Tokens.Add(Token.BoolDecl);
                                break;
                            case "int":
                                Tokens.Add(Token.IntDecl);
                                break;
                            case "prob":
                                Tokens.Add(Token.ProbDecl);
                                break;
                            case "true":
                                Tokens.Add(Token.BoolTrue);
                                break;
                            case "false":
                                Tokens.Add(Token.BoolFalse);
                                break;
                            //also adds the string as a variable name to the variables list if appropriate
                            default:
                                Tokens.Add(Token.VarName);
                                VariableNamesFromParser.Add(nonSymbol);
                                break;
                        }
                        //ends the string and stops tracking it
                        sb.Clear();
                        midString = false;
                    }
                }
            }
            //if a number was being tracked at the very end, adds the number and appropriate token to the lists
            if (midNum)
            {
                Tokens.Add(Token.IntVal); 
                NumberFromParser.Add(numToAdd);
            }
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

B::= O | (B) | B1 & B2 | B1 "|" B2 | !B | E1 > E2 | E1 < E2 | E1=E2

PB::= B | (PB) | PB1 & PB2 | PB1 "|" PB2 | !PB |
      P > E | P < E | P=E | E > P | E < P | E==P | P1 > P2 | P1 < P2 | P1=P2

E::= N | (E) | E1 A E2

P::= R | (P) | P1 A P2 | P A E | E A P

R::= [ RR ]

RR::= (E, E) | (E, E), RR

*/


/* Example Program:
 * 
 * prob x = [(0, 1), (1, 1)];
 * prob y = [(0, 1), (1, 1)];
 * prob z = x + y;
 * observe !(z < 1)
 * out z
 */

/* Example of Disjoint Union using already implemented functions
 * (may still be worth the shortcut though)
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