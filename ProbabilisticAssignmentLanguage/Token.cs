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

        Not,

        And,
        Or,

        Greater,
        Lesser,
        Equal,

        Plus,
        Minus,
        Mult,
        Div,
        Mod,

        LParen,
        RParen,

        LBrack,
        RBrack,
        Comma
    }
}
