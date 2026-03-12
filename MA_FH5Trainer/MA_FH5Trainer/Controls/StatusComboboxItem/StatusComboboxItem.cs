using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;

namespace XPaint.Controls.StatusComboboxItem;

public class StatusComboboxItem : ComboBoxItem
{
    public static readonly DependencyProperty IsOnProperty
        = DependencyProperty.Register(nameof(IsOn),
            typeof(bool),
            typeof(StatusComboboxItem),
            new PropertyMetadata(default(bool)));
    
    [Bindable(true)]
    [Category("XPaint")]
    public bool IsOn
    {
        get => (bool)GetValue(IsOnProperty);
        set => SetValue(IsOnProperty, value);
    }

    static StatusComboboxItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(StatusComboboxItem), new FrameworkPropertyMetadata(typeof(StatusComboboxItem)));
    }
}