namespace SatisfactoryCalculator.Source.Features.FactoryPlanner;

internal partial class FactoryConfigurationOutputControl : UserControl
{
    public FactoryConfigurationOutputControl()
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
            typeof(FactoryConfigurationOutputControl),
            new(ConfigurationPropertyChangedCallback));

    private static void ConfigurationPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((FactoryConfigurationOutputControl)d).ConfigurationPropertyChanged();
    }

    private void ConfigurationPropertyChanged()
    {
        Entities = Configuration.Items.Where(p =>
            Configuration.ReferenceDictionary[p.ClassName].RecipeProduct.Length > 0);
    }

    private IEntity? SelectedEntity
    {
        get => (IEntity?)GetValue(SelectedEntityProperty);
        set => SetValue(SelectedEntityProperty, value);
    }

    private static readonly DependencyProperty SelectedEntityProperty =
        DependencyProperty.Register(
            nameof(SelectedEntity), 
            typeof(IEntity), 
            typeof(FactoryConfigurationOutputControl),
            new(null, SelectedEntityPropertyChangedCallback));

    private static void SelectedEntityPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((FactoryConfigurationOutputControl)d).SelectedEntityPropertyChanged();
    }

    private void SelectedEntityPropertyChanged()
    {
        OutputRequirements.Entity = SelectedEntity;
        AvailableRecipes = Configuration.ReferenceDictionary[SelectedEntity!.ClassName].RecipeProduct;
        SelectedRecipe = AvailableRecipes.First(p => !p.IsAlternate);
    }

    private IEnumerable<IEntity> Entities
    {
        get => (IEnumerable<IEntity>)GetValue(EntitiesProperty);
        set => SetValue(EntitiesProperty, value);
    }

    private static readonly DependencyProperty EntitiesProperty =
        DependencyProperty.Register(
            nameof(Entities), 
            typeof(IEnumerable<IEntity>), 
            typeof(FactoryConfigurationOutputControl));
    
    private IEnumerable<IRecipe> AvailableRecipes
    {
        get => (IEnumerable<IRecipe>)GetValue(AvailableRecipesProperty);
        set => SetValue(AvailableRecipesProperty, value);
    }

    private static readonly DependencyProperty AvailableRecipesProperty =
        DependencyProperty.Register(
            nameof(AvailableRecipes), 
            typeof(IEnumerable<IRecipe>), 
            typeof(FactoryConfigurationOutputControl));

    private IRecipe SelectedRecipe
    {
        get => (IRecipe)GetValue(SelectedRecipeProperty);
        set => SetValue(SelectedRecipeProperty, value);
    }

    private static readonly DependencyProperty SelectedRecipeProperty =
        DependencyProperty.Register(
            nameof(SelectedRecipe),
            typeof(IRecipe),
            typeof(FactoryConfigurationOutputControl),
            new(SelectedRecipePropertyChangedCallback));

    private static void SelectedRecipePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((FactoryConfigurationOutputControl)d).SelectedRecipePropertyChanged();
    }

    private void SelectedRecipePropertyChanged()
    {
        OutputRequirements.ChosenRecipe = SelectedRecipe;
    }

    public OutputRequirements OutputRequirements
    {
        get => (OutputRequirements)GetValue(OutputRequirementsProperty);
        set => SetValue(OutputRequirementsProperty, value);
    }
    
    public static readonly DependencyProperty OutputRequirementsProperty = 
        DependencyProperty.Register(
            nameof(OutputRequirements), 
            typeof(OutputRequirements), 
            typeof(FactoryConfigurationOutputControl));
}