using Miner = SatisfactoryCalculator.Source.Models.Miner;

namespace SatisfactoryCalculator.Source.UI.UserControls;

public partial class MinerControl
{
    public MinerControl()
    {
        InitializeComponent();
    }
    
    internal Miner Miner
    {
        get => (Miner)GetValue(MinerProperty);
        set => SetValue(MinerProperty, value);
    }
    
    internal static readonly DependencyProperty MinerProperty = 
        DependencyProperty.Register(
            nameof(Miner),
            typeof(Miner), 
            typeof(MinerControl)
        );
}