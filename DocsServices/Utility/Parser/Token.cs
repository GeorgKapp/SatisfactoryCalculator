namespace SatisfactoryCalculator.DocsServices.Utility.Parser;

internal class Token : IEquatable<Token>
{
    public Token(TokenType TokenType, string Value)
    {
        this.TokenType = TokenType;
        this.Value = Value;
    }

    public TokenType TokenType { get; init; }
    public string Value { get; init; }
        
        
    public bool Equals(Token? other)
    {
        if (other is null)
            return false;

        return other.TokenType == TokenType && other.Value.Equals(Value);
    }
}