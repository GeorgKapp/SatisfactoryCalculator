namespace SatisfactoryCalculator.DocsServices.Utility.Parser;

internal class UnrealEngineParseResult : IEquatable<UnrealEngineParseResult>, ICloneable
{
    public UnrealEngineParseResult() { }
    public UnrealEngineParseResult(
        string ClassName, 
        double? Amount = null)
    {
        this.ClassName = ClassName;
        this.Amount = Amount;
    }

    public string ClassName { get; set; }
    public double? Amount { get; set; }

    public bool Equals(UnrealEngineParseResult? other)
    {
        if (other is null)
            return false;

        return other.ClassName == ClassName && other.Amount == Amount;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
