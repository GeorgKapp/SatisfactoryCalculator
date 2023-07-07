using FactoryConfiguration = SatisfactoryCalculator.Source.Models.FactoryConfiguration;

namespace SatisfactoryCalculator.Source.UI.UserControls;

internal partial class FactoryConfigurationListControl : UserControl
{
    public FactoryConfigurationListControl()
    {
        InitializeComponent();
    }
    
    public List<FactoryConfiguration> ItemsSource
    {
        get => (List<FactoryConfiguration>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(
            nameof(ItemsSource), 
            typeof(List<FactoryConfiguration>), 
            typeof(FactoryConfigurationListControl),
            new PropertyMetadata(new List<FactoryConfiguration>()));
    
    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    public static readonly DependencyProperty SelectedItemProperty = 
        DependencyProperty.Register(
            nameof(SelectedItem), 
            typeof(object), 
            typeof(FactoryConfigurationListControl), 
            new(null));
    
    private void CreateNewOnClick(object sender, RoutedEventArgs e)
    {
        var newFactoryConfiguration = new FactoryConfiguration();
        ItemsSource.Add(newFactoryConfiguration);
        SelectedItem = newFactoryConfiguration;
    }
}