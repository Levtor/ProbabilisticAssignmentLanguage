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

/* C ::-
 * x <- E           (* standard assignment *)
 * x <- \Psi        (* x gets random value according to distribution \Psi *)
 * C1 ; C2          (* sequential composition *)
 * if B then C1 else C2
 * while B do C
 * observe B        (* only allow values that satisfy B *)
*/


/*CONCRETE SYNTAX

P : Program
C : Command
B : Boolean Expression
N : Integer Expression
I : Identifier

P ::= C

C::=
skip
| C1 ; C2
| if B then C1 else C2
| while B do C
| observe B
| L = B

E::= N | L | (E1 + E2) | (E1 * E2) | (E1 - E2)

L::= I | I[E1, ..En]

Ds:= < nothing >

  | D; Ds

D ::= var I
   |  var I [N1,.., Nn]
   |  alias I0 I
   |  alias I0 I[N1, ...Nn]
   |  macro I { C }

N::= strings of digits

I ::=  strings of letters or digits or underscore,
         starting with a letter,
         not including keywords

*/

(*ABSTRACT SYNTAX *)

type Id = string
type Num = int

datatype Exp =
  NumE of Num
| GetE of Left
| PlusE of Exp * Exp
| MinusE of Exp * Exp
| TimesE of Exp * Exp
and Left =
  LocationOf of Id * Exp list

datatype Comm =
  SkipC
| SeqC of Comm * Comm
| OutputC of Exp
| InputC of Left
| IfC of Exp * Comm * Comm
| WhileC of Exp * Comm
| ExpandC of Id
| MutateC of Left * Exp

datatype Decl = 
  DataD of Id * Num list
| AliasD of Id * Id * Num list
| MacroD of Id * Comm 

datatype Prog = 
  ProgP of Decl list * Comm
| ErrorP of string   (* to report errors *)


(*SCANNER
    converts the input string into a list of "tokens" *)

datatype Token =
   SkipT
 | WriteT
 | ReadT
 | IfT
 | WhileT
 | ExpandT
 | VarT
 | AliasT
 | MacroT
 | SemicT
 | CommaT
 | EqualT
 | PlusT
 | MinusT
 | TimesT
 | LparenT
 | RparenT
 | LsquareT
 | RsquareT
 | LcurlyT
 | RcurlyT
 | IdT of string
 | NumT of int

fun print_token token = case token of
   SkipT => "skip"
 | WriteT => "write"
 | ReadT => "read"
 | IfT => "if"
 | WhileT => "while"
 | ExpandT => "expand"
 | VarT => "var"
 | AliasT => "alias"
 | MacroT => "macro"
 | SemicT => ";"
 | CommaT => ","
 | EqualT => "="
 | PlusT => "+"
 | MinusT => "-"
 | TimesT => "*"
 | LparenT => "("
 | RparenT => ")"
 | LsquareT => "["
 | RsquareT => "]"
 | LcurlyT => "{"
 | RcurlyT => "}"
 | (IdT s) => ("identifier " ^ s)
 | (NumT n) => "number"
