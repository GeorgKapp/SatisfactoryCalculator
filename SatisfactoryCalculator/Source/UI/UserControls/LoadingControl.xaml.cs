﻿namespace SatisfactoryCalculator.Source.UI.UserControls;

public partial class LoadingControl : UserControl
{
    public string Report
    {
        get => (string)GetValue(ReportProperty);
        set => SetValue(ReportProperty, value);
    }
    public static readonly DependencyProperty ReportProperty = DependencyProperty.Register(nameof(Report), typeof(string), typeof(LoadingControl));

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }
    public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(LoadingControl));

    public ICommand CancelCommand
    {
        get => (ICommand)GetValue(CancelCommandProperty);
        set => SetValue(CancelCommandProperty, value);
    }
    public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register(nameof(CancelCommand), typeof(ICommand), typeof(LoadingControl));

    public LoadingControl()
    {
        InitializeComponent();
    }
}
