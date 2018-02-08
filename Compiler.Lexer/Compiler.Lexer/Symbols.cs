using System;
using System.Collections.Generic;

namespace Compiler.Lexer
{
    public partial class Tokenizer
    {
        private static readonly Dictionary<string, TokenKind> SymbolToTokenKind =
            new Dictionary<string, TokenKind>(StringComparer.OrdinalIgnoreCase)
            {
                ["("] = TokenKind.OpenParenthesis,
                [")"] = TokenKind.CloseParenthesis,
                ["{"] = TokenKind.OpenBrace,
                ["}"] = TokenKind.CloseBrace,
                ["["] = TokenKind.OpenBracket,
                ["]"] = TokenKind.CloseBracket,
                [":"] = TokenKind.Colon,
                [";"] = TokenKind.Semicolon,
                [","] = TokenKind.Comma,
                ["."] = TokenKind.Dot,
                ["?"] = TokenKind.Question,
            };

    }
}
