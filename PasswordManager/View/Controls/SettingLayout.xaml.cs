using Microsoft.Maui.Controls;
using System.ComponentModel;

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
        BindableProperty.Create(nameof(Orientation), typeof(OrientationEnum), typeof(SettingLayout), defaultValue: OrientationEnum.Horizontal);

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

        switch (Orientation)
        {
            case OrientationEnum.Horizontal:
                this.ControlTemplate = this.Resources["OrientationHorizontal"] as ControlTemplate;
                break;
            case OrientationEnum.Vertical:
                this.ControlTemplate = this.Resources["OrientationVertical"] as ControlTemplate;
                break;
        }
    }

    public enum OrientationEnum
    { 
        Vertical,
        Horizontal
    }
}