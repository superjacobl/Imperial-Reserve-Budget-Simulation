using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imperial_Reserve_Budget_Simulation.Models;

public class VooperiaRecord
{
    public Scenario Scenario { get; set; }
    public Budget Budget { get; set; }
    public PIS PIS { get; set; }
    public List<User> Users { get; set; }
    public double GDP { get; set; }
}

public static class Vooperia
{
    public static Budget Budget = new();
    public static PIS PIS = new();
    public static double GDP = new();
    public static Consts Consts = new();

    public static void Tick(int i)
    {
        // avg sizes of 4
        double FactoryCount = 300;
        double MineCount = 450;

        GDP = 0;

        // inputs & outputs for factories
        //GDP += FactoryCount * (4 * Consts.AvgerageResourcePrice);
        GDP += FactoryCount * (40 * Consts.AvgerageResourcePrice);

        // same for mines
        //GDP += MineCount * (0.25 * Consts.AvgerageResourcePrice * 2);
        GDP += MineCount * (30 * Consts.AvgerageResourcePrice * 0.5);

        GDP *= 365.24 * 24;
    }
}
