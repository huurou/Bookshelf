using static Android.Content.ClipData;

namespace Bookshelf.MAUI.Views.Controls;

public partial class CalendarView : ContentView
{
    public static readonly BindableProperty YearMonthProperty = BindableProperty.Create("YearMonth", typeof(DateTime), typeof(CalendarCellView), DateTime.Today);

    public DateTime YearMonth
    {
        get => (DateTime)GetValue(YearMonthProperty);
        set
        {
            SetValue(YearMonthProperty, value);

            var ym = new DateTime(value.Year, value.Month, 1);
            YearMonthLabel.Text = ym.ToString("yyyyîNMMåé");

            //èâä˙âª
            foreach (var child in CalendarGrid.Children)
            {
                ((CalendarCellView)child).DayLabel = "";
                ((CalendarCellView)child).BackgroundColor = Colors.AliceBlue;
            }

            var lastDay = ym.AddMonths(1).AddDays(-1).Day;
            var day = 1;
            foreach (var child in CalendarGrid.Children.Skip((int)ym.DayOfWeek).Take(lastDay))
            {
                _dayCellDic[day] = (CalendarCellView)child;
                ((CalendarCellView)child).DayLabel = day.ToString();
                day++;
            }

            var today = DateTime.Today;
            if (ym.Year == today.Year && ym.Month == today.Month)
            {
                _dayCellDic[today.Day].BackgroundColor = Colors.LightCoral;
            }
        }
    }

    private DateTime _ym = new();
    private Dictionary<int, CalendarCellView> _dayCellDic = new();

    public CalendarView()
	{
		InitializeComponent();
    }
}