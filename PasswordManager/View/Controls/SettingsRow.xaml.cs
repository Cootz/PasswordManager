using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace PasswordManager.View.Controls;

#nullable enable

/// <summary>
/// Represents row of settings screen in format | Title|Content |
/// </summary>
public partial class SettingsRow : ContentView
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(SettingsRow), defaultValue: string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    //[Bindable(true)]
    //public string Title { get; set; } = string.Empty;

    public SettingsRow()
	{
		InitializeComponent();
	}


}