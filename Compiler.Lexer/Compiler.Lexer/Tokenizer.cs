using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Lexer
{
    public partial class Tokenizer
    {
        private CharStream Stream;
        private Token LastToken;

        public Tokenizer(string source)
        {
            Stream = new CharStream(source);
            LastToken = new Token
                (TokenKind.UnknownToken, string.Empty, 0, 0);
        }

        public Token Get()
        {
            Token get;
            int lineNumber = Stream.CurrentLineNumber;
            int position = Stream.CurrentPosition;

            do
            {
                if (Stream.EOF)
                {
                    return null;
                }
            } while (IsWhiteSpace());

            get = IsSymbol();
            if (get != null)
            {
                return get;
            }

            get = IsOperator();
            if (get != null)
            {
                return get;
            }

            get = IsIdentifier();
            if (get != null)
            {
                return get;
            }

            get = IsCharacterLiteral();
            if (get != null)
            {
                return get;
            }

            get = IsStringLiteral();
            if (get != null)
            {
                return get;
            }

            get = IsNumericLiteral();
            if (get != null)
            {
                return get;
            }

            get = IsAddressLiteral();
            if (get != null)
            {
                return get;
            }

            get = new Token(TokenKind.UnknownToken, Stream.Get().ToString(),
                lineNumber, position);
            return get;

        }

        public Token Peek()
        {
            int line = Stream.CurrentLineNumber;
            int position = Stream.CurrentPosition;
            Token previousToken = LastToken;
            Token peek = Get();
            Stream.CurrentLineNumber = line;
            Stream.CurrentPosition = position;
            LastToken = previousToken;
            return peek;
        }

        private bool IsWhiteSpace()
        {
            bool isWhiteSpace = false;
            while (Stream.Peek() != null && char.IsWhiteSpace((char)Stream.Peek()))
            {
                isWhiteSpace = true;
                Stream.Get();
            }
            return isWhiteSpace;
        }

        private Token IsSymbol()
        {
            int lineNumber = Stream.CurrentLineNumber;
            int position = Stream.CurrentPosition;
            string lexeme = Stream.Peek().ToString();
            TokenKind tokenKind;

            if (SymbolToTokenKind.TryGetValue(lexeme, out tokenKind))
            {
                Stream.Get();
                Token token = new Token(tokenKind, lexeme, lineNumber, position);
                LastToken = token;
                return token;
            }


            return null;
        }

        private Token IsOperator()
        {
            int lineNumber = Stream.CurrentLineNumber;
            int position = Stream.CurrentPosition;
            var lexeme = new StringBuilder();

            char? firstChar = null;
            if (Stream.Peek() == '+' || Stream.Peek() == '-')
            {
                firstChar = Stream.Peek();
            }

            while (Stream.Peek() != null
                   && lineNumber == Stream.CurrentLineNumber
                   && OperatorToTokenKind.ContainsKey(Stream.Peek().ToString())
                   || (Stream.Peek() == '+' && firstChar == '+')
                   || (Stream.Peek() == '-' && firstChar == '-'))
            {
                lexeme.Append(Stream.Get());
            }

            if ((lexeme.ToString() == "+" || lexeme.ToString() == "-")
                & (ushort)LastToken.Kind > 99 && (ushort)LastToken.Kind < 200)
            {
                //A+B or A-B
                lexeme.Insert(0, 'A');
                lexeme.Insert(2, 'B');
            }
            else if (lexeme.ToString() == "+" || lexeme.ToString() == "-")
            {
                //+A or -A
                lexeme.Insert(1, 'A');
            }

            if ((lexeme.ToString() == "++" || lexeme.ToString() == "--")
                & (ushort)LastToken.Kind > 99 && (ushort)LastToken.Kind < 200)
            {
                //A++ or A--
                lexeme.Insert(0, 'A');
            }
            else if (lexeme.ToString() == "++" || lexeme.ToString() == "--")
            {
                //++A or --A
                lexeme.Insert(2, 'A');
            }

            TokenKind tokenKind;
            if (OperatorToTokenKind.TryGetValue(lexeme.ToString(), out tokenKind))
            {
                Token token = new Token
                    (tokenKind, lexeme.ToString(), lineNumber, position);
                LastToken = token;
                return token;
            }


            return null;
        }

        private Token IsIdentifier()
        {
            int lineNumber = Stream.CurrentLineNumber;
            int position = Stream.CurrentPosition;
            var lexeme = new StringBuilder();
            TokenKind tokenKind;
            Token token;

            if (Stream.Peek() == null
                || !(char.IsLetter((char)Stream.Peek()) || Stream.Peek() == '_'))
            {
                return null;
            }

            lexeme.Append(Stream.Get());

            int count = 0;
            while (Stream.Peek() != null
                   && lineNumber == Stream.CurrentLineNumber
                   && (char.IsLetter((char)Stream.Peek())
                       || char.IsDigit((char)Stream.Peek())
                       || Stream.Peek() == '_'))
            {
                count++;
                lexeme.Append(Stream.Get());
            }


            if (KeywordToTokenKind.TryGetValue(lexeme.ToString(), out tokenKind))
            {
                token = new Token
                    (tokenKind, lexeme.ToString(), lineNumber, position);
                LastToken = token;
                return token;
            }

            token = new Token
                (TokenKind.Identifier, lexeme.ToString(), lineNumber, position);
            LastToken = token;
            return token;


        }

        private Token IsCharacterLiteral()
        {
            int lineNumber = Stream.CurrentLineNumber;
            int position = Stream.CurrentPosition;
            var lexeme = new StringBuilder();
            Token token;

            if (Stream.Peek() != '\'')
            {
                return null;
            }

            int singleQuoteCount = 2;
            while (Stream.Peek() != null
                   && singleQuoteCount > 0
                   && lineNumber == Stream.CurrentLineNumber)
            {
                char c = (char)Stream.Get();

                if (c == '\\' && Stream.Peek() == '\'')
                {
                    singleQuoteCount++;
                }

                if (c == '\'')
                {
                    singleQuoteCount--;
                }

                lexeme.Append(c);
            }

            if (singleQuoteCount != 0)
            {
                return null;
            }

            token = new Token
                (TokenKind.CharacterLiteral, lexeme.ToString(), lineNumber, position);
            LastToken = token;
            return token;

        }

        private Token IsStringLiteral()
        {
            int lineNumber = Stream.CurrentLineNumber;
            int position = Stream.CurrentPosition;
            var lexeme = new StringBuilder();
            Token token;

            if (Stream.Peek() != '\"')
            {
                return null;
            }

            int doubleQuoteCount = 2;
            while (Stream.Peek() != null && doubleQuoteCount > 0)
            {
                char c = (char)Stream.Get();

                if (c == '\\' && Stream.Peek() == '\"')
                {
                    doubleQuoteCount++;
                }

                if (c == '\"')
                {
                    doubleQuoteCount--;
                }

                lexeme.Append(c);
            }

            if (doubleQuoteCount != 0)
            {
                return null;
            }

            token = new Token
                (TokenKind.StringLiteral, lexeme.ToString(), lineNumber, position);
            LastToken = token;
            return token;

        }

        private Token IsNumericLiteral()
        {
            int lineNumber = Stream.CurrentLineNumber;
            int position = Stream.CurrentPosition;
            var lexeme = new StringBuilder();
            Token token;

            bool isReal = false;

            if (Stream.Peek() == null || !char.IsDigit((char)Stream.Peek()))
            {
                return null;
            }

            while (Stream.Peek() != null
                   && lineNumber == Stream.CurrentLineNumber
                   && (char.IsDigit((char)Stream.Peek())
                       || Stream.Peek() == '.'
                       || Stream.Peek() == 'M'))
            {
                if (Stream.Peek() == '.' && (!char.IsDigit(Stream.Peek(2)[1]) || isReal))
                {
                    break;
                }

                if (Stream.Peek() == '.')
                {
                    isReal = true;
                }

                lexeme.Append(Stream.Get());
            }

            if (isReal)
            {
                token = new Token
                    (TokenKind.RealLiteral, lexeme.ToString(), lineNumber, position);
            }
            else
            {
                token = new Token
                    (TokenKind.IntegerLiteral, lexeme.ToString(), lineNumber, position);
            }


            LastToken = token;
            return token;

        }

        private Token IsAddressLiteral()
        {
            int lineNumber = Stream.CurrentLineNumber;
            int position = Stream.CurrentPosition;
            var lexeme = new StringBuilder();
            Token token;

            bool isAddress = false;

            if (Stream.Peek() == null || Stream.Peek() != '@')
            {
                return null;
            }

            while (Stream.Peek() != null
                   && lineNumber == Stream.CurrentLineNumber
                   && (char.IsDigit((char)Stream.Peek())
                       || Stream.Peek() == '@'))
            {
                if (Stream.Peek() == '@' && (!char.IsDigit(Stream.Peek(2)[1]) || isAddress))
                {
                    break;
                }

                if (Stream.Peek() == '@')
                {
                    isAddress = true;
                }

                lexeme.Append(Stream.Get());
            }

            token = new Token
                (TokenKind.AddressLiteral, lexeme.ToString(), lineNumber, position);
            LastToken = token;
            return token;

        }

    }
}
