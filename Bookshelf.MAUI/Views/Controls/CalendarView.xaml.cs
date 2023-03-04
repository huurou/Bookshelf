using static Android.Content.ClipData;

namespace Bookshelf.MAUI.Views.Controls;

public partial class CalendarView : ContentView
{
    private List<IView> _gridItems = new();

    public CalendarView()
	{
		InitializeComponent();

        var ym = new DateTime(2023, 4, 1);

        foreach (var child in CalendarGrid.Children)
        {
            _gridItems.Add(child);
        }

        var lastDay = ym.AddMonths(1).AddDays(-1).Day;
        for (var day = 1; day <= lastDay; day++)
        {
            ((Label)_gridItems[6 + (int)ym.DayOfWeek + day]).Text = day.ToString();
        }
    }
}