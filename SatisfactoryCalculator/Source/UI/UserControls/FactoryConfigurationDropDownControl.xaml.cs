using FactoryConfiguration = SatisfactoryCalculator.Source.Models.FactoryConfiguration;

namespace SatisfactoryCalculator.Source.UI.UserControls;

internal partial class FactoryConfigurationDropDownControl : UserControl
{
    public FactoryConfigurationDropDownControl()
    {
        InitializeComponent();
    }
    
    public event EventHandler CreateNewClicked;

    public List<FactoryConfiguration> FactoryConfigurations
    {
        get => (List<FactoryConfiguration>)GetValue(FactoryConfigurationsProperty);
        set => SetValue(FactoryConfigurationsProperty, value);
    }

    public static readonly DependencyProperty FactoryConfigurationsProperty =
        DependencyProperty.Register(
            nameof(FactoryConfigurations), 
            typeof(List<FactoryConfiguration>), 
            typeof(FactoryConfigurationDropDownControl),
            new PropertyMetadata(new List<FactoryConfiguration>()));
    
    private void OnCreateNewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        CreateNewClicked?.Invoke(this, EventArgs.Empty);
    }
}