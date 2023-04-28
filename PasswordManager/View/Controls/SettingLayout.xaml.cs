namespace PasswordManager.View.Controls;
#nullable enable

/// <summary>
/// Represents row or column of a setting
/// </summary>
public partial class SettingLayout : ContentView
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(SettingLayout), string.Empty);

    public static readonly BindableProperty OrientationProperty =
        BindableProperty.Create(nameof(Orientation), typeof(OrientationEnum), typeof(SettingLayout),
            OrientationEnum.Horizontal, propertyChanged: OrientationChanged);

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

    private static void OrientationChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var layout = (SettingLayout)bindable;

        SetOrientation(layout, (OrientationEnum)newValue);
        layout.InvalidateMeasure();
    }

    private static void SetOrientation(SettingLayout layout, OrientationEnum orientation)
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