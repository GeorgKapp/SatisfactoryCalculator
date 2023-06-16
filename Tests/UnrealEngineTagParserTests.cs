namespace SatisfactoryCalculator.Tests;

public class UnrealEngineTagParserTests
{
    private const string BluePrintStringWithAmounts =
        "((ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/Cable/Desc_Cable.Desc_Cable_C\"',Amount=200),(ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/IronRod/Desc_IronRod.Desc_IronRod_C\"',Amount=200),(ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/IronScrew/Desc_IronScrew.Desc_IronScrew_C\"',Amount=500),(ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/IronPlate/Desc_IronPlate.Desc_IronPlate_C\"',Amount=300))";
    
    private const string BluePrintStringWithOneMissingAmount =
        "((ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/Cable/Desc_Cable.Desc_Cable_C\"',Amount=200),(ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/IronRod/Desc_IronRod.Desc_IronRod_C\"',Amount=200),(ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/IronScrew/Desc_IronScrew.Desc_IronScrew_C\"',Amount=500),(ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/IronPlate/Desc_IronPlate.Desc_IronPlate_C\"'))";

    private const string BluePrintStringWithNoAmounts =
        "((ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/Cable/Desc_Cable.Desc_Cable_C\"'),(ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/IronRod/Desc_IronRod.Desc_IronRod_C\"'),(ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/IronScrew/Desc_IronScrew.Desc_IronScrew_C\"'),(ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/IronPlate/Desc_IronPlate.Desc_IronPlate_C\"'))";

    private const string BluePrintStringWithNoRootClass = 
        "(BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/RawResources/OreCopper/Desc_OreCopper.Desc_OreCopper_C\"')";

    private const string SchemaStringWithAmounts =
        "((ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Environment/Crystal/Desc_Crystal_mk2.Desc_Crystal_mk2_C\"',Amount=1),(ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/Rotor/Desc_Rotor.Desc_Rotor_C\"',Amount=25),(ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/Cable/Desc_Cable.Desc_Cable_C\"',Amount=100))";

    private const string SchemaStringWithSingleEntry =
        "((ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Environment/Crystal/Desc_Crystal_mk2.Desc_Crystal_mk2_C\"',Amount=1))";
    
    [Fact(DisplayName = "Check Lexer Input for Amount")]
    public void CheckLexerInputForAmount() => CheckLexerInput(
        BluePrintStringWithAmounts, 
        new Token[]
        {
            new(TokenType.ScopeStart, "("),
            new(TokenType.ScopeStart, "("),
            new(TokenType.String, "ItemClass"),
            new(TokenType.Equal, "="),
            new(TokenType.String, "BlueprintGeneratedClass"),
            new(TokenType.Descriptor, "'"),
            new(TokenType.String, "\"/Game/FactoryGame/Resource/Parts/Cable/Desc_Cable.Desc_Cable_C\""),
            new(TokenType.Descriptor, "'"),
            new(TokenType.Seperator, ","),
            new(TokenType.Amount, "Amount"),
            new(TokenType.Equal, "="),
            new(TokenType.String, "200"),
            new(TokenType.ScopeEnd, ")"),
            new(TokenType.Seperator, ","),
            new(TokenType.ScopeStart, "("),
            new(TokenType.String, "ItemClass"),
            new(TokenType.Equal, "="),
            new(TokenType.String, "BlueprintGeneratedClass"),
            new(TokenType.Descriptor, "'"),
            new(TokenType.String, "\"/Game/FactoryGame/Resource/Parts/IronRod/Desc_IronRod.Desc_IronRod_C\""),
            new(TokenType.Descriptor, "'"),
            new(TokenType.Seperator, ","),
            new(TokenType.Amount, "Amount"),
            new(TokenType.Equal, "="),
            new(TokenType.String, "200"),
            new(TokenType.ScopeEnd, ")"),
            new(TokenType.Seperator, ","),
            new(TokenType.ScopeStart, "("),
            new(TokenType.String, "ItemClass"),
            new(TokenType.Equal, "="),
            new(TokenType.String, "BlueprintGeneratedClass"),
            new(TokenType.Descriptor, "'"),
            new(TokenType.String, "\"/Game/FactoryGame/Resource/Parts/IronScrew/Desc_IronScrew.Desc_IronScrew_C\""),
            new(TokenType.Descriptor, "'"),
            new(TokenType.Seperator, ","),
            new(TokenType.Amount, "Amount"),
            new(TokenType.Equal, "="),
            new(TokenType.String, "500"),
            new(TokenType.ScopeEnd, ")"),
            new(TokenType.Seperator, ","),
            new(TokenType.ScopeStart, "("),
            new(TokenType.String, "ItemClass"),
            new(TokenType.Equal, "="),
            new(TokenType.String, "BlueprintGeneratedClass"),
            new(TokenType.Descriptor, "'"),
            new(TokenType.String, "\"/Game/FactoryGame/Resource/Parts/IronPlate/Desc_IronPlate.Desc_IronPlate_C\""),
            new(TokenType.Descriptor, "'"),
            new(TokenType.Seperator, ","),
            new(TokenType.Amount, "Amount"),
            new(TokenType.Equal, "="),
            new(TokenType.String, "300"),
            new(TokenType.ScopeEnd, ")"),
            new(TokenType.ScopeEnd, ")"),
        });

