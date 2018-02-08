using System;

namespace Compiler.Lexer
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer tokenizer = new Tokenizer(args[0]);

            Token token;
            while ((token = tokenizer.Peek()) != null)
            {
                Console.WriteLine(token.Kind + " LEX: " + token.Lexeme + " POS: " + token.Position + " LN: " + token.LineNumber);
                tokenizer.Get();
            }

        }
    }
}
