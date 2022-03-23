using System;
using System.Collections.Generic;
using System.Text;

namespace ProbabilisticAssignmentLanguage
{
    public enum Token
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
        End,

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
}
