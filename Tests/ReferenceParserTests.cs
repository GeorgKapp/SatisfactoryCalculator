using SatisfactoryCalculator.DocsServices.Utility;

namespace SatisfactoryCalculator.Tests;

public class ReferenceParserTests
{
    private const string ammoClassReferences = "(\"/Script/Engine.BlueprintGeneratedClass'/Game/FactoryGame/Resource/Parts/NobeliskExplosive/Desc_NobeliskExplosive.Desc_NobeliskExplosive_C'\",\"/Script/Engine.BlueprintGeneratedClass'/Game/FactoryGame/Equipment/NobeliskDetonator/Ammo/Desc_NobeliskShockwave.Desc_NobeliskShockwave_C'\",\"/Script/Engine.BlueprintGeneratedClass'/Game/FactoryGame/Equipment/NobeliskDetonator/Ammo/Desc_NobeliskCluster.Desc_NobeliskCluster_C'\",\"/Script/Engine.BlueprintGeneratedClass'/Game/FactoryGame/Resource/Parts/SnowballProjectile/Desc_SnowballProjectile.Desc_SnowballProjectile_C'\",\"/Script/Engine.BlueprintGeneratedClass'/Game/FactoryGame/Events/Christmas/Fireworks/Desc_Fireworks_Projectile_01.Desc_Fireworks_Projectile_01_C'\",\"/Script/Engine.BlueprintGeneratedClass'/Game/FactoryGame/Events/Christmas/Fireworks/Desc_Fireworks_Projectile_02.Desc_Fireworks_Projectile_02_C'\",\"/Script/Engine.BlueprintGeneratedClass'/Game/FactoryGame/Events/Christmas/Fireworks/Desc_Fireworks_Projectile_03.Desc_Fireworks_Projectile_03_C'\",\"/Script/Engine.BlueprintGeneratedClass'/Game/FactoryGame/Equipment/NobeliskDetonator/Ammo/Desc_NobeliskNuke.Desc_NobeliskNuke_C'\",\"/Script/Engine.BlueprintGeneratedClass'/Game/FactoryGame/Equipment/NobeliskDetonator/Ammo/Desc_NobeliskGas.Desc_NobeliskGas_C'\")";
    private const string scannableObjectsReferences = "((ItemDescriptor=\"/Script/Engine.BlueprintGeneratedClass'/Game/FactoryGame/Resource/Environment/CrashSites/Desc_HardDrive.Desc_HardDrive_C'\",ActorsAllowedToScan=(\"/Script/CoreUObject.Class'/Script/FactoryGame.FGObjectScanner'\",\"/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableRadarTower'\")))";
    
    [Fact(DisplayName = "Assert Ammo Class References correct output")]
    public void AssertAmmoClassReferences()
    {
        var output = ReferenceParseUtility.GetReferences(ammoClassReferences).ToArray();
        
        Assert.Equal(9, output.Length);
        Assert.Equal("NobeliskExplosive", output[0]);
        Assert.Equal("NobeliskShockwave", output[1]);
        Assert.Equal("NobeliskCluster", output[2]);
        Assert.Equal("SnowballProjectile", output[3]);
        Assert.Equal("Fireworks_Projectile_01", output[4]);
        Assert.Equal("Fireworks_Projectile_02", output[5]);
        Assert.Equal("Fireworks_Projectile_03", output[6]);
        Assert.Equal("NobeliskNuke", output[7]);
        Assert.Equal("NobeliskGas", output[8]);
    }

    [Fact(DisplayName = "Assert Scannable Object Class References correct output")]
    public void AssertScannableObjectsReferences()
    {
        var output = ScannableObjectParseUtility.MapToScannableObjects(
            scannableObjectsReferences, 
            new [] {"ObjectScanner", "HardDrive"}, 
            new [] {"RadarTower"});
        
        Assert.Single(output);
        Assert.Equal("HardDrive", output[0].ItemClassName);
        
        Assert.Equal(2, output[0].ScanningActors.Count);
        Assert.Equal("ObjectScanner", output[0].ScanningActors.ToArray()[0].ItemClassName);
        Assert.Equal("RadarTower", output[0].ScanningActors.ToArray()[1].BuildingClassName);
    }
}