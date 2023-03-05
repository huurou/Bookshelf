using Windows.Storage.Streams;

namespace Bookshelf.MAUI.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

        _ym = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        Calendar.YearMonth = _ym;

        image.Source = "";
    }

    private DateTime _ym = new();

    private void L_Button_Clicked(object sender, EventArgs e)
    {
        _ym = _ym.AddMonths(-1);
        Calendar.YearMonth = _ym;
    }

    private void R_Button_Clicked(object sender, EventArgs e)
    {
        _ym = _ym.AddMonths(1);
        Calendar.YearMonth = _ym;
    }
}