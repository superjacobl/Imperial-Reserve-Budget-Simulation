using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imperial_Reserve_Budget_Simulation.Models;

public enum Rank
{
    Spleen,
    Crab,
    Gaty,
    Corgi,
    Oof,
    Unranked
}

public class PISDayData
{
    public double Deposited { get; set; }
    public double BonusDeposited { get; set; }
    public double Got { get; set; }

    public PISDayData(double deposited, double bonusdeposited, double got)
    {
        Deposited = deposited;
        BonusDeposited = bonusdeposited;
        Got = got;
    }
}

public class User
{
    public double YearlyPay { get; set; }
    public double UBI { get; set; }
    public Rank Rank { get; set; }

    public double DepositedIntoPIS { get; set; }
    public double FreelyDepositIntoPIS { get; set; }
    public double GotFromPIS { get; set; }
    public List<PISDayData> PISHistory { get; set; } = new();

    public void Tick(int i)
    {
        double amount = 0;
        foreach (var item in Vooperia.PIS.PayoutSchemes)
        {
            amount += item.CalcAmount(this);
        }
        amount /= 30;
        Vooperia.PIS.Balance -= amount;
        GotFromPIS += amount;

        var payinto = 0.0;
        var dailypay = YearlyPay / 365.24;
        payinto += dailypay * Vooperia.PIS.PISTax;

        var beforepayinto = payinto;
        foreach (var item in Vooperia.PIS.PISBonuses)
        {
            if (DepositedIntoPIS < item.Under)
            {
                payinto *= item.Bonus;
            }
        }
        Vooperia.Budget.Balance -= payinto - beforepayinto;
        FreelyDepositIntoPIS += payinto - beforepayinto;
        DepositedIntoPIS += payinto;
        Vooperia.PIS.Balance += payinto;

        var amountfromubi = Vooperia.Budget.UBI[Rank] * Vooperia.PIS.UBIBonus;
        FreelyDepositIntoPIS += amountfromubi;
        DepositedIntoPIS += amountfromubi;
        Vooperia.PIS.Balance += amountfromubi;
        Vooperia.Budget.Balance -= amountfromubi;

        if (i % 30 == 0 && i != 0) 
            PISHistory.Add(new(DepositedIntoPIS, FreelyDepositIntoPIS, GotFromPIS));
    }
}