    [Fact(DisplayName = "Check Parser with amounts")]
    public void CheckParserInputWithFixedAmounts() => CheckParserOutput(
        BluePrintStringWithAmounts, new UnrealEngineParseResult[]
        {
            new("Cable", 200), 
            new("IronRod", 200),
            new("IronScrew", 500), 
            new("IronPlate", 300)
        });
    
    [Fact(DisplayName = "Check Parser with no amounts")]
    public void CheckParserInputWithNoAmounts() => CheckParserOutput(
        BluePrintStringWithNoAmounts, new UnrealEngineParseResult[]
        {
            new("Cable", null), 
            new("IronRod", null),
            new("IronScrew", null), 
            new("IronPlate", null)
        });
    
    [Fact(DisplayName = "Check Parser with one missing amount")]
    public void CheckParserInputWithOneMissingAmount() => CheckParserOutput(
        BluePrintStringWithOneMissingAmount, new UnrealEngineParseResult[]
        {
            new("Cable", 200), 
            new("IronRod", 200),
            new("IronScrew", 500), 
            new("IronPlate", null)
        });
    
    [Fact(DisplayName = "Check Parser with no root class")]
    public void CheckParserInputWithNoRootClass() => CheckParserOutput(
        BluePrintStringWithNoRootClass, new UnrealEngineParseResult[]
        {
            new("OreCopper", null)
        });
    
    [Fact(DisplayName = "Check Parser with schema string")]
    public void CheckParserInputSchemaString() => CheckParserOutput(
        SchemaStringWithAmounts, new UnrealEngineParseResult[]
        {
            new("Crystal_mk2", 1), 
            new("Rotor", 25),
            new("Cable", 100)
        });
    
    [Fact(DisplayName = "Check Parser with schema string single entry")]
    public void CheckParserInputSchemaStringWithSingleEntry() => CheckParserOutput(
        SchemaStringWithSingleEntry, new UnrealEngineParseResult[]
        {
            new("Crystal_mk2", 1)
        });

    private void CheckParserOutput(string input, UnrealEngineParseResult[] expectedResults)
    {
        var results = UnrealEngineClassParser.ParseInputs(input);

        Assert.Equal(expectedResults.Length, results.Length);

        for (var i = 0; i < results.Length; i++)
            Assert.Equal(expectedResults[i], results[i]);
    }

    private void CheckLexerInput(string input, Token[] expectedTokens)
    {
        var result = UnrealEngineClassLexer.TokenizeString(input);
        
        Assert.Equal(expectedTokens.Length, result.Length);

        for (var i = 0; i < expectedTokens.Length; i++)
            Assert.Equal(expectedTokens[i], result[i]);
    }
}