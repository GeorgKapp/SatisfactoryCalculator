// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS8618
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
public class RootObject
{
    public string NativeClass { get; set; }
    public Classes[] Classes { get; set; }
}