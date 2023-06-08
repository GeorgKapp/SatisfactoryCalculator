namespace SatisfactoryCalculator.DocsServices.Utility.Parser;

internal static class UnrealEngineClassLexer
{
    static UnrealEngineClassLexer()
    {
        _tokens.Add(TokenType.ScopeStart, "(");
        _tokens.Add(TokenType.ScopeEnd, ")");
        _tokens.Add(TokenType.Amount, "Amount");
        _tokens.Add(TokenType.Equal, "=");
        _tokens.Add(TokenType.Seperator, ",");
        _tokens.Add(TokenType.Descriptor, "'");
    }

    public static Token[] TokenizeString(string input)
    {
        if (string.IsNullOrEmpty(input))
            return Array.Empty<Token>();

        List<Token> tokenOutput = new();
        
        // ReSharper disable once InlineOutVariableDeclaration
        // ReSharper disable once TooWideLocalVariableScope
        string tokenMatch;
        var i = 0;
        while (i < input.Length)
        {
            if (IsToken(input, i, TokenType.ScopeStart, out tokenMatch))
            {
                tokenOutput.Add(new(TokenType.ScopeStart, tokenMatch));
                i += tokenMatch.Length;
            }
            else if (IsToken(input, i, TokenType.ScopeEnd, out tokenMatch))
            {
                tokenOutput.Add(new(TokenType.ScopeEnd, tokenMatch));
                i += tokenMatch.Length;
            }
            else if (IsToken(input, i, TokenType.Amount, out tokenMatch))
            {
                tokenOutput.Add(new(TokenType.Amount, tokenMatch));
                i += tokenMatch.Length;
            }
            else if (IsToken(input, i, TokenType.Equal, out tokenMatch))
            {
                tokenOutput.Add(new(TokenType.Equal, tokenMatch));
                i += tokenMatch.Length;
            }
            else if (IsToken(input, i, TokenType.Seperator, out tokenMatch))
            {
                tokenOutput.Add(new(TokenType.Seperator, tokenMatch));
                i += tokenMatch.Length;
            }
            else if (IsToken(input, i, TokenType.Descriptor, out tokenMatch))
            {
                tokenOutput.Add(new(TokenType.Descriptor, tokenMatch));
                i += tokenMatch.Length;
            }
            else
            {
                tokenOutput.Add(new(TokenType.String, input[i].ToString()));
                i += 1;
            }
        }
        
        return 
            MergeTokensByTokenType(tokenOutput, TokenType.String)
            .ToArray();
    }

    private static bool IsToken(string input, int currentIndex, TokenType tokenType, out string tokenMatch)
    {
        tokenMatch = string.Empty;

        var tokenString = _tokens[tokenType];
        if (currentIndex + tokenString.Length > input.Length)
            return false;

        var tokenPart = input[currentIndex..(currentIndex + tokenString.Length)];

        if (tokenPart != tokenString)
            return false;

        tokenMatch = tokenPart;
        return true;
    }

    private static IEnumerable<Token> MergeTokensByTokenType(IEnumerable<Token> tokens, TokenType tokenType)
    {
        var mergedTokens = new List<Token>();
        var mergedValues = string.Empty;
        
        foreach (var currentToken in tokens)
        {
            if (!string.IsNullOrWhiteSpace(mergedValues) && tokenType != currentToken.TokenType)
            {
                mergedTokens.Add(new(tokenType, mergedValues));
                mergedValues = string.Empty;
                mergedTokens.Add(currentToken);
            }
            else if (currentToken.TokenType == tokenType)
            {
                mergedValues += currentToken.Value;
            }
            else
            {
                mergedTokens.Add(currentToken);
            }
        }

        return mergedTokens;
    }

    private static Dictionary<TokenType, string> _tokens = new();
}