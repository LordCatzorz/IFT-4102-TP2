using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT_4102_TP2
{
    class Solver
    {
        List<DataPlayTennis> data;
        public Solver()
        {
            this.data = new List<DataPlayTennis>();
        }

        public Tuple<bool, double> SolveQ3(DataPlayTennis.Ciel _ciel, DataPlayTennis.Temperature _temperature, DataPlayTennis.Humidite _humidite, DataPlayTennis.Vent _vent)
        {
            addQ3Dataset();
            double nbData = this.data.Count;

            double nbJouerTrue = this.data.Where(d => d.JouerTennis == true).Count();
            double nbJouerFalse = this.data.Where(d => d.JouerTennis == false).Count();

            double nbCielAndJouerTrue = this.data.Where(d => d.JouerTennis == true && d.ciel == _ciel).Count();
            double nbCielAndJouerFalse = this.data.Where(d => d.JouerTennis == false && d.ciel == _ciel).Count();

            double nbTempAndJouerTrue = this.data.Where(d => d.JouerTennis == true && d.temperature == _temperature).Count();
            double nbTempAndJouerFalse = this.data.Where(d => d.JouerTennis == false && d.temperature == _temperature).Count();

            double nbHumiditeAndJouerTrue = this.data.Where(d => d.JouerTennis == true && d.humidite == _humidite).Count();
            double nbHumiditeAndJouerFalse = this.data.Where(d => d.JouerTennis == false && d.humidite == _humidite).Count();

            double nbVentAndJouerTrue = this.data.Where(d => d.JouerTennis == true && d.vent == _vent).Count();
            double nbVentAndJouerFalse = this.data.Where(d => d.JouerTennis == false && d.vent == _vent).Count();


            double pJouerTrue =  nbJouerTrue / nbData;
            double pJouerFalse = nbJouerFalse / nbData;

            double pCielJouerTrue = nbCielAndJouerTrue / nbJouerTrue;
            double pCielJouerFalse = nbCielAndJouerFalse / nbJouerFalse;

            double pTempJouerTrue = nbTempAndJouerTrue / nbJouerTrue;
            double pTempJouerFalse = nbTempAndJouerFalse / nbJouerFalse;

            double pHumiditeJouerTrue = nbHumiditeAndJouerTrue / nbJouerTrue;
            double pHumiditeJouerFalse = nbHumiditeAndJouerFalse / nbJouerFalse;

            double pVentJouerTrue = nbVentAndJouerTrue / nbJouerTrue;
            double pVentJouerFalse = nbVentAndJouerFalse / nbJouerFalse;


            double pAnswerTrue = pJouerTrue * pCielJouerTrue * pTempJouerTrue * pHumiditeJouerTrue * pVentJouerTrue;
            double pAnswerFalse = pJouerFalse * pCielJouerFalse * pTempJouerFalse * pHumiditeJouerFalse * pVentJouerFalse;

            double pAnswer = Math.Max(pAnswerFalse, pAnswerTrue) / (pAnswerTrue + pAnswerFalse);

            return new Tuple<bool, double>(pAnswerTrue > pAnswerFalse, pAnswer);
            
        }
        private void addQ3Dataset()
        {
            this.data.Add(new DataPlayTennis("J1", DataPlayTennis.Ciel.ENSOLEILLE, DataPlayTennis.Temperature.CHAUDE, DataPlayTennis.Humidite.ELEVEE, DataPlayTennis.Vent.FAIBLE, false));
            this.data.Add(new DataPlayTennis("J2", DataPlayTennis.Ciel.ENSOLEILLE, DataPlayTennis.Temperature.CHAUDE, DataPlayTennis.Humidite.ELEVEE, DataPlayTennis.Vent.FORT, false));
            this.data.Add(new DataPlayTennis("J3", DataPlayTennis.Ciel.NUAGEUX, DataPlayTennis.Temperature.CHAUDE, DataPlayTennis.Humidite.ELEVEE, DataPlayTennis.Vent.FAIBLE, true));
            this.data.Add(new DataPlayTennis("J4", DataPlayTennis.Ciel.PLUVIEUX, DataPlayTennis.Temperature.TEMPEREE, DataPlayTennis.Humidite.ELEVEE, DataPlayTennis.Vent.FAIBLE, true));
            this.data.Add(new DataPlayTennis("J5", DataPlayTennis.Ciel.PLUVIEUX, DataPlayTennis.Temperature.FROIDE, DataPlayTennis.Humidite.NORMAL, DataPlayTennis.Vent.FAIBLE, true));
            this.data.Add(new DataPlayTennis("J6", DataPlayTennis.Ciel.PLUVIEUX, DataPlayTennis.Temperature.FROIDE, DataPlayTennis.Humidite.NORMAL, DataPlayTennis.Vent.FORT, false));
            this.data.Add(new DataPlayTennis("J7", DataPlayTennis.Ciel.NUAGEUX, DataPlayTennis.Temperature.FROIDE, DataPlayTennis.Humidite.NORMAL, DataPlayTennis.Vent.FORT, true));
            this.data.Add(new DataPlayTennis("J8", DataPlayTennis.Ciel.ENSOLEILLE, DataPlayTennis.Temperature.TEMPEREE, DataPlayTennis.Humidite.ELEVEE, DataPlayTennis.Vent.FAIBLE, false));
            this.data.Add(new DataPlayTennis("J9", DataPlayTennis.Ciel.ENSOLEILLE, DataPlayTennis.Temperature.FROIDE, DataPlayTennis.Humidite.NORMAL, DataPlayTennis.Vent.FAIBLE, true));
            this.data.Add(new DataPlayTennis("J10", DataPlayTennis.Ciel.PLUVIEUX, DataPlayTennis.Temperature.TEMPEREE, DataPlayTennis.Humidite.NORMAL, DataPlayTennis.Vent.FAIBLE, true));
            this.data.Add(new DataPlayTennis("J11", DataPlayTennis.Ciel.ENSOLEILLE, DataPlayTennis.Temperature.TEMPEREE, DataPlayTennis.Humidite.NORMAL, DataPlayTennis.Vent.FORT, true));
            this.data.Add(new DataPlayTennis("J12", DataPlayTennis.Ciel.NUAGEUX, DataPlayTennis.Temperature.TEMPEREE, DataPlayTennis.Humidite.ELEVEE, DataPlayTennis.Vent.FORT, true));
            this.data.Add(new DataPlayTennis("J13", DataPlayTennis.Ciel.NUAGEUX, DataPlayTennis.Temperature.CHAUDE, DataPlayTennis.Humidite.NORMAL, DataPlayTennis.Vent.FAIBLE, true));
            this.data.Add(new DataPlayTennis("J14", DataPlayTennis.Ciel.PLUVIEUX, DataPlayTennis.Temperature.TEMPEREE, DataPlayTennis.Humidite.ELEVEE, DataPlayTennis.Vent.FORT, false));
        }

        public Tuple<bool, double> SolveQ4(DataPlayTennis.Ciel _ciel, DataPlayTennis.Temperature _temperature, DataPlayTennis.Humidite _humidite, DataPlayTennis.Vent _vent)
        {
            DataPlayTennis searchedData = new DataPlayTennis("searched", _ciel, _temperature, _humidite, _vent);

            this.data.Sort(DataPlayTennis.getComparer());

            foreach (var item in this.data)
            {
                Console.WriteLine("Similarity for {0} : {1}", item.journee, DataPlayTennis.Similarity(DataPlayTennis.ComparedToData,item));
            }

            List<DataPlayTennis> nearest4Neighbour = this.data.GetRange(0, 4);

            foreach (var item in nearest4Neighbour)
            {
                Console.WriteLine("The value of JouerTennis for {0} is {1}", item.journee, item.JouerTennis);
            }

            double nbJouerTrue = nearest4Neighbour.Where(d => d.JouerTennis).Count();
            double nbJouerFalse = nearest4Neighbour.Where(d => d.JouerTennis==false).Count();

            return new Tuple<bool, double>(nbJouerTrue > nbJouerFalse, nbJouerTrue / (nbJouerFalse+nbJouerTrue));

        }

        



    }
}
