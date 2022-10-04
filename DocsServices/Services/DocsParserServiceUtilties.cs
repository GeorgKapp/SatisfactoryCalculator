namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
    private string[] GetAllClassPrefixes(Class1[] classes1)
    {
        return classes1
            .SelectMany(p => p.Classes)
            .Select(p => p.ClassName.Split('_')[0])
            .Distinct()
            .ToArray();
    }

    private string[] GetAllForms(Class1[] classes1)
    {
        return classes1
           .SelectMany(p => p.Classes)
           .Select(p => p.mForm)
           .Distinct()
           .ToArray();
    }

    private string[] GetAllStackSizes(Class1[] classes1)
    {
        return classes1
           .SelectMany(p => p.Classes)
           .Select(p => p.mStackSize)
           .Distinct()
           .ToArray();
    }

    private string[] GetAllEquipmentSlots(Class1[] classes1)
    {
        return classes1
           .SelectMany(p => p.Classes)
           .Select(p => p.mEquipmentSlot)
           .Distinct()
           .ToArray();
    }

    private string[] GetAllTypes(Class1[] classes1)
    {
        return classes1
           .SelectMany(p => p.Classes)
           .Select(p => p.mType)
           .Distinct()
           .ToArray();
    }

    private string[] GetAllSchematicIcons(Class1[] classes1)
    {
        return classes1
           .SelectMany(p => p.Classes)
           .Select(p => p.mSchematicIcon)
           .Distinct()
           .ToArray();
    }

    private string[] GetAllSmallSchematicIcons(Class1[] classes1)
    {
        return classes1
           .SelectMany(p => p.Classes)
           .Select(p => p.mSmallSchematicIcon)
           .Distinct()
           .ToArray();
    }

    private string[] GetAllSmallSchematicDependencyClasses(Class1[] classes1)
    {
        return classes1
          .SelectMany(p => p.Classes)
          .Where(p => p.mSchematicDependencies is not null)
          .SelectMany(p => p.mSchematicDependencies)
          .Where(p => p is not null)
          .Select(p => p.Class)
          .Distinct()
          .ToArray();
    }

}
