using System;
using System.Collections.Generic;

namespace Compiler.Lexer
{
    public partial class Tokenizer
    {
        private static readonly Dictionary<string, TokenKind> OperatorToTokenKind =
            new Dictionary<string, TokenKind>(StringComparer.OrdinalIgnoreCase)
            {
                ["="] = TokenKind.Assignment,
                ["A+B"] = TokenKind.Addition,
                ["A-B"] = TokenKind.Subtraction,
                ["+A"] = TokenKind.UnaryPlus,
                ["-A"] = TokenKind.UnaryMinus,
                ["*"] = TokenKind.Multiplication,
                ["/"] = TokenKind.Division,
                ["%"] = TokenKind.Modulo,
                ["=="] = TokenKind.EqualTo,
                ["!="] = TokenKind.NotEqualTo,
                [">"] = TokenKind.GeaterThan,
                ["<"] = TokenKind.LessThan,
                [">="] = TokenKind.GreaterThanOrEqualTo,
                ["<="] = TokenKind.LessThanOrEqualTo,
                ["!"] = TokenKind.LogicalNOT,
                ["&&"] = TokenKind.LogicalAND,
                ["||"] = TokenKind.LogicalOR,
                ["A++"] = TokenKind.PostfixPlus,
                ["A--"] = TokenKind.PostfixMinus,
                ["++A"] = TokenKind.PrefixPlus,
                ["--A"] = TokenKind.PrefixMinus,
                ["~"] = TokenKind.BitwiseNOT,
                ["&"] = TokenKind.BitwiseAND,
                ["|"] = TokenKind.BitwiseOR,
                ["^"] = TokenKind.BitwiseXOR,
                ["<<"] = TokenKind.BitwiseLeftShift,
                [">>"] = TokenKind.BitwiseRightShift,
                ["+="] = TokenKind.AdditionAssignment,
                ["-="] = TokenKind.SubtractionAssignment,
                ["*="] = TokenKind.MultiplicationAssignment,
                ["/="] = TokenKind.DivisionAssignment,
                ["%="] = TokenKind.ModuloAssignment,
                ["&="] = TokenKind.BitwiseANDAssignment,
                ["|="] = TokenKind.BitwiseORAssignment,
                ["^="] = TokenKind.BitwiseXORAssignment,
                ["<<="] = TokenKind.BitwiseLeftShiftAssignment,
                [">>="] = TokenKind.BitwiseRightShiftAssignment,
            };

    }
}
