using SatisfactoryCalculator.Source.Features.Shared.Command;
using System.Threading;

namespace SatisfactoryCalculator.Source.Features.FactoryPlanner;

internal partial class FactoryConfigurationOutputListControl : UserControl
{
    public FactoryConfigurationOutputListControl()
    {
        InitializeComponent();
    }
    
    public Configuration Configuration
    {
        get => (Configuration)GetValue(ConfigurationProperty);
        set => SetValue(ConfigurationProperty, value);
    }

    public static readonly DependencyProperty ConfigurationProperty =
        DependencyProperty.Register(
            nameof(Configuration), 
            typeof(Configuration), 
            typeof(FactoryConfigurationOutputListControl));

    public ObservableCollection<OutputRequirements> ItemsSource
    {
        get => (ObservableCollection<OutputRequirements>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(
            nameof(ItemsSource), 
            typeof(ObservableCollection<OutputRequirements>), 
            typeof(FactoryConfigurationOutputListControl),
            new PropertyMetadata(new ObservableCollection<OutputRequirements>()));


    private ICommand? _deleteEntryCommand;
    public ICommand DeleteEntryCommand => _deleteEntryCommand ??= new RelayCommand(Delete);


    private void CreateNewClick(object sender, RoutedEventArgs e)
    {
        if (ItemsSource.Any(p => p.Entity is null))
        {
            MessageBox.Show("At least one output does not have a selected item");
            return;
        }
        
        var newFactoryConfigurationOutput = new OutputRequirements();
        ItemsSource.Add(newFactoryConfigurationOutput);

        var scrollViewer = this.FindVisualParent<ScrollViewer>();
        scrollViewer.ScrollToBottom();
    }

    private void Delete(object? parameter)
    {
        ItemsSource.Remove((OutputRequirements)parameter!);
    }
}