namespace SatisfactoryCalculator.Source.Features.Shared.UserControls;

public partial class RecipeControl
{
    internal Recipe.Recipe Recipe
    {
        get => (Recipe.Recipe)GetValue(RecipeProperty);
        set => SetValue(RecipeProperty, value);
    }
    internal static readonly DependencyProperty RecipeProperty = DependencyProperty.Register(nameof(Recipe), typeof(Recipe.Recipe), typeof(RecipeControl), new(UpdateVisibilities));

    private static void UpdateVisibilities(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((RecipeControl)d).textBoxEqual.Visibility = (e.NewValue as Recipe.Recipe)?.Products.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
        ((RecipeControl)d).textBoxArrow.Visibility = (e.NewValue as Recipe.Recipe)?.Buildings.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
    }

    public RecipeControl()
    {
        InitializeComponent();
    }
}
