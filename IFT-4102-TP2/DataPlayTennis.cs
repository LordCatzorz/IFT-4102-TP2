using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT_4102_TP2
{
    class DataPlayTennis
    {
        public static DataPlayTennis ComparedToData;
        public enum Ciel
        {
            ENSOLEILLE,
            NUAGEUX,
            PLUVIEUX
        }
        public enum Temperature
        {
            CHAUDE,
            TEMPEREE,
            FROIDE
        }
        public enum Humidite
        {
            ELEVEE,
            NORMAL
        }
        public enum Vent
        {
            FORT,
            FAIBLE
        }

        public String journee { get; set; }
        public Ciel ciel { get; set; }
        public Temperature temperature { get; set; }
        public Humidite humidite { get; set; }
        public Vent vent { get; set; }
        public bool JouerTennis;

        public DataPlayTennis(String _journee, Ciel _ciel, Temperature _temperature, Humidite _humidite, Vent _vent, bool _jouerTennis = false)
        {
            ComparedToData = this;
            this.journee = _journee;
            this.ciel = _ciel;
            this.temperature = _temperature;
            this.humidite = _humidite;
            this.vent = _vent;
            this.JouerTennis = _jouerTennis;
        }

        public static double Similarity(DataPlayTennis data1, DataPlayTennis data2)
        {
            double deltaCiel = (data1.ciel == data2.ciel) ? 1 : 0;
            double deltaTemp = (data1.temperature == data2.temperature) ? 1 : 0;
            double deltaHumidite = (data1.humidite == data2.humidite) ? 1 : 0;
            double deltaVent = (data1.vent == data2.vent) ? 1 : 0;

            double weightCiel = 1;
            double weightTemp = 2;
            double weightHumdite = 1;
            double weightVent = 1;

            double sum = weightCiel * deltaCiel
                + weightHumdite * deltaHumidite
                + weightTemp * deltaTemp
                + weightVent * deltaVent;

            return sum / 4.0;
            }

        private class CompareSimilarity : IComparer<DataPlayTennis>
        {
            public int Compare(DataPlayTennis x, DataPlayTennis y)
            {

                if (DataPlayTennis.Similarity(DataPlayTennis.ComparedToData, x) < DataPlayTennis.Similarity(DataPlayTennis.ComparedToData, y))
                {
                    return 1;
                }
                if (DataPlayTennis.Similarity(DataPlayTennis.ComparedToData, x) > DataPlayTennis.Similarity(DataPlayTennis.ComparedToData, y))
                {
                    return -1;
                }
                return 0;
            }

        }

        public static IComparer<DataPlayTennis> getComparer()
        {
            return (IComparer<DataPlayTennis>)new CompareSimilarity();
        }
    }
}
