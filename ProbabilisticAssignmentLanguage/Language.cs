using System;
using System.Collections.Generic;
using System.Text;

namespace ProbabilisticAssignmentLanguage
{
    public class Language
    {
        /// <summary>
        /// States for the tokenizer
        /// </summary>
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
        private void Tokenizer(string input, Queue<Token> tokens, Queue<long> numbers, Queue<string> variableNames)
        {
            tokens.Clear();
            numbers.Clear();
            variableNames.Clear();
            long numToAdd = 0;
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
                            numToAdd += (c - '0');
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
                            numToAdd += (c - '0');
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
                case "skip":
                    return Token.Skip;
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
        private Command ParseCommand(Queue<Token> tokens, Queue<long> numbers, Queue<string> variableNames)
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
                    commandTree = new Observe { ProbBool = ParseExp(tokens, numbers, variableNames) };
                    break;
                case Token.Out:
                    List<Exp> outProbs = new List<Exp>();
                    if (tokens.Dequeue() != Token.LBrack) throw new Exception("'[' expected but not found while parsing out command");
                    bool moreToGo = true;
                    while (moreToGo)
                    {
                        outProbs.Add(ParseExp(tokens, numbers, variableNames));
                        Token miniNext = tokens.Dequeue();
                        if (miniNext == Token.RBrack) moreToGo = false;
                        else if (miniNext != Token.Comma) throw new Exception("']' or ',' expected but not found while parsing out command");
                    }
                    commandTree = new Out { ProbsOut = outProbs };
                    break;
                case Token.If:
                    IfThenElse ite = new IfThenElse();
                    ite.BoolIf = ParseExp(tokens, numbers, variableNames);
                    if (tokens.Dequeue() != Token.Then) throw new Exception("'Then' expected but not found");
                    ite.CommandThen = ParseCommand(tokens, numbers, variableNames);
                    if (tokens.Dequeue() != Token.Else) throw new Exception("'Else' expected but not found");
                    ite.CommandElse = ParseCommand(tokens, numbers, variableNames);
                    if (tokens.Dequeue() != Token.End) throw new Exception("'End' expected but not found (if-then-else)");
                    commandTree = ite;
                    break;
                case Token.While:
                    WhileDo wd = new WhileDo();
                    wd.BoolWhile = ParseExp(tokens, numbers, variableNames);
                    if (tokens.Dequeue() != Token.Do) throw new Exception("'Do' expected but not found");
                    wd.Command = ParseCommand(tokens, numbers, variableNames);
                    if (tokens.Dequeue() != Token.End) throw new Exception("'End' expected but not found (while-do)");
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
                {
                    if (tokens.Count == 0) return commandTree;
                    else
                    {
                        next = tokens.Peek();
                        if (next == Token.End || next == Token.Else) return commandTree;
                        else commandTree = new Semicolon { Command1 = commandTree, Command2 = ParseCommand(tokens, numbers, variableNames) };
                    }
                }
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
        private Exp ParseExp(Queue<Token> tokens, Queue<long> numbers, Queue<string> variableNames)
        {
            Token next = tokens.Dequeue();
            Exp expTree = null;
            switch (next)
            {
                case Token.VarName:
                    expTree = new Var { VarName = variableNames.Dequeue() };
                    break;
                case Token.IntVal:
                    expTree = new ArithMonad { Number = numbers.Dequeue() };
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
        private ProbMonad ParseProbMonad(Queue<Token> tokens, Queue<long> numbers, Queue<string> variableNames)
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

        /// <summary>
        /// Interprets a command syntax tree
        /// </summary>
        /// <param name="tree">the expression syntax tree being evaluated</param>
        /// <param name="variables">a dictionary of the variables defined in the program the expression is being evaluated in</param>
        /// <param name="masterProbList">a list of all the base probs for future reference</param>
        /// <param name="masterObserveList">a list of all the probabilistic boolean expressions in observe statements</param>
        /// <param name="masterOutList">a list of all the probs in out statements</param>
        private void InterpretCommand(Command tree, Dictionary<string, Exp> variables,
            List<ProbMonad> masterProbList, List<Exp> masterObserveList, List<List<Exp>> masterOutList)
        {
            Exp exp;
            ExpressionType et;
            if (tree is Skip skip)
            {
                return;
            }
            else if (tree is Semicolon semicolon)
            {
                InterpretCommand(semicolon.Command1, variables, masterProbList, masterObserveList, masterOutList);
                InterpretCommand(semicolon.Command2, variables, masterProbList, masterObserveList, masterOutList);
            }
            else if (tree is IfThenElse ift)
            {
                et = InterpretExpression(ift.BoolIf, variables, masterProbList, out exp);
                if (et == ExpressionType.Bool && exp is BoolMonad bm)
                {
                    if (bm.Bool) InterpretCommand(ift.CommandThen, variables, masterProbList, masterObserveList, masterOutList);
                    else InterpretCommand(ift.CommandElse, variables, masterProbList, masterObserveList, masterOutList);
                }
                else throw new Exception("expression that is not boolean used for if-then-else statement");
            }
            else if (tree is WhileDo wd)
            {
                et = InterpretExpression(wd.BoolWhile, variables, masterProbList, out exp);
                if (et == ExpressionType.Bool && exp is BoolMonad bm)
                {
                    if (bm.Bool)
                    {
                        InterpretCommand(wd.Command, variables, masterProbList, masterObserveList, masterOutList);
                        InterpretCommand(tree, variables, masterProbList, masterObserveList, masterOutList);
                    }
                }
                else throw new Exception("expression that is not boolean used for while-do statement");
            }
            else if (tree is DeclareInt di)
            {
                et = InterpretExpression(di.Integer, variables, masterProbList, out exp);
                if (et == ExpressionType.Arith && exp is ArithMonad)
                {
                    if (variables.TryGetValue(di.VarName, out _)) variables[di.VarName] = exp;
                    else variables.Add(di.VarName, exp);
                }
                else throw new Exception("int variable declared with expression that is not an int");
            }
            else if (tree is DeclareBool db)
            {
                et = InterpretExpression(db.Bool, variables, masterProbList, out exp);
                if (et == ExpressionType.Bool && exp is BoolMonad)
                {
                    if (variables.TryGetValue(db.VarName, out _)) variables[db.VarName] = exp;
                    else variables.Add(db.VarName, exp);
                }
                else throw new Exception("bool variable declared with expression that is not a bool");
            }
            else if (tree is DeclareProb dp)
            {
                et = InterpretExpression(dp.Prob, variables, masterProbList, out exp);
                if (et == ExpressionType.Prob)
                {
                    if (variables.TryGetValue(dp.VarName, out _)) variables[dp.VarName] = exp;
                    else variables.Add(dp.VarName, exp);
                }
                else throw new Exception("prob variable declared with expression that is not a prob");
            }
            else if (tree is Observe observe)
            {
                et = InterpretExpression(observe.ProbBool, variables, masterProbList, out exp);
                if (et == ExpressionType.PBool)
                {
                    masterObserveList.Add(exp);
                }
                else throw new Exception("observe statement used on expression that is not a boolean expression involving a prob");
            }
            else if (tree is Out outCommand)
            {
                List<Exp> toAddToMasterOutList = new List<Exp>();
                foreach (Exp expression in outCommand.ProbsOut)
                {
                    et = InterpretExpression(expression, variables, masterProbList, out exp);
                    if (et == ExpressionType.Prob)
                    {
                        toAddToMasterOutList.Add(exp);
                    }
                    else throw new Exception("out statement has expression that is not a prob");
                }
                masterOutList.Add(toAddToMasterOutList);
            }
            else throw new Exception("command interpreter is trying to interpret something that isn't a command");
        }

        /// <summary>
        /// Return enum for the expression interpreter that signifies the type of the expression
        /// </summary>
        enum ExpressionType
        {
            Arith,
            Bool,
            Prob,
            PBool
        }
        /// <summary>
        /// Interprets an expression syntax tree
        /// </summary>
        /// <param name="tree">the expression syntax tree being evaluated</param>
        /// <param name="variables">a dictionary of the variables defined in the program the expression is being evaluated in</param>
        /// <param name="masterProbList">a list of all the base probs for future reference</param>
        /// <param name="value">the value the expression evaluates to</param>
        /// <returns>the type of the expression</returns>
        private ExpressionType InterpretExpression(Exp tree, Dictionary<string, Exp> variables, List<ProbMonad> masterProbList, out Exp value)
        {
            if (tree is ExpCombine combine)
            {
                Exp subValue1;
                Exp subValue2;
                ExpressionType subType1 = InterpretExpression(combine.Exp1, variables, masterProbList, out subValue1);
                ExpressionType subType2 = InterpretExpression(combine.Exp2, variables, masterProbList, out subValue2);
                switch ((subType1, subType2, combine.Operator))
                {

                    case (ExpressionType.Arith, ExpressionType.Arith, Token.Plus):
                        value = new ArithMonad { Number = (subValue1 as ArithMonad?).Value.Number + (subValue2 as ArithMonad?).Value.Number };
                        return ExpressionType.Arith;
                    case (ExpressionType.Arith, ExpressionType.Arith, Token.Minus):
                        value = new ArithMonad { Number = (subValue1 as ArithMonad?).Value.Number - (subValue2 as ArithMonad?).Value.Number };
                        return ExpressionType.Arith;
                    case (ExpressionType.Arith, ExpressionType.Arith, Token.Mult):
                        value = new ArithMonad { Number = (subValue1 as ArithMonad?).Value.Number * (subValue2 as ArithMonad?).Value.Number };
                        return ExpressionType.Arith;
                    case (ExpressionType.Arith, ExpressionType.Arith, Token.Div):
                        value = new ArithMonad { Number = (subValue1 as ArithMonad?).Value.Number / (subValue2 as ArithMonad?).Value.Number };
                        return ExpressionType.Arith;
                    case (ExpressionType.Arith, ExpressionType.Arith, Token.Mod):
                        value = new ArithMonad { Number = (subValue1 as ArithMonad?).Value.Number % (subValue2 as ArithMonad?).Value.Number };
                        return ExpressionType.Arith;

                    case (ExpressionType.Arith, ExpressionType.Arith, Token.Greater):
                        value = new BoolMonad { Bool = (subValue1 as ArithMonad?).Value.Number > (subValue2 as ArithMonad?).Value.Number };
                        return ExpressionType.Bool;
                    case (ExpressionType.Arith, ExpressionType.Arith, Token.Lesser):
                        value = new BoolMonad { Bool = (subValue1 as ArithMonad?).Value.Number < (subValue2 as ArithMonad?).Value.Number };
                        return ExpressionType.Bool;
                    case (ExpressionType.Arith, ExpressionType.Arith, Token.Equal):
                        value = new BoolMonad { Bool = (subValue1 as ArithMonad?).Value.Number == (subValue2 as ArithMonad?).Value.Number };
                        return ExpressionType.Bool;

                    case (ExpressionType.Bool, ExpressionType.Bool, Token.And):
                        value = new BoolMonad { Bool = (subValue1 as BoolMonad?).Value.Bool && (subValue2 as BoolMonad?).Value.Bool };
                        return ExpressionType.Bool;
                    case (ExpressionType.Bool, ExpressionType.Bool, Token.Or):
                        value = new BoolMonad { Bool = (subValue1 as BoolMonad?).Value.Bool || (subValue2 as BoolMonad?).Value.Bool };
                        return ExpressionType.Bool;

                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Plus):
                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Minus):
                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Mult):
                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Div):
                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Mod):
                    case (ExpressionType.Prob, ExpressionType.Arith, Token.Plus):
                    case (ExpressionType.Prob, ExpressionType.Arith, Token.Minus):
                    case (ExpressionType.Prob, ExpressionType.Arith, Token.Mult):
                    case (ExpressionType.Prob, ExpressionType.Arith, Token.Div):
                    case (ExpressionType.Prob, ExpressionType.Arith, Token.Mod):
                    case (ExpressionType.Arith, ExpressionType.Prob, Token.Plus):
                    case (ExpressionType.Arith, ExpressionType.Prob, Token.Minus):
                    case (ExpressionType.Arith, ExpressionType.Prob, Token.Mult):
                    case (ExpressionType.Arith, ExpressionType.Prob, Token.Div):
                    case (ExpressionType.Arith, ExpressionType.Prob, Token.Mod):
                        value = new ExpCombine
                        {
                            Operator = combine.Operator,
                            Exp1 = subValue1,
                            Exp2 = subValue2
                        };
                        return ExpressionType.Prob;

                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Greater):
                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Lesser):
                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Equal):
                    case (ExpressionType.Prob, ExpressionType.Arith, Token.Greater):
                    case (ExpressionType.Prob, ExpressionType.Arith, Token.Lesser):
                    case (ExpressionType.Prob, ExpressionType.Arith, Token.Equal):
                    case (ExpressionType.Arith, ExpressionType.Prob, Token.Greater):
                    case (ExpressionType.Arith, ExpressionType.Prob, Token.Lesser):
                    case (ExpressionType.Arith, ExpressionType.Prob, Token.Equal):

                    case (ExpressionType.PBool, ExpressionType.PBool, Token.And):
                    case (ExpressionType.PBool, ExpressionType.PBool, Token.Or):
                    case (ExpressionType.PBool, ExpressionType.Bool, Token.And):
                    case (ExpressionType.PBool, ExpressionType.Bool, Token.Or):
                    case (ExpressionType.Bool, ExpressionType.PBool, Token.And):
                    case (ExpressionType.Bool, ExpressionType.PBool, Token.Or):
                        value = new ExpCombine
                        {
                            Operator = combine.Operator,
                            Exp1 = subValue1,
                            Exp2 = subValue2
                        };
                        return ExpressionType.PBool;
                    default:
                        throw new Exception("Binary operator applied to an inappropriate type pair");
                }
            }
            else if (tree is Var var)
            {
                if (variables.TryGetValue(var.VarName, out value))
                {
                    if (value is ArithMonad) return ExpressionType.Arith;
                    else if (value is BoolMonad) return ExpressionType.Bool;
                    else return ExpressionType.Prob;
                }
                else throw new Exception("variable name referenced that doesn't exist: " + var.VarName);
            }
            else if (tree is Not not)
            {
                Exp subValue;
                ExpressionType type = InterpretExpression(not.ExpToReverse, variables, masterProbList, out subValue);
                if (type == ExpressionType.Bool && subValue is BoolMonad boolSubValue)
                {
                    value = new BoolMonad { Bool = !boolSubValue.Bool };
                    return ExpressionType.Bool;
                }
                else if (type == ExpressionType.PBool)
                {
                    value = new Not { ExpToReverse = subValue };
                    return ExpressionType.PBool;
                }
                else throw new Exception("'!' operator applied to non-boolean expression");
            }
            else if (tree is ArithMonad arith)
            {
                value = arith;
                return ExpressionType.Arith;
            }
            else if (tree is BoolMonad boolm)
            {
                value = boolm;
                return ExpressionType.Bool;
            }
            else if (tree is ProbMonad prob)
            {
                List<ProbElement> elts = new List<ProbElement>();
                foreach(ProbElement elt in prob.Elements)
                {
                    ExpressionType valExpType = InterpretExpression(elt.Value, variables, masterProbList, out Exp eltVal);
                    ExpressionType weightExpType = InterpretExpression(elt.Weight, variables, masterProbList, out Exp eltWeight);
                    if (valExpType == ExpressionType.Arith && weightExpType == ExpressionType.Arith)
                    {
                        elts.Add(new ProbElement
                        {
                            Value = eltVal,
                            Weight = eltWeight
                        });
                    }
                    else throw new Exception("prob variable decared using non-arithmetic expression");
                }
                ProbMonad probVal = new ProbMonad { Elements = elts, Index = (uint)masterProbList.Count };
                masterProbList.Add(probVal);
                value = probVal;
                return ExpressionType.Prob;
            }
            else throw new Exception("expression interpreter is trying to interpret something that isn't an expression");
        }

        /// <summary>
        /// Calculates the output of a program given its basic prob monads, observe statements, and out statements
        /// </summary>
        /// <param name="masterProbList">a list of basic prob monads</param>
        /// <param name="masterObserveList">a list of observe statements</param>
        /// <param name="masterOutList">a list of out statements</param>
        /// <returns>a list of dictionaries representing each out statement's probability distribution when subject to all observe statements</returns>
        private Dictionary<string, ulong>[] CalculateOutput(List<ProbMonad> masterProbList, List<Exp> masterObserveList, List<List<Exp>> masterOutList)
        {
            Dictionary<string, ulong>[] output = new Dictionary<string, ulong>[masterOutList.Count];
            for(int k = 0; k < masterOutList.Count; k++)
            {
                output[k] = new Dictionary<string, ulong>();
            }
            uint[] indices = new uint[masterProbList.Count];
            ulong probNumber = 1;
            for (int i = 0; i < masterProbList.Count; i++)
            {
                probNumber *= (ulong)masterProbList[i].Elements.Count;
            }
            for (ulong i = 0; i < probNumber; i++)
            {
                ulong copy = i;
                for (int j = 0; j < masterProbList.Count; j++)
                {
                    uint n = (uint)masterProbList[j].Elements.Count; 
                    indices[j] = (uint)(copy % n);
                    copy /= n;
                }

                bool survivedObserves = true;
                foreach (Exp observe in masterObserveList)
                {
                    if (InterpretProbExpression(observe, indices, masterProbList, out ExpressionType expressionType) is BoolReducedProb brp)
                    {
                        if (!brp.Bool)
                        {
                            survivedObserves = false;
                            break;
                        }
                    }
                    else throw new Exception("observe list has an expression in it that isn't a probabilistic boolean expression");
                }
                if (survivedObserves)
                {
                    ulong weight = 1;
                    for (int k = 0; k < masterProbList.Count; k++)
                    {
                        if (masterProbList[k].Elements[(int)indices[k]].Weight is ArithMonad weightExp)
                        {
                            weight *= (ulong)weightExp.Number;
                        }
                        else throw new Exception("Expression representing weight in a prob in the master prob list is not the correct type");
                    }

                    for (int k = 0; k < masterOutList.Count; k++)
                    {
                        StringBuilder key = new StringBuilder();
                        foreach (Exp outProb in masterOutList[k])
                        {
                            if (InterpretProbExpression(outProb, indices, masterProbList, out ExpressionType type) is ElementReducedProb erp)
                            {
                                key.Append(erp.Value);
                                key.Append(", ");
                            }
                            else throw new Exception("out list has an expression in it that isn't a prob");
                        }
                        key.Remove(key.Length - 2, 2);
                        string keyString = key.ToString();
                        if (output[k].TryGetValue(keyString, out _)) output[k][keyString] += weight;
                        else output[k].Add(keyString, weight);
                    }
                }
            }
            
            for (int k = 0; k < masterOutList.Count; k++)
            {
                var keys = new List<string>(output[k].Keys);
                ulong runningGCD = 0;
                foreach (string key in keys)
                {
                    runningGCD = GCD(runningGCD, output[k][key]);
                }
                foreach (string key in keys)
                {
                    output[k][key] /= runningGCD;
                }
            }
            
            return output;
        }
        private ulong GCD(ulong a, ulong b)
        {
            if (a == 0) return b;
            else if (b == 0) return a;
            else return GCD(b, a % b);
        }

        /// <summary>
        /// the return type of the below function; it can be either a boolean or a value-weight pair
        /// </summary>
        interface ReducedProb { };
        /// <summary>
        /// the boolean version of ReducedProb, the return type of the below function
        /// </summary>
        struct BoolReducedProb : ReducedProb
        {
            public bool Bool;
        }
        /// <summary>
        /// the value-weight pair version of ReducedProb, the return type of the below function
        /// </summary>
        struct ElementReducedProb : ReducedProb
        {
            public long Value;
        }
        /// <summary>
        /// Calculates the boolean or value-weight pair value of a probabilistic expression at a specific point
        /// </summary>
        /// <param name="tree">the probabilistic expression</param>
        /// <param name="indices">an array describing the point in the probabilistic expression being examined</param>
        /// <param name="masterProbList">the list of basic probabilistic distributions defined in the program</param>
        /// <param name="type">an out variable that designates whether the returned expression is a boolean or a value-weight pair</param>
        /// <returns>the boolean or value-weight pair that the given probabilistic expression evaluates to at the given point</returns>
        private ReducedProb InterpretProbExpression(Exp tree, uint[] indices, List<ProbMonad> masterProbList, out ExpressionType type)
        {
            if (tree is ExpCombine combine)
            {
                ExpressionType subType1;
                ExpressionType subType2;
                ReducedProb subValue1 = InterpretProbExpression(combine.Exp1, indices, masterProbList, out subType1);
                ReducedProb subValue2 = InterpretProbExpression(combine.Exp2, indices, masterProbList, out subType2);
                switch ((subType1, subType2, combine.Operator))
                {

                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Plus):
                        type = ExpressionType.Prob;
                        return new ElementReducedProb
                        {
                            Value = (subValue1 as ElementReducedProb?).Value.Value + (subValue2 as ElementReducedProb?).Value.Value,
                        };
                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Minus):
                        type = ExpressionType.Prob;
                        return new ElementReducedProb
                        {
                            Value = (subValue1 as ElementReducedProb?).Value.Value - (subValue2 as ElementReducedProb?).Value.Value,
                        };
                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Mult):
                        type = ExpressionType.Prob;
                        return new ElementReducedProb
                        {
                            Value = (subValue1 as ElementReducedProb?).Value.Value * (subValue2 as ElementReducedProb?).Value.Value,
                        };
                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Div):
                        type = ExpressionType.Prob;
                        return new ElementReducedProb
                        {
                            Value = (subValue1 as ElementReducedProb?).Value.Value / (subValue2 as ElementReducedProb?).Value.Value,
                        };
                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Mod):
                        type = ExpressionType.Prob;
                        return new ElementReducedProb
                        {
                            Value = (subValue1 as ElementReducedProb?).Value.Value % (subValue2 as ElementReducedProb?).Value.Value,
                        };

                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Greater):
                        type = ExpressionType.Bool;
                        return new BoolReducedProb { Bool = (subValue1 as ElementReducedProb?).Value.Value > (subValue2 as ElementReducedProb?).Value.Value };
                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Lesser):
                        type = ExpressionType.Bool;
                        return new BoolReducedProb { Bool = (subValue1 as ElementReducedProb?).Value.Value < (subValue2 as ElementReducedProb?).Value.Value };
                    case (ExpressionType.Prob, ExpressionType.Prob, Token.Equal):
                        type = ExpressionType.Bool;
                        return new BoolReducedProb { Bool = (subValue1 as ElementReducedProb?).Value.Value == (subValue2 as ElementReducedProb?).Value.Value };

                    case (ExpressionType.Bool, ExpressionType.Bool, Token.And):
                        type = ExpressionType.Bool;
                        return new BoolReducedProb { Bool = (subValue1 as BoolReducedProb?).Value.Bool && (subValue2 as BoolReducedProb?).Value.Bool };
                    case (ExpressionType.Bool, ExpressionType.Bool, Token.Or):
                        type = ExpressionType.Bool;
                        return new BoolReducedProb { Bool = (subValue1 as BoolReducedProb?).Value.Bool || (subValue2 as BoolReducedProb?).Value.Bool };
                    default:
                        throw new Exception("Binary operator applied to an inappropriate type pair (probabilistic)");
                }
            }
            else if (tree is Not not)
            {
                ExpressionType subType;
                ReducedProb value = InterpretProbExpression(not.ExpToReverse, indices, masterProbList, out subType);
                if (subType == ExpressionType.Bool && value is BoolReducedProb brp)
                {
                    type = ExpressionType.Bool;
                    return new BoolReducedProb { Bool = !brp.Bool };
                }
                else throw new Exception("'!' operator applied to non-boolean expression (probabilistic)");
            }
            else if (tree is ArithMonad arith)
            {
                type = ExpressionType.Prob;
                return new ElementReducedProb { Value = arith.Number };
            }
            else if (tree is BoolMonad boolm)
            {
                type = ExpressionType.Bool;
                return new BoolReducedProb { Bool = boolm.Bool };
            }
            else if (tree is ProbMonad prob)
            {
                type = ExpressionType.Prob;
                ProbElement currentElement = prob.Elements[(int)indices[prob.Index]];
                return new ElementReducedProb
                {
                    Value = (currentElement.Value as ArithMonad?).Value.Number
                };
            }
            else throw new Exception("expression interpreter is trying to interpret something that isn't an expression (probabilistic)");
        }

        public Dictionary<string, ulong>[] RunInterpreterWithExampleProgram(string s)
        {
            Queue<Token> tokens = new Queue<Token>();
            Queue<long> numbers = new Queue<long>();
            Queue<string> variables = new Queue<string>();
            Tokenizer(s, tokens, numbers, variables);
            Command cmd = ParseCommand(tokens, numbers, variables);
            Dictionary<string, Exp> variablesDict = new Dictionary<string, Exp>();
            List<ProbMonad> masterProbList = new List<ProbMonad>();
            List<Exp> masterObserveList = new List<Exp>();
            List<List<Exp>> masterOutList = new List<List<Exp>>();
            InterpretCommand(cmd, variablesDict, masterProbList, masterObserveList, masterOutList);
            return CalculateOutput(masterProbList, masterObserveList, masterOutList);
        }

        public SyntaxTree RunParserWithExampleProgram(string s)
        {
            Queue<Token> tokens = new Queue<Token>();
            Queue<long> numbers = new Queue<long>();
            Queue<string> variables = new Queue<string>();
            Tokenizer(s, tokens, numbers, variables);
            return ParseCommand(tokens, numbers, variables);
        }

        public (Queue<Token>, Queue<long>, Queue<string>) RunTokenizerWithExampleProgram(string s)
        {
            Queue<Token> tokens = new Queue<Token>();
            Queue<long> numbers = new Queue<long>();
            Queue<string> variables = new Queue<string>();
            Tokenizer(s, tokens, numbers, variables);
            return (tokens, numbers, variables);
        }
    }
}
