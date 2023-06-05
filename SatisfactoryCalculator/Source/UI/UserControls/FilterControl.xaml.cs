namespace SatisfactoryCalculator.Source.UI.UserControls;

internal partial class FilterControl
{
    public FilterControl()
    {
        InitializeComponent();
    }

    public ObservableCollection<IEntity>? ItemsSource
    {
        get => (ObservableCollection<IEntity>?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(
            nameof(ItemsSource),
            typeof(ObservableCollection<IEntity>),
            typeof(FilterControl),
            new(OnItemSourceUpdated));
    
    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    public static readonly DependencyProperty SelectedItemProperty = 
        DependencyProperty.Register(
            nameof(SelectedItem), 
            typeof(object), 
            typeof(FilterControl), 
            new(null));

    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }
    
    public static readonly DependencyProperty ItemTemplateProperty = 
        DependencyProperty.Register(
            nameof(ItemTemplate), 
            typeof(DataTemplate), 
            typeof(FilterControl), 
            new(OnItemSourceUpdated));
    
    private static void OnItemSourceUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var filterControl = (FilterControl)d;
        
        filterControl.UpdateCollectionView();
        filterControl.RefreshFilter();
    }
    
    private void UpdateCollectionView()
    {
        if (ItemsSource is null) 
            return;
        
        _collectionView = CollectionViewSource.GetDefaultView(ItemsSource);
        _collectionView.Filter = ApplyFilter;
        listView.FindVisualChild<ScrollViewer>()?.ScrollToTop();
    }

    private void RefreshFilter()
    {
        _collectionView?.Refresh();
    }

    private bool ApplyFilter(object? filterObject)
    {
        if (string.IsNullOrWhiteSpace(_filter))
            return true;
        
        if (filterObject is null)
            return true;

        // ReSharper disable once HeapView.ClosureAllocation
        var filterObjectValue = ((IEntity)filterObject).Name;
        if (string.IsNullOrWhiteSpace(filterObjectValue))
            return false;

        var array = _filter.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return array.All(value => filterObjectValue.Contains(value, StringComparison.OrdinalIgnoreCase));
    }

    private void FilterTextChanged(object sender, TextChangedEventArgs e)
    {
        _filter = ((TextBox)sender).Text;
        RefreshFilter();
    }

    private string? _filter;
    private ICollectionView? _collectionView;
}
