namespace SatisfactoryCalculator.Source.Base;

public abstract class ObservableObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    // ReSharper disable once UnusedMethodReturnValue.Global
    protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, newValue)) 
            return false;
        
        field = newValue;
        PropertyChanged?.Invoke(this, new(propertyName));
        return true;
    }

    protected void Notify(string propertyName)
    {
        PropertyChanged?.Invoke(this, new(propertyName));
    }
}
