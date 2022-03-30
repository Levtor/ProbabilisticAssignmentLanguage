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

            string s = "prob x = [(0, 1), (1, 1)]; prob y = [(0, 1), (1, 1)]; prob z = x + y; observe !(z < 1); out z";
            Language l = new Language();
            SyntaxTree tree = l.TestParserWithExampleProgram(s);
            Console.WriteLine("Hello World!");
        }
    }

    public class Language
    {
        //List<> MasterProbMonadList;
        //List<> MasterBoolList;
        //List<> MasterOutList;

        enum TokenizerState
        {
            neutral,
            midString,
            midNum
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
            tokens.Clear();
            numbers.Clear();
            variableNames.Clear();
            int numToAdd = 0;
            StringBuilder sb = new StringBuilder();
            TokenizerState state = TokenizerState.neutral;

            foreach (char c in input + ' ')
            {
                switch (state)
                {
                    case TokenizerState.neutral:
                        if (char.IsWhiteSpace(c)) continue;
                        else if (char.IsDigit(c))
                        {
                            numToAdd *= 10;
                            numToAdd += c - '0';
                            state = TokenizerState.midNum;
                        }
                        else
                        {
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
                                default:
                                    sb.Append(c);
                                    state = TokenizerState.midString;
                                    break;
                            }
                        }
                        break;

                    case TokenizerState.midString:
                        if (char.IsWhiteSpace(c))
                        {
                            string nonSymbol = sb.ToString();
                            Token result = StringToToken(nonSymbol);
                            tokens.Enqueue(result);
                            if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                            sb.Clear();
                            state = TokenizerState.neutral;
                        }
                        else
                        {
                            string nonSymbol;
                            Token result;
                            switch (c)
                            {
                                case ';':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.Semi);
                                    state = TokenizerState.neutral;
                                    break;
                                case '+':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.Plus);
                                    state = TokenizerState.neutral;
                                    break;
                                case '-':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.Minus);
                                    state = TokenizerState.neutral;
                                    break;
                                case '*':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.Mult);
                                    state = TokenizerState.neutral;
                                    break;
                                case '/':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.Div);
                                    state = TokenizerState.neutral;
                                    break;
                                case '%':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.Mod);
                                    state = TokenizerState.neutral;
                                    break;
                                case '&':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.And);
                                    state = TokenizerState.neutral;
                                    break;
                                case '|':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.Or);
                                    state = TokenizerState.neutral;
                                    break;
                                case '!':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.Not);
                                    state = TokenizerState.neutral;
                                    break;
                                case '>':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.Greater);
                                    state = TokenizerState.neutral;
                                    break;
                                case '<':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.Lesser);
                                    state = TokenizerState.neutral;
                                    break;
                                case '=':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.Equal);
                                    state = TokenizerState.neutral;
                                    break;
                                case '(':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.LParen);
                                    state = TokenizerState.neutral;
                                    break;
                                case ')':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.RParen);
                                    state = TokenizerState.neutral;
                                    break;
                                case '[':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.LBrack);
                                    state = TokenizerState.neutral;
                                    break;
                                case ']':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.RBrack);
                                    state = TokenizerState.neutral;
                                    break;
                                case ',':
                                    nonSymbol = sb.ToString();
                                    result = StringToToken(nonSymbol);
                                    tokens.Enqueue(result);
                                    if (result == Token.VarName) variableNames.Enqueue(nonSymbol);
                                    sb.Clear();
                                    tokens.Enqueue(Token.Comma);
                                    state = TokenizerState.neutral;
                                    break;
                                default:
                                    sb.Append(c);
                                    break;
                            }
                        }
                        break;

                    case TokenizerState.midNum:
                        if (char.IsDigit(c))
                        {
                            numToAdd *= 10;
                            numToAdd += c - '0';
                        }
                        else if (char.IsWhiteSpace(c))
                        {
                            tokens.Enqueue(Token.IntVal);
                            numbers.Enqueue(numToAdd);
                            numToAdd = 0;
                            state = TokenizerState.neutral;
                        }
                        else
                        {
                            tokens.Enqueue(Token.IntVal);
                            numbers.Enqueue(numToAdd);
                            numToAdd = 0;
                            switch (c)
                            {
                                case ';':
                                    tokens.Enqueue(Token.Semi);
                                    state = TokenizerState.neutral;
                                    break;
                                case '+':
                                    tokens.Enqueue(Token.Plus);
                                    state = TokenizerState.neutral;
                                    break;
                                case '-':
                                    tokens.Enqueue(Token.Minus);
                                    state = TokenizerState.neutral;
                                    break;
                                case '*':
                                    tokens.Enqueue(Token.Mult);
                                    state = TokenizerState.neutral;
                                    break;
                                case '/':
                                    tokens.Enqueue(Token.Div);
                                    state = TokenizerState.neutral;
                                    break;
                                case '%':
                                    tokens.Enqueue(Token.Mod);
                                    state = TokenizerState.neutral;
                                    break;
                                case '&':
                                    tokens.Enqueue(Token.And);
                                    state = TokenizerState.neutral;
                                    break;
                                case '|':
                                    tokens.Enqueue(Token.Or);
                                    state = TokenizerState.neutral;
                                    break;
                                case '!':
                                    tokens.Enqueue(Token.Not);
                                    state = TokenizerState.neutral;
                                    break;
                                case '>':
                                    tokens.Enqueue(Token.Greater);
                                    state = TokenizerState.neutral;
                                    break;
                                case '<':
                                    tokens.Enqueue(Token.Lesser);
                                    state = TokenizerState.neutral;
                                    break;
                                case '=':
                                    tokens.Enqueue(Token.Equal);
                                    state = TokenizerState.neutral;
                                    break;
                                case '(':
                                    tokens.Enqueue(Token.LParen);
                                    state = TokenizerState.neutral;
                                    break;
                                case ')':
                                    tokens.Enqueue(Token.RParen);
                                    state = TokenizerState.neutral;
                                    break;
                                case '[':
                                    tokens.Enqueue(Token.LBrack);
                                    state = TokenizerState.neutral;
                                    break;
                                case ']':
                                    tokens.Enqueue(Token.RBrack);
                                    state = TokenizerState.neutral;
                                    break;
                                case ',':
                                    tokens.Enqueue(Token.Comma);
                                    state = TokenizerState.neutral;
                                    break;
                                default:
                                    sb.Append(c);
                                    state = TokenizerState.midString;
                                    break;
                            }
                        }
                        break;
                }
            }
        }
        private Token StringToToken(string s)
        {
            switch (s)
            {
                case "observe":
                    return Token.Observe;
                case "out":
                    return Token.Out;
                case "if":
                    return Token.If;
                case "then":
                    return Token.Then;
                case "else":
                    return Token.Else;
                case "while":
                    return Token.While;
                case "do":
                    return Token.Do;
                case "end":
                    return Token.End;
                case "bool":
                    return Token.BoolDecl;
                case "int":
                    return Token.IntDecl;
                case "prob":
                    return Token.ProbDecl;
                case "true":
                    return Token.BoolTrue;
                case "false":
                    return Token.BoolFalse;
                default:
                    return Token.VarName;
            }
        }

        /// <summary>
        /// Takes a list of Tokens and associated numbers and strings and converts it into a syntax tree command
        /// </summary>
        /// <param name="tokens">a list for the tokens to be held in</param>
        /// <param name="numbers">a list for the numbers associated with IntVal tokens to be held in</param>
        /// <param name="variableNames">a list for the strings associated with VarName tokens to be held in</param>
        /// <returns>a syntax tree representing a command</returns>
        private Command ParseCommand(Queue<Token> tokens, Queue<int> numbers, Queue<string> variableNames)
        {
            Token next = tokens.Dequeue();
            Command commandTree = null;
            switch (next)
            {
                case Token.Skip:
                    commandTree = new Skip();
                    break;
                case Token.Semi:
                    break;
                case Token.Observe:
                    commandTree = new Observe { ProbBool = ParseExp(tokens, numbers, variableNames)};
                    break;
                case Token.Out:
                    commandTree = new Out { ProbOut = ParseExp(tokens, numbers, variableNames)};
                    break;
                case Token.If:
                    IfThenElse ite = new IfThenElse();
                    ite.BoolIf = ParseExp(tokens, numbers, variableNames);
                    if (tokens.Dequeue() != Token.Then) throw new Exception("'Then' expected but not found");
                    ite.CommandThen = ParseCommand(tokens, numbers, variableNames);
                    if (tokens.Dequeue() != Token.Else) throw new Exception("'Else' expected but not found");
                    ite.CommandElse = ParseCommand(tokens, numbers, variableNames);
                    if (tokens.Dequeue() != Token.Semi) throw new Exception("';' expected but not found (if-then-else)");
                    if (tokens.Dequeue() != Token.Semi) throw new Exception("'End' expected but not found (if-then-else)");
                    commandTree = ite;
                    break;
                case Token.While:
                    WhileDo wd = new WhileDo();
                    wd.BoolWhile = ParseExp(tokens, numbers, variableNames);
                    if (tokens.Dequeue() != Token.Do) throw new Exception("'Do' expected but not found");
                    wd.Command = ParseCommand(tokens, numbers, variableNames);
                    if (tokens.Dequeue() != Token.Semi) throw new Exception("';' expected but not found (while-do)");
                    if (tokens.Dequeue() != Token.Semi) throw new Exception("'End' expected but not found (while-do)");
                    commandTree = wd;
                    break;
                case Token.BoolDecl:
                    DeclareBool db = new DeclareBool();
                    next = tokens.Dequeue();
                    if (next != Token.VarName) throw new Exception("variable name expected but not found (bool declaration)");
                    else db.VarName = variableNames.Dequeue();
                    if (tokens.Dequeue() != Token.Equal) throw new Exception("'=' expected but not found (bool declaration)");
                    db.Bool = ParseExp(tokens, numbers, variableNames);
                    commandTree = db;
                    break;
                case Token.IntDecl:
                    DeclareInt di = new DeclareInt();
                    next = tokens.Dequeue();
                    if (next != Token.VarName) throw new Exception("variable name expected but not found (int declaration)");
                    else di.VarName = variableNames.Dequeue();
                    if (tokens.Dequeue() != Token.Equal) throw new Exception("'=' expected but not found (int declaration)");
                    di.Integer = ParseExp(tokens, numbers, variableNames);
                    commandTree = di;
                    break;
                case Token.ProbDecl:
                    DeclareProb dp = new DeclareProb();
                    next = tokens.Dequeue();
                    if (next != Token.VarName) throw new Exception("variable name expected but not found (prob declaration)");
                    else dp.VarName = variableNames.Dequeue();
                    if (tokens.Dequeue() != Token.Equal) throw new Exception("'=' expected but not found (prob declaration)");
                    dp.Prob = ParseExp(tokens, numbers, variableNames);
                    commandTree = dp;
                    break;
                default:
                    throw new Exception("unexpected token observed while parsing command with code: " + (int)next);
            }
            if (tokens.Count > 0)
            {
                next = tokens.Dequeue();
                if (next == Token.Semi)
                    commandTree = new Semicolon { Command1 = commandTree, Command2 = ParseCommand(tokens, numbers, variableNames) };

            }
            return commandTree;
        }

        /// <summary>
        /// Takes a list of Tokens and associated numbers and strings and converts it into a syntax tree expression
        /// </summary>
        /// <param name="tokens">a list for the tokens to be held in</param>
        /// <param name="numbers">a list for the numbers associated with IntVal tokens to be held in</param>
        /// <param name="variableNames">a list for the strings associated with VarName tokens to be held in</param>
        /// <returns>a syntax tree representing an expression (arithmetic, boolean, probabilistic, etc.)</returns>
        private Exp ParseExp(Queue<Token> tokens, Queue<int> numbers, Queue<string> variableNames)
        {
            Token next = tokens.Dequeue();
            Exp expTree = null;
            switch (next)
            {
                case Token.VarName:
                    expTree = new Var { VarName = variableNames.Dequeue() };
                    break;
                case Token.IntVal:
                    expTree = new ArithMonad { Value = numbers.Dequeue() };
                    break;
                case Token.BoolTrue:
                    expTree = new BoolMonad { Bool = true };
                    break;
                case Token.BoolFalse:
                    expTree = new BoolMonad { Bool = false };
                    break;
                case Token.Not:
                    expTree = new Not { ExpToReverse = ParseExp(tokens, numbers, variableNames) };
                    break;
                case Token.LParen:
                    expTree = ParseExp(tokens, numbers, variableNames);
                    if (tokens.Dequeue() != Token.RParen) throw new Exception("')' expected but not found");
                    break;
                case Token.LBrack:
                    expTree = ParseProbMonad(tokens, numbers, variableNames);
                    break;
                default:
                    throw new Exception("unexpected token observed while parsing expression with code: " + (int)next);
            }
            if (tokens.Count > 0)
            {
                switch (tokens.Peek())
                {
                    case Token.And:
                    case Token.Or:
                    case Token.Greater:
                    case Token.Lesser:
                    case Token.Equal:
                    case Token.Plus:
                    case Token.Minus:
                    case Token.Mult:
                    case Token.Div:
                    case Token.Mod:
                        expTree = new ExpCombine
                        {
                            Operator = tokens.Dequeue(),
                            Exp1 = expTree,
                            Exp2 = ParseExp(tokens, numbers, variableNames)
                        };
                        break;
                }
            }
            return expTree;
        }

        /// <summary>
        /// Takes a list of Tokens and associated numbers and strings and converts it into a syntax tree probabilistic expression monad
        /// </summary>
        /// <param name="tokens">a list for the tokens to be held in</param>
        /// <param name="numbers">a list for the numbers associated with IntVal tokens to be held in</param>
        /// <param name="variableNames">a list for the strings associated with VarName tokens to be held in</param>
        /// <returns>a syntax tree representing a probabilistic expression monad</returns>
        private ProbMonad ParseProbMonad(Queue<Token> tokens, Queue<int> numbers, Queue<string> variableNames)
        {
            ProbMonad newProbMonad = new ProbMonad { Elements = new List<ProbElement>() };
            bool go = true;
            while (go)
            {
                ProbElement pe = new ProbElement();
                if (tokens.Dequeue() != Token.LParen) throw new Exception("'(' expected but not found (probabilistic expression monad definition)");
                pe.Value = ParseExp(tokens, numbers, variableNames);
                if (tokens.Dequeue() != Token.Comma) throw new Exception("',' expected but not found (probabilistic expression monad element definition)");
                pe.Weight = ParseExp(tokens, numbers, variableNames);
                if (tokens.Dequeue() != Token.RParen) throw new Exception("')' expected but not found (probabilistic expression monad definition)");
                newProbMonad.Elements.Add(pe);

                Token next = tokens.Dequeue();
                if (next == Token.RBrack)
                    go = false;
                else if (next != Token.Comma)
                    throw new Exception("unexpected token observed while parsing probabilistic expression monad with code: " + (int)next);
            }
            return newProbMonad;
        }

        public SyntaxTree TestParserWithExampleProgram(string s)
        {
            Queue<Token> tokens = new Queue<Token>();
            Queue<int> numbers = new Queue<int>();
            Queue<string> variables = new Queue<string>();
            Tokenizer(s, tokens, numbers, variables);
            return ParseCommand(tokens, numbers, variables);
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