namespace SatisfactoryCalculator.Source.Features.FactoryPlanner;

internal partial class FactoryConfigurationListControl : UserControl
{
    public FactoryConfigurationListControl()
    {
        InitializeComponent();
    }
    
    public ObservableCollection<FactoryConfiguration> ItemsSource
    {
        get => (ObservableCollection<FactoryConfiguration>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(
            nameof(ItemsSource), 
            typeof(ObservableCollection<FactoryConfiguration>), 
            typeof(FactoryConfigurationListControl),
            new PropertyMetadata(new ObservableCollection<FactoryConfiguration>()));
    
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
    
    private void CreateNewClick(object sender, RoutedEventArgs e)
    {
        var newFactoryConfiguration = new FactoryConfiguration();
        ItemsSource.Add(newFactoryConfiguration);
        SelectedItem = newFactoryConfiguration;
    }
}