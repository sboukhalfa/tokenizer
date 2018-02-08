using System;
using System.Collections.Generic;

namespace Compiler.Lexer
{
    public partial class Tokenizer
    {
        private static readonly Dictionary<string, TokenKind> KeywordToTokenKind =
            new Dictionary<string, TokenKind>(StringComparer.OrdinalIgnoreCase)
            {
                ["and"] = TokenKind.AndKeyword,
                ["break"] = TokenKind.BreakKeyword,
                ["case"] = TokenKind.CaseKeyword,
                ["catch"] = TokenKind.CatchKeyword,
                ["class"] = TokenKind.ClassKeyword,
                ["const"] = TokenKind.ConstKeyword,
                ["continue"] = TokenKind.ContinueKeyword,
                ["default"] = TokenKind.DefaultKeyword,
                ["do"] = TokenKind.DoKeyword,
                ["else"] = TokenKind.ElseKeyword,
                ["enum"] = TokenKind.EnumKeyword,
                ["false"] = TokenKind.FalseKeyword,
                ["for"] = TokenKind.ForKeyword,
                ["func"] = TokenKind.FuncKeyword,
                ["if"] = TokenKind.IfKeyword,
                ["in"] = TokenKind.InKeyword,
                ["is"] = TokenKind.IsKeyword,
                ["new"] = TokenKind.NewKeyword,
                ["nor"] = TokenKind.NorKeyword,
                ["null"] = TokenKind.NullKeyword,
                ["object"] = TokenKind.ObjectKeyword,
                ["or"] = TokenKind.OrKeyword,
                ["package"] = TokenKind.PackageKeyword,
                ["return"] = TokenKind.ReturnKeyword,
                ["sfunc"] = TokenKind.SfuncKeyword,
                ["svar"] = TokenKind.SvarKeyword,
                ["switch"] = TokenKind.SwitchKeyword,
                ["this"] = TokenKind.ThisKeyword,
                ["true"] = TokenKind.TrueKeyword,
                ["try"] = TokenKind.TryKeyword,
                ["var"] = TokenKind.VarKeyword,
                ["while"] = TokenKind.WhileKeyword,
            };

    }
}
