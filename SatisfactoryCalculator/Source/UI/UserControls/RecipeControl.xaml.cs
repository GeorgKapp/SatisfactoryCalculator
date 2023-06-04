using Recipe = SatisfactoryCalculator.Source.Models.Recipe;

namespace SatisfactoryCalculator.Source.UI.UserControls;

public partial class RecipeControl : UserControl
{
    internal Recipe Recipe
    {
        get => (Recipe)GetValue(RecipeProperty);
        set => SetValue(RecipeProperty, value);
    }
    internal static readonly DependencyProperty RecipeProperty = DependencyProperty.Register(nameof(Recipe), typeof(Recipe), typeof(RecipeControl), new PropertyMetadata(UpdateVisibilities));

    private static void UpdateVisibilities(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (d as RecipeControl);
        control.textBoxEqual.Visibility = (e.NewValue as Recipe)?.Products?.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
        control.textBoxArrow.Visibility = (e.NewValue as Recipe)?.Buildings?.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
    }

    public RecipeControl()
    {
        InitializeComponent();
    }
}
