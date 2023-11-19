using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imperial_Reserve_Budget_Simulation.Models;

public class IncomeTax
{
    public double Rate { get; set; }
    public double From { get; set; }
    public double To { get; set; }

    public IncomeTax(double rate, double from, double to)
    {
        Rate = rate;
        From = from;
        To = to;
    }
}

public class Budget
{
    public List<IncomeTax> PersonalIncomeTaxes = new()
    {
    };
    public List<IncomeTax> CorporateIncomeTaxes = new()
    {
    };
    public Dictionary<Rank, double> UBI = new()
    {
        { Rank.Spleen,  215 },
        { Rank.Crab, 90 },
        { Rank.Gaty, 67 },
        { Rank.Corgi, 48 },
        { Rank.Oof, 36 },
        { Rank.Unranked, 18 }
    };
    public double Balance { get; set; } = 1_500_000;
}