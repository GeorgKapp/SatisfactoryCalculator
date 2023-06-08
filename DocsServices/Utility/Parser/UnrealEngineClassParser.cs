namespace SatisfactoryCalculator.DocsServices.Utility.Parser;

internal static class UnrealEngineClassParser
{
    public static UnrealEngineParseResult[] ParseInputs(string input)
    {
        var tokens = UnrealEngineClassLexer.TokenizeString(input);

        UnrealEngineParseRootResult rootResult = null;
        UnrealEngineParseResult currentParseResult = null;

        if (tokens.Length >= 2
            && tokens[0].TokenType == TokenType.ScopeStart
            && tokens[1].TokenType != TokenType.ScopeStart)
        {
            rootResult = new();
        }

        var i = 0;
        while (i < tokens.Length)
        {
            var token = tokens[i];
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (token.TokenType)
            {
                case TokenType.ScopeStart:
                    if (rootResult is null)
                        rootResult = new();
                    else
                        currentParseResult = new();
                    
                    i += 1;
                    break;
                
                case TokenType.ScopeEnd:
                    if (currentParseResult is not null)
                    {
                        rootResult!.Results.Add((UnrealEngineParseResult)currentParseResult.Clone());
                        currentParseResult = null;
                    }
                    i += 1;
                    break;
                
                case TokenType.Descriptor:
                    var classNameWithoutQuotationMarks = tokens[i + 1].Value
                        .Replace("\"", "")
                        .Split(".")
                        .Last();

                    if (currentParseResult is null)
                        _ = "";

                    currentParseResult!.ClassName =
                        ClassNameParseUtility.CleanClassName(classNameWithoutQuotationMarks)!;
                    
                    i += 3;
                    break;
                
                case TokenType.Amount:
                    currentParseResult!.Amount = Convert.ToInt32(tokens[i + 2].Value);
                    i += 3;
                    break;
                
                default:
                    i += 1;
                    break;
            }
        }
        
        return rootResult is not null 
            ? rootResult.Results.ToArray() 
            : Array.Empty<UnrealEngineParseResult>();
    }
}