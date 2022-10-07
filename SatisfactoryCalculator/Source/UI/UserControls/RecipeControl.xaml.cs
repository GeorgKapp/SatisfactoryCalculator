namespace SatisfactoryCalculator.Source.UI.UserControls;

public partial class RecipeControl : UserControl
{
    internal RecipeModel Recipe
    {
        get => (RecipeModel)GetValue(RecipeProperty);
        set => SetValue(RecipeProperty, value);
    }
    internal static readonly DependencyProperty RecipeProperty = DependencyProperty.Register(nameof(Recipe), typeof(RecipeModel), typeof(RecipeControl), new PropertyMetadata(UpdateVisibilities));

    private static void UpdateVisibilities(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (d as RecipeControl);
        control.textBoxEqual.Visibility = (e.NewValue as RecipeModel)?.Products?.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
        control.textBoxArrow.Visibility = (e.NewValue as RecipeModel)?.Buildings?.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
    }

    public RecipeControl()
    {
        InitializeComponent();
    }
}
