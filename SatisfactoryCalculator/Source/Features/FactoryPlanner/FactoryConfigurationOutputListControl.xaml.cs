using SatisfactoryCalculator.Source.Features.Shared.Command;
using System.Threading;

namespace SatisfactoryCalculator.Source.Features.FactoryPlanner;

internal partial class FactoryConfigurationOutputListControl : UserControl
{
    public FactoryConfigurationOutputListControl()
    {
        InitializeComponent();
    }
    
    public IEnumerable<IEntity> Entities
    {
        get => (IEnumerable<IEntity>)GetValue(EntitiesProperty);
        set => SetValue(EntitiesProperty, value);
    }

    public static readonly DependencyProperty EntitiesProperty =
        DependencyProperty.Register(
            nameof(Entities), 
            typeof(IEnumerable<IEntity>), 
            typeof(FactoryConfigurationOutputListControl));
    
    public ObservableCollection<FactoryConfigurationOutput> ItemsSource
    {
        get => (ObservableCollection<FactoryConfigurationOutput>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(
            nameof(ItemsSource), 
            typeof(ObservableCollection<FactoryConfigurationOutput>), 
            typeof(FactoryConfigurationOutputListControl),
            new PropertyMetadata(new ObservableCollection<FactoryConfigurationOutput>()));


    private ICommand? _deleteEntryCommand;
    public ICommand DeleteEntryCommand => _deleteEntryCommand ??= new RelayCommand(Delete);


    private void CreateNewClick(object sender, RoutedEventArgs e)
    {
        var newFactoryConfigurationOutput = new FactoryConfigurationOutput();
        ItemsSource.Add(newFactoryConfigurationOutput);

        var scrollViewer = this.FindVisualParent<ScrollViewer>();
        scrollViewer.ScrollToBottom();
    }

    private void Delete(object? parameter)
    {
        ItemsSource.Remove((FactoryConfigurationOutput)parameter!);
    }
}