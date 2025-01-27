namespace DESAFIO_MOVISIS.Behaviors;

public class CompararCamposBehavior : Behavior<Entry>
{
    public static readonly BindableProperty PropriedadeDeEntrada =
           BindableProperty.Create(nameof(CompareToEntry), typeof(Entry), typeof(CompararCamposBehavior));

    public Entry CompareToEntry
    {
        get => (Entry)GetValue(PropriedadeDeEntrada);
        set => SetValue(PropriedadeDeEntrada, value);
    }
    
    protected override void OnAttachedTo(Entry bindable)
    {
        base.OnAttachedTo(bindable);
        bindable.TextChanged += OnTextChanged;
    }

    protected override void OnDetachingFrom(Entry bindable)
    {
        base.OnDetachingFrom(bindable);
        bindable.TextChanged -= OnTextChanged;
    }

    private void OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if(sender == null)
            return;

        var entry = sender as Entry;
        if (CompareToEntry != null)
        {
            bool isValid = entry?.Text == CompareToEntry.Text;
            entry!.TextColor = isValid ? Colors.Black : Colors.Coral;
        }
    }
}
