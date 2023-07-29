using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Services;

public partial class DashBoard : System.Web.UI.Page
{
    private static List<string> _ignorWords;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Worker"] == null)
        {
            Response.Redirect("Default.aspx");
            return;
        }

        Worker w = (Worker)Session["Worker"];

        if (w.userTypeId != 1)
        {
            Response.Redirect("AddProblemNew.aspx");
        }

        //SetIgnorWords();
        //GetWordsCount();

        var a = SetChartMonthsProblemsCount();
        //var b = SetChartTodayHoursProblemsCount();
        //var b =
        if (w.userTypeId == 1)
        {
            SetChartTodayHoursProblemsCountStacked();
        }

        var c = SetChartProblemDescEmpty();
        //var d = SetChartProblemCountForWorkerToday();
        //var dd = SetChartProblemCountForWorkerTodayCloseThem();

        var ee = SetChartProblemCountForDepartmentToday();
        var f = SetChartProblemCountForLastWeek();
        //Task.WhenAll(d);
        //Task.WhenAll(a, b, c, d, ee, f, dd);
        Task.WhenAll(a, c, ee, f);

        //Logger.GeneralLog("hi");
    }

    private static void SetIgnorWords()
    {
        _ignorWords = new List<string>();
        _ignorWords.Add("לא");
        _ignorWords.Add("כן");
        _ignorWords.Add("יש");
        _ignorWords.Add("זה");
        _ignorWords.Add("על");
        _ignorWords.Add("ידי");
        _ignorWords.Add("אין");
        _ignorWords.Add("חזר");
        _ignorWords.Add("כמה");
        _ignorWords.Add("אמר");
        _ignorWords.Add("טוב");
        _ignorWords.Add("של");
        _ignorWords.Add(",");
        _ignorWords.Add(" ");
        _ignorWords.Add("?");
        _ignorWords.Add(".");
        _ignorWords.Add("");
        _ignorWords.Add("שאלה");
    }

    protected async Task SetChartMonthsProblemsCount()
    {
        string sql = "SELECT MONTH(startTime) AS m, SUM(1) AS pCount FROM problemsClose GROUP BY MONTH(startTime)";
        DataSet ds = Dal.GetDataSet(sql);

        Object[] chartValues = new Object[12];
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    chartValues[(Int32)row[0] - 1] = row[1];
                }
            }
        }

        Highcharts chart = new Highcharts("chartMonthsProblemsCount").InitChart(new Chart { DefaultSeriesType = ChartTypes.Column });

        chart.SetTitle(new Title { Text = "סיכום תקלות חודש", X = -20 });
        chart.SetSubtitle(new Subtitle { Text = "תקלות סגורות בלבד", X = -20 });
        chart.SetXAxis(new XAxis { Categories = new[] { "ינואר", "פברואר", "מרץ", "אפריל", "מאי", "יוני", "יולי", "אוגוסט", "ספטמבר", "אוקטובר", "נובמבר", "דצמבר" } });
        chart.SetLegend(new Legend { Title = new LegendTitle { Text = "חודשים" } });

        Series s = new Series { Name = "problems<br>", Data = new Data(chartValues) };
        chart.SetSeries(s);
        chartMonthsProblemsCount.Text = chart.ToHtmlString();
    }

    protected async Task SetChartTodayHoursProblemsCountStacked()
    {
        string sql = "    SELECT problemsClose.workerId, workers.firstName + N' ' + workers.lastName AS workerName, { fn HOUR(problemsClose.startTime) } AS h, SUM(1) AS hCount " +
                "FROM problemsClose INNER JOIN workers ON problemsClose.workerId = workers.id " +
                "WHERE(problemsClose.startTime > @startTime) " +
                "GROUP BY { fn HOUR(problemsClose.startTime) }, workers.firstName + N' ' + workers.lastName, problemsClose.workerId " +
                "ORDER BY workerName, h";

        DateTime start = DateTime.Now;
        if (DateTime.Now.Hour < 6)
        {
            start = start.AddDays(-1);
            start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
        }
        else
        {
            start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
        }
        DataSet ds = Dal.GetDataSet(sql, new List<SqlParameter> { new SqlParameter("@startTime", start) });
        Dictionary<string, Object[]> workers = new Dictionary<string, Object[]>();

        Object[] chartValues = new Object[24];
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string workerName = row["workerName"].ToString().Replace("'", "").Replace(",", "");
                    if (!workers.ContainsKey(workerName))
                    {
                        chartValues = new Object[24];
                        workers.Add(workerName, chartValues);
                    }

                    int h = (Int32)row["h"];
                    h -= 6;
                    if (h < 0)
                    {
                        h += 24;
                    }
                    workers[workerName][h] = row["hCount"];
                }
            }
        }

        Highcharts chart = new Highcharts("chartTodayHoursProblemsCount").InitChart(new Chart { DefaultSeriesType = ChartTypes.Column });

        chart.SetTitle(new Title { Text = "סיכום שעות", X = -20 });
        chart.SetSubtitle(new Subtitle { Text = "סיכום תקלות לפי העובד שפתח את התקלה", X = -20 });
        chart.SetXAxis(new XAxis { Categories = new[] { "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "00", "01", "02", "03", "04", "05" } });
        chart.SetLegend(new Legend { Title = new LegendTitle { Text = "עובדים" } });

        chart.SetTooltip(new Tooltip { HeaderFormat = "<b>{point.x}</b><br/>", PointFormat = "{series.name}: {point.y}<br/>Total: {point.stackTotal}" });
        chart.SetPlotOptions(new PlotOptions { Column = new PlotOptionsColumn { Stacking = Stackings.Normal, DataLabels = new PlotOptionsColumnDataLabels { Enabled = true } } });

        List<Series> list = new List<Series>();
        foreach (var item in workers)
        {
            Series s = new Series { Name = item.Key, Data = new Data(item.Value) };
            list.Add(s);
        }

        chart.SetSeries(list.ToArray());
        chartTodayHoursProblemsCount.Text = chart.ToHtmlString();


    }

    protected async Task SetChartProblemDescEmpty()
    {

        DateTime start = DateTime.Now;
        if (DateTime.Now.Hour < 6)
        {
            start = start.AddDays(-1);
            start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
        }
        else
        {
            start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
        }

        string sql = "SELECT        problemsClose.workerId, workers.firstName + N' ' + workers.lastName AS workerName, SUM(1) AS pCount " +
                    "FROM problemsClose INNER JOIN workers ON problemsClose.workerId = workers.id " +
                    "WHERE(problemsClose.startTime > @startTime) AND(problemsClose.problemDesc = N'' OR problemsClose.problemDesc IS NULL) " +
                    "GROUP BY workers.firstName + N' ' + workers.lastName, problemsClose.workerId " +
                    "ORDER BY workerName";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@startTime", start));
        DataSet ds = Dal.GetDataSet(sql, values);

        Object[] chartValues = new Object[ds.Tables[0].Rows.Count];
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    chartValues[i] = new object[] { row["workerName"].ToString().Replace("'", "").Replace(",", ""), row["pCount"].ToString() };
                    i++;
                }
            }
        }

        Highcharts chart = new Highcharts("chartProblemDescEmpty").InitChart(new Chart { DefaultSeriesType = ChartTypes.Pie });

        chart.SetTitle(new Title { Text = "סיכום תקלות ריקות", X = -20 });
        chart.SetSubtitle(new Subtitle { Text = "תקלות מהיום שלא הזינו תיאור תקלה", X = -20 });
        chart.SetTooltip(new Tooltip { PointFormat = "{series.name}: <b>{point.y}</b><br/> <b>{point.percentage:.1f}%</b>" });

        //chart.SetTooltip(new Tooltip { HeaderFormat = "<b>{point.x}</b><br/>", PointFormat = "{series.name}: {point.y}<br/>Total: {point.stackTotal}" });

        chart.SetLegend(new Legend { Title = new LegendTitle { Text = "עובדים" } });
        chart.SetPlotOptions(new PlotOptions
        {
            Pie = new PlotOptionsPie
            {
                AllowPointSelect = true,
                Cursor = Cursors.Pointer,
                //StickyTracking=true,
                ShowInLegend = true
                //DataLabels = new PlotOptionsPieDataLabels
                //{
                //    Enabled = true//,Format = "<b>{point.name}</b>: {point.percentage:.1f} %"
                //}
            }
        });
        Series s = new Series { Name = "count", Data = new Data(chartValues) };
        Point[] pp = s.Data.SeriesData;

        chart.SetSeries(s);
        chartProblemDescEmpty.Text = chart.ToHtmlString();
    }

    public async Task SetChartProblemCountForWorkerToday()
    {
        string sql = "SELECT problemsClose.workerId, workers.firstName + N' ' + workers.lastName AS workerName, SUM(1) AS pCount " +
                    "FROM problemsClose INNER JOIN workers ON problemsClose.workerId = workers.id " +
                    "WHERE(problemsClose.startTime >= @startTime AND problemsClose.startTime < @finishTime) AND(problemsClose.problemDesc IS NOT NULL AND problemsClose.problemDesc <> N'') " +
                    "GROUP BY problemsClose.workerId, workers.firstName + N' ' + workers.lastName " +
                    "ORDER BY workerName";

        DateTime start = DateTime.Now;
        if (DateTime.Now.Hour < 6)
        {
            start = start.AddDays(-1);
            start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
        }
        else
        {
            start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
        }

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@startTime", start));
        values.Add(new SqlParameter("@finishTime", start.AddDays(1)));
        DataSet ds = Dal.GetDataSet(sql, values);

        Object[] chartValues = new Object[12];
        List<string> workersName = new List<string>();

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                chartValues = new Object[ds.Tables[0].Rows.Count];
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    chartValues[i] = row["pCount"].ToString();
                    string workerName = row["workerName"].ToString().Replace("'", "").Replace(",", "");

                    workersName.Add(workerName);
                    i++;
                }
            }
        }

        Highcharts chart = new Highcharts("chartProblemCountForWorkerToday").InitChart(new Chart { DefaultSeriesType = ChartTypes.Column });

        chart.SetTitle(new Title { Text = "כמות תקלות היום לפי עובד שפתח תקלה", X = -20 });
        chart.SetSubtitle(new Subtitle { Text = "לא כולל תקלות ריקות", X = -20 });
        chart.SetXAxis(new XAxis { Categories = workersName.ToArray() });
        //chart.SetLegend(new Legend { Title = new LegendTitle { Text = "עובדים" } });

        Series s = new Series { Name = "problems<br>", Data = new Data(chartValues) };
        chart.SetSeries(s);
        chartProblemCountForWorkerToday.Text = chart.ToHtmlString();
    }


    public async Task SetChartProblemCountForWorkerTodayCloseThem()
    {
        string sql = "SELECT problemsClose.toWorker, workers.firstName + N' ' + workers.lastName AS workerName, SUM(1) AS pCount " +
                   "FROM problemsClose INNER JOIN workers ON problemsClose.toWorker = workers.id " +
                   "WHERE(problemsClose.startTime >= @startTime AND problemsClose.startTime < @finishTime) AND(problemsClose.problemDesc IS NOT NULL AND problemsClose.problemDesc <> N'') " +
                   "GROUP BY problemsClose.toWorker, workers.firstName + N' ' + workers.lastName " +
                   "ORDER BY workerName";

        DateTime start = DateTime.Now;
        if (DateTime.Now.Hour < 6)
        {
            start = start.AddDays(-1);
            start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
        }
        else
        {
            start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
        }

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@startTime", start));
        values.Add(new SqlParameter("@finishTime", start.AddDays(1)));
        DataSet ds = Dal.GetDataSet(sql, values);

        Object[] chartValues = new Object[12];
        List<string> workersName = new List<string>();

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                chartValues = new Object[ds.Tables[0].Rows.Count];
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    chartValues[i] = row["pCount"].ToString();
                    string workerName = row["workerName"].ToString().Replace("'", "").Replace(",", "");

                    workersName.Add(workerName);
                    i++;
                }
            }
        }

        Highcharts chart = new Highcharts("chartProblemCountForWorkerTodayCloseThem").InitChart(new Chart { DefaultSeriesType = ChartTypes.Column });

        chart.SetTitle(new Title { Text = "כמות תקלות היום לפי עובד שסגר תקלות", X = -20 });
        chart.SetSubtitle(new Subtitle { Text = "לא כולל תקלות ריקות", X = -20 });
        chart.SetXAxis(new XAxis { Categories = workersName.ToArray() });
        //chart.SetLegend(new Legend { Title = new LegendTitle { Text = "עובדים" } });

        Series s = new Series { Name = "problems<br>", Data = new Data(chartValues) };
        chart.SetSeries(s);
        chartProblemCountForWorkerTodayCloseThem.Text = chart.ToHtmlString();
    }


    public async Task SetChartProblemCountForDepartmentToday()
    {
        string sql = "SELECT        problemsClose.departmentId, departments.departmentName, SUM(1) AS pCount " +
                    "FROM problemsClose INNER JOIN departments ON problemsClose.departmentId = departments.id " +
                    "WHERE(problemsClose.startTime >= @startTime) AND(problemsClose.startTime < @finishTime) AND (problemsClose.problemDesc IS NOT NULL) AND(problemsClose.problemDesc <> N'') " +
                    "GROUP BY departments.departmentName, problemsClose.departmentId " +
                    "order by departmentName";

        DateTime start = DateTime.Now;
        if (DateTime.Now.Hour < 6)
        {
            start = start.AddDays(-1);
            start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
        }
        else
        {
            start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
        }

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@startTime", start));
        values.Add(new SqlParameter("@finishTime", start.AddDays(1)));
        DataSet ds = Dal.GetDataSet(sql, values);

        Object[] chartValues = new Object[12];
        List<string> workersName = new List<string>();

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                chartValues = new Object[ds.Tables[0].Rows.Count];
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    chartValues[i] = row["pCount"].ToString();
                    workersName.Add(row["departmentName"].ToString());
                    i++;
                }
            }
        }

        Highcharts chart = new Highcharts("chartProblemCountForDepartmentToday").InitChart(new Chart { DefaultSeriesType = ChartTypes.Column });

        chart.SetTitle(new Title { Text = "כמות תקלות היום לפי מחלקה ", X = -20 });
        chart.SetSubtitle(new Subtitle { Text = "לא כולל תקלות ריקות", X = -20 });
        chart.SetXAxis(new XAxis { Categories = workersName.ToArray() });
        //chart.SetLegend(new Legend { Title = new LegendTitle { Text = "עובדים" } });

        Series s = new Series { Name = "problems<br>", Data = new Data(chartValues) };
        chart.SetSeries(s);
        chartProblemCountForDepartmentToday.Text = chart.ToHtmlString();
    }

    public async Task SetChartProblemCountForLastWeek()
    {
        string sql = "SELECT        CONVERT(VARCHAR(10), startTime, 10) AS d, SUM(1) AS pCount " +
                "FROM problemsClose " +
                "WHERE(startTime >= @startTime) AND(startTime < @finishTime) AND(problemDesc IS NOT NULL) AND(problemDesc <> N'') " +
                "GROUP BY CONVERT(VARCHAR(10), startTime, 10) " +
                "ORDER BY d";

        DateTime start = DateTime.Now.AddDays(-7);
        if (DateTime.Now.Hour < 6)
        {
            start = start.AddDays(-1);
            start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
        }
        else
        {
            start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
        }

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@startTime", start));
        values.Add(new SqlParameter("@finishTime", DateTime.Now));
        DataSet ds = Dal.GetDataSet(sql, values);

        Object[] chartValues = new Object[12];
        List<string> days = new List<string>();

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                chartValues = new Object[ds.Tables[0].Rows.Count];
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    chartValues[i] = row["pCount"].ToString();
                    string sss = row["d"].ToString();
                    DateTime d = DateTime.ParseExact(sss, "MM-dd-yy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                    days.Add(d.ToString("dd/MM/yy") + " " + d.DayOfWeek);
                    i++;
                }
            }
        }

        Highcharts chart = new Highcharts("chartProblemCountForLastWeek").InitChart(new Chart { DefaultSeriesType = ChartTypes.Column });

        chart.SetTitle(new Title { Text = "כמות תקלות בשבוע האחרון לפי יום", X = -20 });
        chart.SetSubtitle(new Subtitle { Text = "לא כולל תקלות ריקות", X = -20 });
        chart.SetXAxis(new XAxis { Categories = days.ToArray() });
        //chart.SetLegend(new Legend { Title = new LegendTitle { Text = "עובדים" } });

        Series s = new Series { Name = "problems<br>", Data = new Data(chartValues) };
        chart.SetSeries(s);
        chartProblemCountForLastWeek.Text = chart.ToHtmlString();
    }



    [WebMethod]
    public static string GetPlacesCount(string start, string finish)
    {
        //Logger.GeneralLog("Start GetPlacesCount");
        string s = "";

        string sql = "SELECT [phoneId],[phone],[placeName], Sum(1) as c " +
                     "FROM [dbo].[problemsClose] " +
                     "WHERE startTime >= '2019-01-01 00:00:00.000' AND startTime < '2019-02-01 00:00:00.000' " +
                     "Group By [phoneId],[phone],[placeName] " +
                     "HAVING Sum(1)>4 " +
                     "order by c desc;";


        sql = "SELECT[phoneId],[phone],[placeName], Sum(1) as c " +
    "FROM [dbo].[problemsClose] " +
    "WHERE startTime >=  @startTime AND startTime < @finishTime " +
    "Group By [phoneId],[phone],[placeName];";

        //Logger.GeneralLog("Start Convert Dates");
        DateTime ss = GetDate(start);// DateTime.Parse(start);
        DateTime ff = GetDate(finish);// DateTime.Parse(finish);

        //Logger.GeneralLog("Start: " + ss.ToString("dd/MM/yyyy HH:mm:ss"));
        //Logger.GeneralLog("Finish: " + ff.ToString("dd/MM/yyyy HH:mm:ss"));

        //Logger.GeneralLog("Finish Convert Dates");
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter { ParameterName = "@startTime", Value = ss });
        values.Add(new SqlParameter { ParameterName = "@finishTime", Value = ff });

        //Logger.GeneralLog("Start DS");
        DataSet ds = Dal.GetDataSet(sql, values);
        //Logger.GeneralLog("Finish DS");
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                s = ds.GetXml();
            }

        }

        //Logger.GeneralLog(s);
        return s;
    }


    private static DateTime GetDate(string s)
    {
        DateTime result = new DateTime();
        string[] a = s.Split(' ');
        if (a.Length > 1)
        {
            string[] dates = a[0].Split('/');
            string[] times = a[1].Split(':');

            int year = GetInt(dates[2]);
            if (year < 2000)
            {
                year += 2000;
            }
            int month = GetInt(dates[1]);
            int day = GetInt(dates[0]);
            int hour = GetInt(times[0]);
            int min = GetInt(times[1]);
            int sec = GetInt(times[2]);

            result = new DateTime(year, month, day, hour, min, sec);
        }


        return result;
    }

    private static int GetInt(string s)
    {
        int result = 0;
        int.TryParse(s, out result);

        return result;
    }

    [WebMethod]
    public static string GetProblems(int phoneId, string start, string finish)
    {
        string s = "";

        string sql =

                "SELECT problemsClose.id, problemsClose.workerId, problemsClose.phoneId, problemsClose.phone, problemsClose.ip, problemsClose.placeNameId, problemsClose.placeName, problemsClose.customerName, problemsClose.problemDesc, problemsClose.problemSolution, problemsClose.statusId, problemsClose.emergencyId, problemsClose.departmentId, problemsClose.reportToYaron, problemsClose.startTime, problemsClose.finishTime, problemStatus.statusName, workers.firstName + N' ' + workers.lastName AS workerName, departments.departmentName, emergencyTypes.emergencyName, problemsClose.toWorker, workers_1.firstName + N' ' + workers_1.lastName AS toWorkerName, problemsClose.HaveLog " +
                "FROM problemsClose INNER JOIN workers ON problemsClose.workerId = workers.id INNER JOIN problemStatus ON problemsClose.statusId = problemStatus.id INNER JOIN emergencyTypes ON problemsClose.emergencyId = emergencyTypes.id INNER JOIN departments ON problemsClose.departmentId = departments.id INNER JOIN workers AS workers_1 ON problemsClose.toWorker = workers_1.id " +
                "WHERE  (startTime >= @startTime) AND (startTime < @finishTime) AND (phoneId=@phoneId) " +
                "Order By id Desc";


        //DateTime start = new DateTime(2019, 2, 1, 0, 0, 0);
        //DateTime finish = new DateTime(2019, 3, 1, 0, 0, 0);

        DateTime ss = GetDate(start);// DateTime.Parse(start);
        DateTime ff = GetDate(finish);// DateTime.Parse(finish);


        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter { ParameterName = "@startTime", Value = ss });
        values.Add(new SqlParameter { ParameterName = "@finishTime", Value = ff });
        values.Add(new SqlParameter { ParameterName = "@phoneId", Value = phoneId });

        DataSet ds = Dal.GetDataSet(sql, values);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                s = ds.GetXml();
            }

        }
        return "";
        return s;
    }

    public static string GetWordsCount()
    {
        string sql =
                   "SELECT problemsClose.problemDesc " +
                   "FROM problemsClose  " +
                   "WHERE  (startTime >= @startTime) AND (startTime < @finishTime)";

        DateTime start = CacheHelper.Instance.GetIsraelTime();// DateTime.Now;
                                                              //if (DateTime.Now.Hour < 6)
                                                              //{

        //}
        //else
        //{
        //    start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
        //}
        start = start.AddDays(-7);
        start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@startTime", start));
        values.Add(new SqlParameter("@finishTime", start.AddDays(1)));
        DataSet ds = Dal.GetDataSet(sql, values);

        Dictionary<string, int> wordsCount = new Dictionary<string, int>();
        Dictionary<string, int> wordsDoubleCount = new Dictionary<string, int>();
        Dictionary<string, int> wordsThreeCount = new Dictionary<string, int>();
        Dictionary<string, int> wordsFourCount = new Dictionary<string, int>();
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string desc = item["problemDesc"].ToString();
                        if (string.IsNullOrEmpty(desc))
                        {
                            continue;
                        }

                        string[] words = desc.Split(' ');
                        foreach (var word in words)
                        {
                            if (!ignorWord(word))
                            {
                                if (!wordsCount.ContainsKey(word))
                                {
                                    wordsCount.Add(word, 0);
                                }
                                wordsCount[word] += 1;
                            }
                        }

                        if (words.Length > 1)
                        {
                            for (int i = 0; i < words.Length; i++)
                            {
                                if ((i + 1) < words.Length)
                                {
                                    string coupleWords = words[i] + " " + words[i + 1];

                                    if (!wordsDoubleCount.ContainsKey(coupleWords))
                                    {
                                        wordsDoubleCount.Add(coupleWords, 0);
                                    }
                                    wordsDoubleCount[coupleWords] += 1;
                                }
                            }
                        }

                        if (words.Length > 2)
                        {
                            for (int i = 0; i < words.Length; i++)
                            {
                                if ((i + 2) < words.Length)
                                {
                                    string threeWords = words[i] + " " + words[i + 1] + " " + words[i + 2];

                                    if (!wordsThreeCount.ContainsKey(threeWords))
                                    {
                                        wordsThreeCount.Add(threeWords, 0);
                                    }
                                    wordsThreeCount[threeWords] += 1;
                                }
                                //wordsDoubleCount
                            }
                        }

                        if (words.Length > 3)
                        {
                            for (int i = 0; i < words.Length; i++)
                            {
                                if ((i + 3) < words.Length)
                                {
                                    string fourWords = words[i] + " " + words[i + 1] + " " + words[i + 2] + " " + words[i + 3];

                                    if (!wordsFourCount.ContainsKey(fourWords))
                                    {
                                        wordsFourCount.Add(fourWords, 0);
                                    }
                                    wordsFourCount[fourWords] += 1;
                                }
                                //wordsDoubleCount
                            }
                        }

                    }
                }
            }
        }

        Dictionary<string, int> tempWords = new Dictionary<string, int>();
        foreach (var item in wordsCount)
        {
            if (item.Value > 5)
            {
                tempWords.Add(item.Key, item.Value);
            }
        }

        Dictionary<string, int> tempTwo = new Dictionary<string, int>();
        foreach (var item in wordsDoubleCount)
        {
            if (item.Value > 3)
            {
                tempTwo.Add(item.Key, item.Value);
            }
        }

        Dictionary<string, int> tempThree = new Dictionary<string, int>();
        foreach (var item in wordsThreeCount)
        {
            if (item.Value > 2)
            {
                tempThree.Add(item.Key, item.Value);
            }
        }

        Dictionary<string, int> tempFour = new Dictionary<string, int>();
        foreach (var item in wordsFourCount)
        {
            if (item.Value > 1)
            {
                tempFour.Add(item.Key, item.Value);
            }
        }

        return "";
    }

    private static bool ignorWord(string word)
    {
        return _ignorWords.Contains(word);
    }
}