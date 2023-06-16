// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS8618
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
public class MSchematicDependencies
{
    public string Class { get; set; }
    public string mSchematics { get; set; }
    public string mRequireAllSchematicsToBePurchased { get; set; }
    public string mGamePhase { get; set; }
}