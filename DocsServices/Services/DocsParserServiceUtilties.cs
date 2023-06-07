// ReSharper disable UnusedMember.Local
namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
    private static string[] GetAllClassPrefixes(IEnumerable<RootObject> classes1)
    {
        return classes1
            .SelectMany(p => p.Classes)
            .Select(p => p.ClassName.Split('_')[0])
            .Distinct()
            .ToArray();
    }

    private static string[] GetAllForms(IEnumerable<RootObject> classes1)
    {
        return classes1
           .SelectMany(p => p.Classes)
           .Select(p => p.mForm)
           .Distinct()
           .ToArray();
    }

    private static string[] GetAllStackSizes(IEnumerable<RootObject> classes1)
    {
        return classes1
           .SelectMany(p => p.Classes)
           .Select(p => p.mStackSize)
           .Distinct()
           .ToArray();
    }

    private static string[] GetAllEquipmentSlots(IEnumerable<RootObject> classes1)
    {
        return classes1
           .SelectMany(p => p.Classes)
           .Select(p => p.mEquipmentSlot)
           .Distinct()
           .ToArray();
    }

    private static string[] GetAllTypes(IEnumerable<RootObject> classes1)
    {
        return classes1
           .SelectMany(p => p.Classes)
           .Select(p => p.mType)
           .Distinct()
           .ToArray();
    }

    private static string[] GetAllSchematicIcons(IEnumerable<RootObject> classes1)
    {
        return classes1
           .SelectMany(p => p.Classes)
           .Select(p => p.mSchematicIcon)
           .Distinct()
           .ToArray();
    }

    private static string[] GetAllSmallSchematicIcons(IEnumerable<RootObject> classes1)
    {
        return classes1
           .SelectMany(p => p.Classes)
           .Select(p => p.mSmallSchematicIcon)
           .Distinct()
           .ToArray();
    }

    private static string[] GetAllSmallSchematicDependencyClasses(IEnumerable<RootObject> classes1)
    {
        return classes1
          .SelectMany(p => p.Classes)
          .SelectMany(p => p.mSchematicDependencies)
          .Select(p => p.Class)
          .Distinct()
          .ToArray();
    }

}
