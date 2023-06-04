// ReSharper disable HeapView.BoxingAllocation
namespace SatisfactoryCalculator.Controls.Controls;

public class VirtualizedItemsControl : ItemsControl
{
    static VirtualizedItemsControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(VirtualizedItemsControl),
            new FrameworkPropertyMetadata(
                typeof(VirtualizedItemsControl)
                ));
    }

    public VirtualizedItemsControl()
    {
        ((INotifyCollectionChanged)Items).CollectionChanged += VirtualizedItemsControl_CollectionChanged;
    }

    public bool AutoResetVerticalScrollBar
    {
        get => (bool)GetValue(AutoResetVerticalScrollBarProperty);
        set => SetValue(AutoResetVerticalScrollBarProperty, value);
    }
    public static readonly DependencyProperty AutoResetVerticalScrollBarProperty = 
        DependencyProperty.Register(
            nameof(AutoResetVerticalScrollBar), 
            typeof(bool), 
            typeof(VirtualizedItemsControl),
            new PropertyMetadata(true)
            );

    public bool AutoResetHorizontalScrollBar
    {
        get => (bool)GetValue(AutoResetHorizontalScrollBarProperty);
        set => SetValue(AutoResetHorizontalScrollBarProperty, value);
    }
    
    public static readonly DependencyProperty AutoResetHorizontalScrollBarProperty =
        DependencyProperty.Register(
            nameof(AutoResetHorizontalScrollBar), 
            typeof(bool), 
            typeof(VirtualizedItemsControl), 
            new PropertyMetadata(true)
            );

    private void VirtualizedItemsControl_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (GetTemplateChild("PART_ScrollViewer") is not ScrollViewer scrollViewer)
            return;

        if (AutoResetVerticalScrollBar)
            scrollViewer.ScrollToTop();

        if (AutoResetHorizontalScrollBar)
            scrollViewer.ScrollToLeftEnd();
    }
}
