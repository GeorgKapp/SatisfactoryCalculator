namespace SatisfactoryCalculator.Source.UI.UserControls;

public partial class FilterControl : UserControl
{
    public IEnumerable? ItemsSource
    {
        get => (IEnumerable?)GetValue(FilterProperty);
        set => SetValue(FilterProperty, value);
    }
    public static readonly DependencyProperty FilterProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(FilterControl), new PropertyMetadata(OnItemSourceUpdated));
    private static void OnItemSourceUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((FilterControl)d).UpdateCollectionView();
        ((FilterControl)d).RefreshFilter();
    }

    public object SelectedItem
    {
        get => (object)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(FilterControl), new PropertyMetadata(null));

    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }
    public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(FilterControl), new PropertyMetadata(OnItemSourceUpdated));

    public string FilterMemberPath
    {
        get => (string)GetValue(FilterMemberPathProperty);
        set => SetValue(FilterMemberPathProperty, value);
    }
    public static readonly DependencyProperty FilterMemberPathProperty = DependencyProperty.Register(nameof(FilterMemberPath), typeof(string), typeof(FilterControl), new PropertyMetadata(OnItemSourceUpdated));

    public FilterControl()
    {
        InitializeComponent();
    }

    private void UpdateCollectionView()
    {
        if (ItemsSource is not null)
        {
            _itemsCollectionView = CollectionViewSource.GetDefaultView(ItemsSource);
            _itemsCollectionView.Filter = ApplyFilter;
        }
    }

    private void RefreshFilter() => _itemsCollectionView?.Refresh();

    private bool ApplyFilter(object? filterObject)
    {
        if (string.IsNullOrWhiteSpace(_filter))
            return true;

        if (string.IsNullOrWhiteSpace(FilterMemberPath))
            return true;

        if (filterObject is null)
            return true;

        var filterObjectValue = filterObject.GetType().GetProperty(FilterMemberPath)!.GetValue(filterObject)?.ToString();
        if (string.IsNullOrWhiteSpace(filterObjectValue))
            return false;

        var array = _filter.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (var value in array)
        {
            if (!filterObjectValue.Contains(value, StringComparison.OrdinalIgnoreCase))
                return false;
        }
        return true;
    }

    private void FilterTextChanged(object sender, TextChangedEventArgs e)
    {
        _filter = ((TextBox)sender).Text;
        RefreshFilter();
    }

    private string? _filter;
    private ICollectionView? _itemsCollectionView;
}
