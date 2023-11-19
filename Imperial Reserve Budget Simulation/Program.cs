using BetterConsoles.Colors.Extensions;
using BetterConsoles.Tables.Models;
using BetterConsoles.Tables;
using Imperial_Reserve_Budget_Simulation;
using Imperial_Reserve_Budget_Simulation.Models;
using System.Drawing;
using BetterConsoles.Tables.Builders;
using BetterConsoles.Tables.Configuration;

var records = new List<VooperiaRecord>();

var months = 12;

foreach (var scenario in ScenariosData.Scenarios)
{
    Vooperia.Budget = new();
    Vooperia.PIS = new();

    scenario.RunAction(true);

    var users = new List<User>();
    users.Add(new User() { YearlyPay = 500_000, Rank = Rank.Gaty });
    for (int i = 0; i < 1; i++)
        users.Add(new User() { YearlyPay = 50_000 * 1.75, Rank = Rank.Spleen });
    for (int i = 0; i < 5; i++)
        users.Add(new User() { YearlyPay = 50_000 * 1.45, Rank = Rank.Crab });
    for (int i = 0; i < 10; i++)
        users.Add(new User() { YearlyPay = 50_000 * 1.25, Rank = Rank.Gaty });
    for (int i = 0; i < 20; i++)
        users.Add(new User() { YearlyPay = 50_000 * 1.15, Rank = Rank.Corgi });
    for (int i = 0; i < 30; i++)
        users.Add(new User() { YearlyPay = 50_000 * 1, Rank = Rank.Oof });
    for (int i = 0; i < 34; i++)
        users.Add(new User() { YearlyPay = 50_000 * 0.9, Rank = Rank.Unranked });

    for (int i = 0; i < 31 * months; i++)
    {
        users.ForEach(x => x.Tick(i));
        Vooperia.PIS.Tick(i);
        Vooperia.Tick(i);
    }
    records.Add(new()
    {
        Budget = Vooperia.Budget,
        PIS = Vooperia.PIS,
        Scenario = scenario,
        Users = users,
        GDP = Vooperia.GDP
    });
}

Color positiveMoney = Color.FromArgb(152, 168, 75);
Color negativeMoney = Color.IndianRed;
string FormatMoney(double money)
{
    string valueStr = string.Format("¢{0:n0}", money);

    if (money >= 0)
    {
        valueStr = valueStr.ForegroundColor(positiveMoney);
    }
    else
    {
        valueStr = valueStr.ForegroundColor(negativeMoney);
    }

    return valueStr;
}

CellFormat headerFormat = new CellFormat()
{
    Alignment = Alignment.Center,
    ForegroundColor = Color.Magenta
};

Table table = new TableBuilder(headerFormat)
    .AddColumn("Scenario", rowsFormat: new CellFormat(foregroundColor: Color.FromArgb(128, 129, 126)))
    .AddColumn("Month")
        .RowsFormat()
            .ForegroundColor(Color.FromArgb(100, 160, 179))
    .AddColumn("PIS Balance")
        .RowFormatter<double>((x) => FormatMoney(x))
        .RowsFormat()
            .Alignment(Alignment.Right)
    .AddColumn("PIS Balance Change")
        .RowFormatter<double>((x) => FormatMoney(x))
        .RowsFormat()
            .Alignment(Alignment.Right)
    .Build();
table.Config = TableConfig.Unicode();

foreach (var record in records)
{
    int j = 1;
    double prev = record.PIS.BalanceHistory.First();
    foreach (var item in record.PIS.BalanceHistory)
    {
        table.AddRow(record.Scenario.Name, $"Month {j}", item, item - prev);
        j++;
        prev = item;
    }
}

Console.Write(table.ToString());

table = new TableBuilder(headerFormat)
    .AddColumn("Month")
        .RowsFormat()
            .ForegroundColor(Color.FromArgb(100, 160, 179))
    .AddColumn("Deposited into PIS")
        .RowFormatter<double>((x) => FormatMoney(x))
        .RowsFormat()
            .Alignment(Alignment.Right)
    .AddColumn("Free Deposited Amount")
        .RowFormatter<double>((x) => FormatMoney(x))
        .RowsFormat()
            .Alignment(Alignment.Right)
    .AddColumn("Received Back")
        .RowFormatter<double>((x) => FormatMoney(x))
        .RowsFormat()
            .Alignment(Alignment.Right)
    .AddColumn("Ratio")
        .RowFormatter<double>((x) => $"{x:n2}x".ForegroundColor(Color.BlueViolet))
        .RowsFormat()
            .Alignment(Alignment.Right)
  //  .AddColumn("Deposited:Got Ratio")
   //     .RowFormatter<double>((x) => $"{x:n2}x".ForegroundColor(Color.BlueViolet))
   //     .RowsFormat()
  //          .Alignment(Alignment.Right)
    .Build();
table.Config = TableConfig.Unicode();

// me
var user = records[0].Users[0];
//var user = records[0].Users.FirstOrDefault(x => x.PISHistory.Any(r => r.Deposited / r.Got < 1.00));
//if (user is null)
//    Console.WriteLine("HOW");

int k = 0;
double prevdeposited = 0;
double prevgot = 0;
foreach (var record in user.PISHistory)
{
    var inflow = record.Deposited - prevdeposited;
    var outflow = record.Got - prevgot;
    table.AddRow($"Month {k}", record.Deposited, record.BonusDeposited, record.Got, inflow / outflow);//, record.Deposited/record.Got);
    k++;

    prevdeposited = record.Deposited;
    prevgot = record.Got;
}

Console.Write(table.ToString());

Console.WriteLine($"{records[0].Budget.Balance:n0}");

Console.WriteLine($"{records[0].GDP:n0}");