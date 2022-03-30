using System;
using System.Collections.Generic;
using System.Text;

namespace ProbabilisticAssignmentLanguage
{
    /* SYNTAX:

    boolean (O)::= true | false
    integer (N)::= strings of digits
    TypeDefinition (IB, IE, and IP)::= strings of non-key-symbol characters, starting with a non-digit that are not key-words

    Command (C)::=
    skip
    | C1 ; C2
    | if B then C1 else C2 ; end
    | while B do C ; end
    | observe PB
    | bool IB = B
    | int IE = E
    | prob IP = P
    | out P

    Operator (A)::= + | - | * | / | %

    ArithmeticBooleanExpression (B)::= O | IB | (B) | B1 & B2 | B1 "|" B2 | !B | E1 > E2 | E1 < E2 | E1=E2

    ProbabilisticBooleanExpressio (PB)::= B | (PB) | PB1 & PB2 | PB1 "|" PB2 | !PB | P > E | P < E | P=E | E > P | E < P | E=P | P1 > P2 | P1 < P2 | P1=P2

    ArithmeticExpression (E)::= N | IE | (E) | E1 A E2

    ProbabilisticExpression (P)::= R | IP | (P) | P1 A P2 | P A E | E A P

    ProbConstructor (R)::= [ RR ]

    ProbElement (RR)::= (E, E) | (E, E), RR
    */

    public interface SyntaxTree { }

    // Command (C)
    public interface Command: SyntaxTree { }
    // C1 ; C2
    public struct Semicolon : Command
    {
        public Command Command1;
        public Command Command2;
    }
    // if B then C1 else C2 ; end
    public struct IfThenElse : Command
    {
        public ArithBoolExp BoolIf;
        public Command CommandThen;
        public Command CommandElse;
    }
    // while B do C ; end
    public struct WhileDo : Command
    {
        public ArithBoolExp BoolWhile;
        public Command Command2;
    }
    // observe PB
    public struct Observe : Command
    {
        public ProbBoolExp ProbBool;
    }
    // bool IB = B
    public struct DeclareBool : Command
    {
        public string VarName;
        public ArithBoolExp Bool;
    }
    // int IE = E
    public struct DeclareInt : Command
    {
        public string VarName;
        public ArithExp Integer;
    }
    // prob IP = P
    public struct DeclareProb : Command
    {
        public string VarName;
        public ProbExp Prob;
    }
    // out P
    public struct Out : Command
    {
        public ProbExp ProbOut;
    }

    // Probabilistic Boolean Expression (PB)
    public interface ProbBoolExp : SyntaxTree { }
    // ! PB
    public struct ProbBoolNot : ArithBoolExp
    {
        public ProbBoolExp ProbBoolToReverse;
    }
    // PB1 & PB2  |  PB1 | PB2
    public struct ProbBoolCompareProbBools : ArithBoolExp
    {
        public Token Operator;
        public ProbBoolExp ProbBoolExp1;
        public ProbBoolExp ProbBoolExp2;
    }
    // P1 > P2 | P1 < P2 | P1 = P2
    public struct ProbBoolCompareProbs : ArithBoolExp
    {
        public Token Operator;
        public ProbExp ProbExp1;
        public ProbExp ProbExp2;
    }
    // P > E | P < E | P = E | E > P | E < P | E = P
    public struct ProbBoolCompareProbToArith : ArithBoolExp
    {
        public Token Operator;
        public ProbExp ProbExp;
        public ArithExp ArithExp;
    }

    // Boolean Expression (B)
    public interface ArithBoolExp : ProbBoolExp, SyntaxTree { }
    // O
    public struct ArithBoolMonad : ArithBoolExp
    {
        public bool Bool;
    }
    // IB
    public struct ArithBoolVar : ArithBoolExp
    {
        public string VarName;
    }
    // ! B
    public struct ArithBoolNot : ArithBoolExp
    {
        public ArithBoolExp BoolToReverse;
    }
    // B1 & B2  |  B1 | B2
    public struct ArithBoolCompareBools : ArithBoolExp
    {
        public Token Operator;
        public ArithBoolExp ArithBoolExp1;
        public ArithBoolExp ArithBoolExp2;
    }
    // E1 > E2 | E1 < E2 | E1 = E2
    public struct ArithBoolCompareAriths : ArithBoolExp
    {
        public Token Operator;
        public ArithExp ArithExp1;
        public ArithExp ArithExp2;
    }

    // Probabilistic Expression (P)
    public interface ProbExp : SyntaxTree { }
    // RR
    public struct ProbElement
    {
        public int Value { get; }
        public int Weight { get; set; }
    }
    // R
    public struct Prob : ProbExp
    {
        public int Index;
    }
    // IP
    public struct ProbVar : ProbExp
    {
        public string VarName;
    }
    // P1 A P2
    public struct ProbCombineProbs : ProbExp
    {
        public Token Operator;
        public ProbExp ProbExp1;
        public ProbExp ProbExp2;
    }
    // P A E | E A P
    public struct ProbCombineProbAndArith : ProbExp
    {
        public Token Operator;
        public ProbExp ProbExp;
        public ArithExp ArithExp;
    }

    // Arithmetic Expression (E)
    public interface ArithExp : SyntaxTree { }
    // N
    public struct Integer : ArithExp
    {
        public int Value;
    }
    // IE
    public struct ArithVar : ArithExp
    {
        public string VarName;
    }
    // E1 A E2
    public struct IntegerCombine : ArithExp
    {
        public Token Operator;
        public ArithExp ArithExp1;
        public ArithExp ArithExp2;
    }
}
