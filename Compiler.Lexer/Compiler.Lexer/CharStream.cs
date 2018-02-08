using System;
using System.Collections.Generic;
using System.Text;
namespace Compiler.Lexer
{
    public class CharStream
    {
        private readonly IList<string> Lines;
        public bool EOF { get; private set; }
        public int CurrentLineNumber { get; set; }
        public int CurrentPosition { get; set; }

        public CharStream(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                Lines = Preprocessor.Utilities.GenerateList(source);
            }
            else
            {
                EOF = true;
                return;
            }

            int line = 0;
            while (string.IsNullOrEmpty(Lines[line]))
            {
                line++;
            }
            EOF = false;
            CurrentLineNumber = line;
            CurrentPosition = 0;

        }

        public char? Peek()
        {
            if (EOF)
            {
                return null;
            }

            char? c = Get();
            Unget();
            return c;
        }

        public string Peek(int count)
        {
            string peek = Get(count);
            Unget(count);
            return peek;
        }

        public char? Get()
        {
            if (EOF)
            {
                return null;
            }

            while (String.IsNullOrEmpty(Lines[CurrentLineNumber])
                   && CurrentLineNumber < Lines.Count)
            {
                CurrentLineNumber++;
            }

            char c = Lines[CurrentLineNumber][CurrentPosition];

            if (CurrentPosition + 1 < Lines[CurrentLineNumber].Length)
            {
                CurrentPosition++;
            }
            else
            {

                if (CurrentLineNumber + 1 < Lines.Count)
                {

                    CurrentLineNumber++;
                    CurrentPosition = 0;
                }
                else
                {
                    EOF = true;
                }
            }

            return c;
        }

        public string Get(int count)
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                char? c = Get();
                if (c == null)
                {
                    return null;
                }
                else
                {
                    stringBuilder.Append(c);
                }

            }
            return stringBuilder.ToString();
        }

        private void Unget()
        {
            if (EOF)
            {
                EOF = false;
            }
            else
            {
                if (CurrentPosition > 0)
                {
                    CurrentPosition--;
                }
                else if (CurrentLineNumber > 0)
                {
                    while (string.IsNullOrEmpty(Lines[--CurrentLineNumber])) ;
                    CurrentPosition = Lines[CurrentLineNumber].Length - 1;
                }

            }
        }

        private void Unget(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Unget();
            }
        }

    }
}
