using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imperial_Reserve_Budget_Simulation.Models;

public class PISPayoutScheme
{
    public double Rate { get; set; }
    public double From { get; set; }
    public double To { get; set; }
    public PISPayoutScheme(double rate, double from, double to)
    {
        Rate = rate;
        From = from;
        To = to;
    }

    public double CalcAmount(User user)
    {
        if (user.DepositedIntoPIS < From) return 0;
        var diff = user.DepositedIntoPIS - From;
        if (user.DepositedIntoPIS > To) diff = To - From;

        return diff * Rate;
    }
}

public class PISBonus
{
    public double Bonus { get; set; }
    public double Under { get; set; }

    public PISBonus(double bonus, double under)
    {
        Bonus = bonus;
        Under = under;
    }
}

public class PIS
{
    public double Balance = 1_500_000;
    public double PISTax = 0.0825;
    public double UBIBonus = 0.4;

    public double ExpectedMonthlyGrowth = 0.02;

    public List<PISPayoutScheme> PayoutSchemes = new()
    {
        new(0.14, 0, 5_250),
        new(0.10, 5_250, 9_750),
        new(0.075, 9_750, 18_000),
        new(0.045, 18_000, 27_500),
        new(0.0325, 27_500, 1000000000000)
    };

    public List<PISBonus> PISBonuses = new()
    {
        new(3.5, 5000),
        new(2, 15_000)
    };

    public List<double> BalanceHistory = new();

    public void Tick(int i)
    {
        Balance += ExpectedMonthlyGrowth / 30 * Balance;
        if (i%30 == 0 && i != 0)
            BalanceHistory.Add(Balance);
    }
}
