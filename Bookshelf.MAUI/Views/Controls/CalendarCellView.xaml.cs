namespace Bookshelf.MAUI.Views.Controls;

public partial class CalendarCellView : ContentView
{
    public static readonly BindableProperty DayLabelProperty = BindableProperty.Create("DayLabel", typeof(string), typeof(CalendarCellView), "");

    public string DayLabel
    {
        get => (string)GetValue(DayLabelProperty);
        set => SetValue(DayLabelProperty, value);
    }

    public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create("BackgroundColor", typeof(Color), typeof(CalendarCellView), Colors.AliceBlue);

    public Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }


    public CalendarCellView()
	{
		InitializeComponent();
	}

    
}