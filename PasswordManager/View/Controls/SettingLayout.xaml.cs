using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PasswordManager.View.Controls;

#nullable enable

/// <summary>
/// Represents row of settings screen in format | Title|Content |
/// </summary>
public partial class SettingLayout : ContentView
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(SettingLayout), defaultValue: string.Empty);

    public static readonly BindableProperty OrientationProperty =
        BindableProperty.Create(nameof(Orientation), typeof(OrientationEnum), typeof(SettingLayout), defaultValue: OrientationEnum.Horizontal, propertyChanged: OrientationChanged);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public OrientationEnum Orientation
    {
        get => (OrientationEnum)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    public SettingLayout()
	{
        InitializeComponent();

        SetOrientation(this, Orientation);
    }

    static void OrientationChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var layout = (SettingLayout)bindable;

        SetOrientation(layout, (OrientationEnum)newValue);
        layout.InvalidateMeasure();
    }

    static void SetOrientation(SettingLayout layout, OrientationEnum orientation)
    {
        switch (orientation)
        {
            case OrientationEnum.Horizontal:
                layout.ControlTemplate = layout.Resources["OrientationHorizontal"] as ControlTemplate;
                break;
            case OrientationEnum.Vertical:
                layout.ControlTemplate = layout.Resources["OrientationVertical"] as ControlTemplate;
                break;
        }
    }

    public enum OrientationEnum
    { 
        Vertical,
        Horizontal
    }
}