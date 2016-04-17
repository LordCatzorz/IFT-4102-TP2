using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT_4102_TP2
{
    class PDM
    {
        double epison = Math.Pow(10, -3);
        double gamma = 0.9;
        double p1 = 0.3;
        double p2 = 0.3;

        enum State { S0, S1, S2, S3 }

        enum Action { a, b, c }

        List<Pair<State, double>> reward;
        List<Pair<State, State, double>> probabilityT;
        List<Pair<State, double>> utility;

        public PDM()
        {
            probabilityT = new List<Pair<State, State, double>>();
            probabilityT.Add(new Pair<State, State, double>(State.S0, State.S1, 1));
            probabilityT.Add(new Pair<State, State, double>(State.S0, State.S2, 1));
            probabilityT.Add(new Pair<State, State, double>(State.S1, State.S1, 1 - p2));
            probabilityT.Add(new Pair<State, State, double>(State.S1, State.S3, p2));
            probabilityT.Add(new Pair<State, State, double>(State.S2, State.S0, 1 - p1));
            probabilityT.Add(new Pair<State, State, double>(State.S2, State.S3, p1));
            probabilityT.Add(new Pair<State, State, double>(State.S3, State.S0, 1));

            reward = new List<Pair<State, double>>();
            reward.Add(new Pair<State, double>(State.S0, 0));
            reward.Add(new Pair<State, double>(State.S1, 0));
            reward.Add(new Pair<State, double>(State.S2, 1));
            reward.Add(new Pair<State, double>(State.S3, 10));

            utility = new List<Pair<State, double>>();
            utility.Add(new Pair<State, double>(State.S0, 0));
            utility.Add(new Pair<State, double>(State.S1, 0));
            utility.Add(new Pair<State, double>(State.S2, 0));
            utility.Add(new Pair<State, double>(State.S3, 0));
        }

        private class Pair<T1, T2> : ICloneable
        {
            public Pair(T1 t1, T2 t2)
            {
                Item1 = t1;
                Item2 = t2;
            }
            public T1 Item1 { get; set; }
            public T2 Item2 { get; set; }

            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }

        private class Pair<T1, T2, T3> : ICloneable
        {
            public Pair(T1 t1, T2 t2, T3 t3)
            {
                Item1 = t1;
                Item2 = t2;
                Item3 = t3;
            }
            public T1 Item1 { get; set; }
            public T2 Item2 { get; set; }
            public T3 Item3 { get; set; }

            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }

        public string SolveQ2()
        {
            bool modified = true;
            while (modified)
            {
                modified = false;

                List<Pair<State, double>> oldUtility = new List<Pair<State, double>>(utility.Count);
                utility.ForEach((item) =>
                {
                    oldUtility.Add(new Pair<State, double>(item.Item1, item.Item2));
                });


                foreach (Pair<State, double> u in utility)
                {
                    State stateS = u.Item1;
                    double immediateReward = reward.Where(r => r.Item1 == stateS).Single().Item2;
                    double sum = 0;
                    foreach (var t in probabilityT.Where(proT => proT.Item1 == stateS))
                    {
                        State stateSPrime = t.Item2;
                        double utilitySPrime = oldUtility.Where(s => s.Item1 == stateSPrime).Single().Item2;
                        sum += t.Item3 * utilitySPrime;
                    }
                    double newUtility = immediateReward + gamma * sum;
                    if (newUtility > u.Item2 && Math.Abs(newUtility - u.Item2) > epison)
                    {
                        u.Item2 = newUtility;
                        modified = true;
                    }
                }
            }

            return string.Format("V*(S0) = {0}, V*(S1) = {1} ,V*(S2) = {2}, V*(S3) = {3}", utility[0].Item2, utility[1].Item2, utility[2].Item2, utility[3].Item2);
        }
    }
}
