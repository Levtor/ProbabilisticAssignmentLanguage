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
    | out IP

    Operator (A)::= + | - | * | / | %

    ArithmeticBooleanExpression (B)::= O | (B) | B1 & B2 | B1 "|" B2 | !B | E1 > E2 | E1 < E2 | E1=E2

    ProbabilisticBooleanExpressio (PB)::= B | (PB) | PB1 & PB2 | PB1 "|" PB2 | !PB | P > E | P < E | P=E | E > P | E < P | E=P | P1 > P2 | P1 < P2 | P1=P2

    ArithmeticExpression (E)::= N | (E) | E1 A E2

    ProbabilisticExpression (P)::= R | (P) | P1 A P2 | P A E | E A P

    ProbConstructor (R)::= [ RR ]

    ProbElement (RR)::= (E, E) | (E, E), RR
    */

    public interface SyntaxTree { }

    //
    public interface Command: SyntaxTree { }
    public struct Semicolon : Command
    {
        public Command Command1;
        public Command Command2;
    }
    public struct IfThenElse : Command
    {
        public ArithBoolExp BoolIf;
        public Command CommandThen;
        public Command CommandElse;
    }
    public struct WhileDo : Command
    {
        public ArithBoolExp BoolWhile;
        public Command Command2;
    }
    public struct Observe : Command
    {
        public ProbBoolExp ProbBool;
    }
    public struct DeclareBool : Command
    {
        public string VarName;
        public ArithBoolExp Bool;
    }
    public struct DeclareInt : Command
    {
        public string VarName;
        public ArithExp Integer;
    }
    public struct DeclareProb : Command
    {
        public string VarName;
        public ProbExp Prob;
    }
    public struct Out : Command
    {
        public ProbExp ProbOut;
    }

    //
    public interface ProbBoolExp : SyntaxTree { }
    public struct ProbBoolNot : ArithBoolExp
    {
        public ProbBoolExp ProbBoolToReverse;
    }
    public struct ProbBoolCompareProbBools : ArithBoolExp
    {
        public Token Operator;
        public ProbBoolExp ProbBoolExp1;
        public ProbBoolExp ProbBoolExp2;
    }
    public struct ProbBoolCompareProbs : ArithBoolExp
    {
        public Token Operator;
        public ProbExp ProbExp1;
        public ProbExp ProbExp2;
    }
    public struct ProbBoolCompareProbToArith : ArithBoolExp
    {
        public Token Operator;
        public ProbExp ProbExp;
        public ArithExp ArithExp;
    }

    //
    public interface ArithBoolExp : ProbBoolExp, SyntaxTree { }
    public struct ArithBoolMonad : ArithBoolExp
    {
        public bool Bool;
    }
    public struct ArithBoolNot : ArithBoolExp
    {
        public ArithBoolExp BoolToReverse;
    }
    public struct ArithBoolCompareBools : ArithBoolExp
    {
        public Token Operator;
        public ArithBoolExp ArithBoolExp1;
        public ArithBoolExp ArithBoolExp2;
    }
    public struct ArithBoolCompareAriths : ArithBoolExp
    {
        public Token Operator;
        public ArithExp ArithExp1;
        public ArithExp ArithExp2;
    }

    //
    public interface ProbExp : SyntaxTree { }
    public struct ProbElement
    {
        public int Value { get; }
        public int Weight { get; set; }
    }
    public struct Prob : ProbExp
    {
        public List<ProbElement> Value;
    }
    public struct ProbCombineProbs : ProbExp
    {
        public Token Operator;
        public ProbExp ProbExp1;
        public ProbExp ProbExp2;
    }
    public struct ProbCombineProbAndArith : ProbExp
    {
        public Token Operator;
        public ProbExp ProbExp;
        public ArithExp ArithExp;
    }

    //
    public interface ArithExp : SyntaxTree { }
    public struct Integer : ArithExp
    {
        public int Value;
    }
    public struct IntegerCombine : ArithExp
    {
        public Token Operator;
        public ArithExp ArithExp1;
        public ArithExp ArithExp2;
    }
}
