using static Android.Content.ClipData;

namespace Bookshelf.MAUI.Views.Controls;

public partial class CalendarView : ContentView
{
    private DateTime _ym = new();
    private Dictionary<int, CalendarCellView> _dayCellDic = new();

    public CalendarView()
	{
		InitializeComponent();

        _ym = new DateTime(2023, 3, 1);


        var lastDay = _ym.AddMonths(1).AddDays(-1).Day;
        var day = 1;
        foreach (var child in CalendarGrid.Children.Skip((int)_ym.DayOfWeek).Take(lastDay))
        {
            //_dayCellDic.Add(day, child);
            _dayCellDic[day] = (CalendarCellView)child;
            ((CalendarCellView)child).DayLabel = day.ToString();
            day++;
        }

        var today = DateTime.Today;
        if (_ym.Year == today.Year && _ym.Month == today.Month)
        {
            _dayCellDic[today.Day].BackgroundColor = Colors.LightCoral;
        }
    }
}