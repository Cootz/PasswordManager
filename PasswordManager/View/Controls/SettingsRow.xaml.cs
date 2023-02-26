using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace PasswordManager.View.Controls;

/// <summary>
/// Represents row of settings screen in format Title Content
/// </summary>
public partial class SettingsRow : ContentView
{
	[Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
	public string Title { get; set; }

    [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
    public ContentView Content { get; set; }

	public SettingsRow()
	{
		InitializeComponent();
	}
}