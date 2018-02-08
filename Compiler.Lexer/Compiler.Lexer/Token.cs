using System;

namespace Compiler.Lexer
{
    public sealed class Token
    {
        public TokenKind Kind { get; }
        public string Lexeme { get; }
        public int LineNumber { get; }
        public int Position { get; }

        public Token(TokenKind kind, string lexeme, int lineNumber, int position)
        {
            Kind = kind;
            Lexeme = lexeme;
            LineNumber = lineNumber;
            Position = position;
        }
    }
}