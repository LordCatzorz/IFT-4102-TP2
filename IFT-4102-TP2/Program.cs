using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT_4102_TP2
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver bnc = new Solver();
            Tuple<bool,double> answer = (bnc.SolveQ3(DataPlayTennis.Ciel.NUAGEUX, DataPlayTennis.Temperature.FROIDE, DataPlayTennis.Humidite.NORMAL, DataPlayTennis.Vent.FAIBLE));

            Console.WriteLine(String.Format("The answer to Q3 is {0} with a probability of {1}", answer.Item1, answer.Item2));

            answer = (bnc.SolveQ4(DataPlayTennis.Ciel.NUAGEUX, DataPlayTennis.Temperature.FROIDE, DataPlayTennis.Humidite.NORMAL, DataPlayTennis.Vent.FAIBLE));

            Console.WriteLine(String.Format("The answer to Q4 is {0} with a approximate probability of {1}", answer.Item1, answer.Item2));
            Console.ReadLine();
        }
    }
}
