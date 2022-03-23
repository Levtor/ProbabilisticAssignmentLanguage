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
        }


        /// <summary>
        /// Takes a string and turns it into 3 lists, one for tokens, numbers, and variable names
        /// </summary>
        /// <param name="input">the string of input code</param>
        /// <param name="tokens">a list for the tokens to be held in</param>
        /// <param name="numbers">a list for the numbers associated with IntVal tokens to be held in</param>
        /// <param name="variableNames">a list for the strings associated with VarName tokens to be held in</param>
        private void Tokenizer(string input, Queue<Token> tokens, Queue<int> numbers, Queue<string> variableNames)
        {
            tokens = new Queue<Token>();
            numbers = new Queue<int>();
            variableNames = new Queue<string>();
            bool midNum = false;
            int numToAdd = 0;
            bool midString = false;
            StringBuilder sb = new StringBuilder();

            // Loops through each character in the string
            foreach (char c in input)
            {
                if (char.IsWhiteSpace(c)) continue;
                bool addedToString = false;
                //if the character is a digit,
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
                        tokens.Enqueue(Token.IntVal);
                        numbers.Enqueue(numToAdd);
                        //ends the number and stops tracking it
                        numToAdd = 0;
                        midNum = false;
                    }
                    //adds the appropriate single-character token
                    switch (c)
                    {
                        case ';':
                            tokens.Enqueue(Token.Semi);
                            break;
                        case '+':
                            tokens.Enqueue(Token.Plus);
                            break;
                        case '-':
                            tokens.Enqueue(Token.Minus);
                            break;
                        case '*':
                            tokens.Enqueue(Token.Mult);
                            break;
                        case '/':
                            tokens.Enqueue(Token.Div);
                            break;
                        case '%':
                            tokens.Enqueue(Token.Mod);
                            break;
                        case '&':
                            tokens.Enqueue(Token.And);
                            break;
                        case '|':
                            tokens.Enqueue(Token.Or);
                            break;
                        case '!':
                            tokens.Enqueue(Token.Not);
                            break;
                        case '>':
                            tokens.Enqueue(Token.Greater);
                            break;
                        case '<':
                            tokens.Enqueue(Token.Lesser);
                            break;
                        case '=':
                            tokens.Enqueue(Token.Equal);
                            break;
                        case '(':
                            tokens.Enqueue(Token.LParen);
                            break;
                        case ')':
                            tokens.Enqueue(Token.RParen);
                            break;
                        case '[':
                            tokens.Enqueue(Token.LBrack);
                            break;
                        case ']':
                            tokens.Enqueue(Token.RBrack);
                            break;
                        case ',':
                            tokens.Enqueue(Token.Comma);
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
                                tokens.Enqueue(Token.Observe);
                                break;
                            case "out":
                                tokens.Enqueue(Token.Out);
                                break;
                            case "if":
                                tokens.Enqueue(Token.If);
                                break;
                            case "then":
                                tokens.Enqueue(Token.Then);
                                break;
                            case "else":
                                tokens.Enqueue(Token.Else);
                                break;
                            case "while":
                                tokens.Enqueue(Token.While);
                                break;
                            case "do":
                                tokens.Enqueue(Token.Do);
                                break;
                            case "end":
                                tokens.Enqueue(Token.End);
                                break;
                            case "bool":
                                tokens.Enqueue(Token.BoolDecl);
                                break;
                            case "int":
                                tokens.Enqueue(Token.IntDecl);
                                break;
                            case "prob":
                                tokens.Enqueue(Token.ProbDecl);
                                break;
                            case "true":
                                tokens.Enqueue(Token.BoolTrue);
                                break;
                            case "false":
                                tokens.Enqueue(Token.BoolFalse);
                                break;
                            //also adds the string as a variable name to the variables list if appropriate
                            default:
                                tokens.Enqueue(Token.VarName);
                                variableNames.Enqueue(nonSymbol);
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
                tokens.Enqueue(Token.IntVal); 
                numbers.Enqueue(numToAdd);
            }
        }


        private SyntaxTree ParserToTokenTree(Queue<Token> tokens, Queue<int> numbers, Queue<string> variableNames)
        {
            Token eval = tokens.Dequeue();
            while (tokens.Count > 0)
            {
                switch (eval)
                {
                    case Token.Skip:
                    case Token.Semi:
                    case Token.Observe:
                    case Token.Out:
                    case Token.If:
                    case Token.Then:
                    case Token.Else:
                    case Token.While:
                    case Token.Do:
                    case Token.BoolDecl:
                    case Token.IntDecl:
                    case Token.ProbDecl:
                    case Token.VarName:
                    case Token.IntVal:
                    case Token.BoolTrue:
                    case Token.BoolFalse:
                    case Token.Plus:
                    case Token.Minus:
                    case Token.Mult:
                    case Token.Div:
                    case Token.Mod:
                    case Token.And:
                    case Token.Or:
                    case Token.Not:
                    case Token.Greater:
                    case Token.Lesser:
                    case Token.Equal:
                    case Token.LParen:
                    case Token.RParen:
                    case Token.LBrack:
                    case Token.RBrack:
                    case Token.End:
                    case Token.Comma:
                }
            }
        }
    }
}



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