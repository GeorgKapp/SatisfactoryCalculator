using Resource = SatisfactoryCalculator.Source.Models.Resource;

namespace SatisfactoryCalculator.Source.UI.UserControls;

public partial class ResourceControl
{
    public ResourceControl()
    {
        InitializeComponent();
    }
    
    internal Resource Resource
    {
        get => (Resource)GetValue(ResourceProperty);
        set => SetValue(ResourceProperty, value);
    }
    
    internal static readonly DependencyProperty ResourceProperty = 
        DependencyProperty.Register(
            nameof(Resource),
            typeof(Resource), 
            typeof(ResourceControl)
        );
}