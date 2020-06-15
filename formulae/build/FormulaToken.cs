using sly.lexer;

namespace formulae.build {

    [Lexer(IgnoreEOL = false)]
    public enum FormulaToken {

        EOS = 0,

        [Lexeme(GenericToken.Int)]        
        INT = 1,

        [Lexeme(GenericToken.Double)]
        DOUBLE = 2,

        [Lexeme(GenericToken.Identifier,IdentifierType.Alpha)]
        ID = 3,

        [Lexeme(GenericToken.SugarToken, "+")] 
        PLUS = 4,

        [Lexeme(GenericToken.SugarToken, "-")] 
        MINUS = 5,

        [Lexeme(GenericToken.SugarToken, "*")] 
        TIMES = 6,

        [Lexeme(GenericToken.SugarToken, "/")] DIV = 7 ,

        [Lexeme(GenericToken.SugarToken, "(")] LPAREN = 8,

        [Lexeme(GenericToken.SugarToken, ")")] RPAREN = 9,

        [Lexeme(GenericToken.SugarToken, "&&")] AND = 10,

        [Lexeme(GenericToken.SugarToken, "||")] OR = 11,

        [Lexeme(GenericToken.SugarToken, "!")] NOT = 12,

        [Lexeme(GenericToken.SugarToken, "<")] LT = 13,

        [Lexeme(GenericToken.SugarToken, ">")] GT = 14,

        [Lexeme(GenericToken.SugarToken, ">=")]  GTE = 15,

        [Lexeme(GenericToken.SugarToken, "<=")] LTE = 16,

        [Lexeme(GenericToken.SugarToken, "==")] EQ = 17,

        [Lexeme(GenericToken.SugarToken, "<>")] NEQ = 18,

        [Lexeme(GenericToken.SugarToken, "=")] SET = 19,

        [Lexeme(GenericToken.SugarToken, "\r\n",IsLineEnding=true)] WIN_EOL = 50,

        [Lexeme(GenericToken.SugarToken, "\r",IsLineEnding=true)] MAC_EOL = 51,

        [Lexeme(GenericToken.SugarToken, "\n",IsLineEnding=true)] IX_EOL = 50,

        [SingleLineComment("#")]
        COMMENT = 100,
        
        [Lexeme(GenericToken.KeyWord,"TRUE")]
            [Lexeme(GenericToken.KeyWord,"VRAI")]
        TRUE = 150,
        
        [Lexeme(GenericToken.KeyWord,"FALSE")]
        [Lexeme(GenericToken.KeyWord,"FAUX")]
        FALSE = 151,
        

    }
}