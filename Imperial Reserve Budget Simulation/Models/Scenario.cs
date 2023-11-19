using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imperial_Reserve_Budget_Simulation.Models;

public class Scenario
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Action<bool> RunAction { get; set; }

    public Scenario(string name, string description, Action<bool> runaction)
    {
        Name = name;
        Description = description;
        RunAction = runaction;
    }
}

public static class ScenariosData
{
    public static List<Scenario> Scenarios = new()
    {
        new Scenario("Normal", "Normal", x => {
        }),
        new Scenario("PIS Monthly Growth (4%)", "PIS Monthly Growth of 4%", x => {
            Vooperia.PIS.ExpectedMonthlyGrowth = 0.04;
        }),
        new Scenario("PIS Monthly Growth (6%)", "PIS Monthly Growth of 6%", x => {
            Vooperia.PIS.ExpectedMonthlyGrowth = 0.06;
        }),
        new Scenario("PIS Monthly Growth (8%)", "PIS Monthly Growth of 8%", x => {
            Vooperia.PIS.ExpectedMonthlyGrowth = 0.08;
        })
    };
}