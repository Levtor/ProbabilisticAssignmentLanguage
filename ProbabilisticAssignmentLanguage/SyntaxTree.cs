using System.Collections.Generic;

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
    // skip
    public struct Skip : Command { }
    // C1 ; C2
    public struct Semicolon : Command
    {
        public Command Command1;
        public Command Command2;
    }
    // if B then C1 else C2 ; end
    public struct IfThenElse : Command
    {
        public Exp BoolIf;
        public Command CommandThen;
        public Command CommandElse;
    }
    // while B do C ; end
    public struct WhileDo : Command
    {
        public Exp BoolWhile;
        public Command Command;
    }
    // observe PB
    public struct Observe : Command
    {
        public Exp ProbBool;
    }
    // bool IB = B
    public struct DeclareBool : Command
    {
        public string VarName;
        public Exp Bool;
    }
    // int IE = E
    public struct DeclareInt : Command
    {
        public string VarName;
        public Exp Integer;
    }
    // prob IP = P
    public struct DeclareProb : Command
    {
        public string VarName;
        public Exp Prob;
    }
    // out P
    public struct Out : Command
    {
        public Exp ProbOut;
    }
    
    public interface Exp : SyntaxTree { }
    public struct Not : Exp
    {
        public Exp ExpToReverse;
    }
    public struct ExpCombine : Exp
    {
        public Token Operator;
        public Exp Exp1;
        public Exp Exp2;
    }
    public struct Var : Exp
    {
        public string VarName;
    }
    public struct ArithMonad : Exp
    {
        public int Value;
    }
    public struct BoolMonad : Exp
    {
        public bool Bool;
    }
    public struct ProbMonad : Exp
    {
        public List<ProbElement> Elements;
    }
    public struct ProbElement
    {
        public Exp Value;
        public Exp Weight;
    }
}
