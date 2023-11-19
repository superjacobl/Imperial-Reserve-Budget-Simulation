using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imperial_Reserve_Budget_Simulation;

public static class Functions
{
    static char[] endings = { 'k', 'm' };
    public static long ParseString(string s)
    {
        s = s.Replace(",", "");
        var lastchar = s.Last();
        if (!endings.Contains(lastchar))
            return long.Parse(s);
        s = s.Replace(lastchar.ToString(), "");
        var num = long.Parse(s);
        num *= lastchar switch
        {
            'k' => 1_000,
            'm' => 1_000_000
        };
        return num;
    }

    public static (long muit, double amount, string symbol) GetValues(double num, bool NoK = false)
    {
        long muit = 1;
        string symbol = "";
        if (num > 0)
            if (NoK)
            {
                if (num > 1_000_000)
                {
                    muit *= 1_000_000;
                    num /= 1_000_000;
                    symbol = "m";
                    if (num > 1000)
                    {
                        muit *= 1000;
                        num /= 1000;
                        symbol = "b";
                    }
                }
            }
            else
            {
                if (num > 1000)
                {
                    muit = 1000;
                    num /= 1000;
                    symbol = "k";
                }
                if (num > 1000)
                {
                    muit *= 1000;
                    num /= 1000;
                    symbol = "m";
                }
                if (num > 1000)
                {
                    muit *= 1000;
                    num /= 1000;
                    symbol = "b";
                }
            }
        else
        {
            double nn = num * -1;
            if (NoK)
            {
                if (nn > 1_000_000)
                {
                    muit *= 1_000_000;
                    nn /= 1_000_000;
                    symbol = "m";
                    if (nn > 1000)
                    {
                        muit *= 1000;
                        nn /= 1000;
                        symbol = "b";
                    }
                }
            }
            else
            {
                if (nn > 1000)
                {
                    muit = 1000;
                    nn /= 1000;
                    symbol = "k";
                }
                if (nn > 1000)
                {
                    muit *= 1000;
                    nn /= 1000;
                    symbol = "m";
                }
                if (nn > 1000)
                {
                    muit *= 1000;
                    nn /= 1000;
                    symbol = "b";
                }
            }
            nn *= -1;
            return (muit, nn, symbol);
        }
        return (muit, num, symbol);
    }

    public static string Format(decimal Value, bool AddPlusSign = false, bool WholeNum = false, int Rounding = 2, bool NoK = false, string ExtraSymbol = "", bool KeepDecimalIfUnder1K = false, bool UseDecimalIfOver1K = false)
    {
        var data = GetValues((double)Value, NoK);
        if (WholeNum)
        {
            if (UseDecimalIfOver1K && Value >= 1000.00m)
                WholeNum = false;
            if (KeepDecimalIfUnder1K && Value <= 999.99m)
                WholeNum = false;
        }
        if (WholeNum)
        {
            long amount = (long)data.amount;
            if (AddPlusSign)
            {
                if (amount >= 0)
                    return $"+{ExtraSymbol}{amount.ToString("#,##0")}{data.symbol}";
                return $"-{ExtraSymbol}{(-1 * amount).ToString("#,##0")}{data.symbol}";
            }
            return $"{ExtraSymbol}{amount.ToString("#,##0")}{data.symbol}";
        }
        else
        {
            double amount = Math.Round(data.amount, Rounding);
            if (AddPlusSign)
            {
                if (amount >= 0)
                    return $"+{ExtraSymbol}{amount.ToString("#,##0.##")}{data.symbol}";
                return $"-{ExtraSymbol}{(-1 * amount).ToString("#,##0.##")}{data.symbol}";
            }
            return $"{ExtraSymbol}{amount.ToString("#,##0.##")}{data.symbol}";
        }
    }

    public static string Format(long Value, bool AddPlusSign = false, bool WholeNum = false, int Rounding = 2, bool NoK = false, string ExtraSymbol = "", bool KeepDecimalIfUnder1K = false, bool UseDecimalIfOver1K = false)
    {
        return Format((decimal)Value, AddPlusSign, WholeNum, Rounding, NoK, ExtraSymbol, KeepDecimalIfUnder1K, UseDecimalIfOver1K);
    }
    public static string Format(double Value, bool AddPlusSign = false, bool WholeNum = false, int Rounding = 2, bool NoK = false, string ExtraSymbol = "", bool KeepDecimalIfUnder1K = false, bool UseDecimalIfOver1K = false)
    {
        return Format((decimal)Value, AddPlusSign, WholeNum, Rounding, NoK, ExtraSymbol, KeepDecimalIfUnder1K, UseDecimalIfOver1K);
    }
}