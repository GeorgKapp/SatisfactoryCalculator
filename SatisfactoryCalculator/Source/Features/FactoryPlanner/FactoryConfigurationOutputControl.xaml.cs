namespace SatisfactoryCalculator.Source.Features.FactoryPlanner;

internal partial class FactoryConfigurationOutputControl : UserControl
{
    public FactoryConfigurationOutputControl()
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
            typeof(FactoryConfigurationOutputControl));
    
    public FactoryConfigurationOutput FactoryConfigurationOutput
    {
        get => (FactoryConfigurationOutput)GetValue(FactoryConfigurationOutputProperty);
        set => SetValue(FactoryConfigurationOutputProperty, value);
    }
    
    public static readonly DependencyProperty FactoryConfigurationOutputProperty = 
        DependencyProperty.Register(
            nameof(FactoryConfigurationOutput), 
            typeof(FactoryConfigurationOutput), 
            typeof(FactoryConfigurationOutputControl));

    private void ItemAmountTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!_allowTextBoxChange)
            return;

        if (string.IsNullOrWhiteSpace(itemAmountTextBox.Text)) 
            return;
        
        _allowTextBoxChange = false;
        FactoryConfigurationOutput.BuildingAmount = null;
        _allowTextBoxChange = true;
    }

    private void BuildingAmountTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!_allowTextBoxChange)
            return;

        if (string.IsNullOrWhiteSpace(buildingAmountTextBox.Text)) 
            return;
        
        _allowTextBoxChange = false;
        FactoryConfigurationOutput.Amount = null;
        _allowTextBoxChange = true;
    }

    private bool _allowTextBoxChange = true;
}