using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT_4102_TP2
{
    class TreeLearner
    {

        List<DataBuyComputer> data;
        public TreeLearner()
        {
            data = new List<DataBuyComputer>();
        }

        public void SolveQ5()
        {
            addQ5DataSet();
            TreeSolver(data, String.Empty);
        }

        void TreeSolver(List<DataBuyComputer> _tree, string _parentAttribute)
        {
            List<bool> listClassifier = _tree.GroupBy(x => (bool)(x.classifier.GetValue())).Select(x =>(bool)(x.First().classifier.GetValue())).ToList();
            if (listClassifier.Count() == 1)
            {
                object parentAttibuteValue = _tree.First().attributs.Where(a => a.GetName() == _parentAttribute).First().Print();
                Console.WriteLine("Leaf with result {0} and parent {1} equals to {2}", listClassifier.First(), _parentAttribute,parentAttibuteValue);
                return;
            }
           
            List<string> attributeName = _tree.First().attributs.GroupBy(a => a.GetName()).Select(a => a.First().GetName()).ToList();

            List<Tuple<string, double>> valueGain = new List<Tuple<string, double>>();
            foreach (string name in attributeName)
            {
                double gain = calculateEntropieGain(_tree, name);
                valueGain.Add(new Tuple<string, double>(name, gain));
            }
            double max = valueGain.Max(d => d.Item2);
            string nameRoot = valueGain.Where(d => d.Item2 == max).First().Item1;
            if (_parentAttribute != String.Empty)
            {
                object parentAttibuteValue = _tree.First().attributs.Where(a => a.GetName() == _parentAttribute).First().Print();
                Console.WriteLine("Node is {0} with parent {1} equals to {2}", nameRoot, _parentAttribute, parentAttibuteValue);
            }
            else
            {
                Console.WriteLine("Node is {0} with parent {1}", nameRoot, _parentAttribute);
            }
            List<List<DataBuyComputer>> listOfTree = new List<List<DataBuyComputer>>();
            for (int i = 0; i < _tree.Count; i++)
            {
                object value = _tree[i].attributs.Where(a => a.GetName() == nameRoot).Select(a => a.GetValue()).First();

                bool foundSubTree = false;
                foreach (List<DataBuyComputer> subTree in listOfTree)
                {
                    object subTreeValue = subTree.First().attributs.Where(a => a.GetName() == nameRoot).Select(a => a.GetValue()).First();
                    if (value.Equals(subTreeValue))
                    {
                        foundSubTree = true;
                        subTree.Add(_tree[i]);
                    }
                }

                if (!foundSubTree)
                {
                    List<DataBuyComputer> newList = new List<DataBuyComputer>();
                    newList.Add(_tree[i]);
                    listOfTree.Add(newList);
                }
            }

            foreach (var subTree in listOfTree)
            {
                TreeSolver(subTree, nameRoot);
            }
        }


        double calculateEntropieValue(List<DataBuyComputer> _data, string _attributeName, object _valueName, double _nbData)
        {
            double nbVal = _data.Where(d => d.attributs.Where(a => a.GetName() == _attributeName).Single().GetValue().Equals(_valueName)).Count();
            double probability = nbVal / _nbData;
            return (-probability) * Math.Log(probability, 2);
        }


        double calculateEntropie(string _attributeName)
        {
            double entropie = 0;
            double nbData = data.Count;
            if (data.Where(d => d.classifier.GetName() == _attributeName).Count() > 0)
            {
                //Classifier
                List<object> knownValue = new List<object>();
                for (int i = 0; i < data.Count; i++)
                {
                    object value = data[i].classifier.GetValue();
                    if (!knownValue.Contains(value))
                    {
                        knownValue.Add(value);
                    }
                }
                foreach (object itemVal in knownValue)
                {
                    object val = data[0].classifier.GetValue();
                    double nbVal = data.Where(d => d.classifier.GetValue().Equals(itemVal)).Count();
                    double probability = nbVal / nbData;
                    entropie += (-probability) * Math.Log(probability, 2);
                }
            }
            else
            {
                //Attributes

                List<object> knownValue = new List<object>();
                for (int i = 0; i < data.Count; i++)
                {
                    object value = data[i].attributs.Where(a => a.GetName() == _attributeName).Single().GetValue();
                    if (!knownValue.Contains(value))
                    {
                        knownValue.Add(value);
                    }
                }
                foreach (var itemVal in knownValue)
                {
                    entropie += calculateEntropieValue(data, _attributeName, itemVal, nbData);
                }
            }
            return entropie;
        }

        double calculateEntropieGain(List<DataBuyComputer> S, string _attributeName)
        {
            List<object> knownValue = new List<object>();
            for (int i = 0; i < S.Count(); i++)
            {
                object value = S[i].attributs.Where(a => a.GetName() == _attributeName).Single().GetValue();
                if (!knownValue.Contains(value))
                {
                    knownValue.Add(value);
                }
            }
            List<Tuple<object, double, double>> dataForEntropie = new List<Tuple<object, double, double>>();
            foreach (object attibuteValue in knownValue)
            {
                IEnumerable<DataBuyComputer> subData = S.Where(d => d.attributs.Exists(a => a.GetName() == _attributeName && a.GetValue().Equals(attibuteValue)));
                double nbData = subData.Count();
                double nbDataTrue = subData.Where(d => d.classifier.GetValue().Equals(true)).Count();
                double nbDataFalse = subData.Where(d => d.classifier.GetValue().Equals(false)).Count();
                double probabilityTrue = (nbDataTrue / nbData);
                double probabilityFalse = (nbDataFalse / nbData);

                double entropieTrue = probabilityTrue == 0 ? 0 : (-probabilityTrue) * Math.Log(probabilityTrue, 2);
                double entropieFalse = probabilityFalse == 0 ? 0 : (-probabilityFalse) * Math.Log(probabilityFalse, 2);
                double entropie = entropieTrue + entropieFalse;
                dataForEntropie.Add(new Tuple<object, double, double>(attibuteValue, entropie, nbData));
            }

            double sumGain = 0;
            foreach (var item in dataForEntropie)
            {
                sumGain += (item.Item3 / (double)S.Count()) * item.Item2;
            }
            return calculateEntropie("buys_computer") - sumGain;

        }
        void addQ5DataSet()
        {
            data.Add(new DataBuyComputer("1", DataBuyComputer.Age.LOWER_OR_EQUALS_30, DataBuyComputer.Income.HIGH, false, DataBuyComputer.CreditRating.FAIR, false));
            data.Add(new DataBuyComputer("2", DataBuyComputer.Age.LOWER_OR_EQUALS_30, DataBuyComputer.Income.HIGH, false, DataBuyComputer.CreditRating.EXCELLENT, false));
            data.Add(new DataBuyComputer("3", DataBuyComputer.Age._31_TO_40, DataBuyComputer.Income.HIGH, false, DataBuyComputer.CreditRating.FAIR, true));
            data.Add(new DataBuyComputer("4", DataBuyComputer.Age.OVER_40, DataBuyComputer.Income.MEDIUM, false, DataBuyComputer.CreditRating.FAIR, true));
            data.Add(new DataBuyComputer("5", DataBuyComputer.Age.OVER_40, DataBuyComputer.Income.LOW, true, DataBuyComputer.CreditRating.FAIR, true));
            data.Add(new DataBuyComputer("6", DataBuyComputer.Age.OVER_40, DataBuyComputer.Income.LOW, true, DataBuyComputer.CreditRating.EXCELLENT, false));
            data.Add(new DataBuyComputer("7", DataBuyComputer.Age._31_TO_40, DataBuyComputer.Income.LOW, true, DataBuyComputer.CreditRating.EXCELLENT, true));
            data.Add(new DataBuyComputer("8", DataBuyComputer.Age.LOWER_OR_EQUALS_30, DataBuyComputer.Income.MEDIUM, false, DataBuyComputer.CreditRating.FAIR, false));
            data.Add(new DataBuyComputer("9", DataBuyComputer.Age.LOWER_OR_EQUALS_30, DataBuyComputer.Income.LOW, true, DataBuyComputer.CreditRating.FAIR, true));
            data.Add(new DataBuyComputer("10", DataBuyComputer.Age.OVER_40, DataBuyComputer.Income.MEDIUM, true, DataBuyComputer.CreditRating.FAIR, true));
            data.Add(new DataBuyComputer("11", DataBuyComputer.Age.LOWER_OR_EQUALS_30, DataBuyComputer.Income.MEDIUM, true, DataBuyComputer.CreditRating.EXCELLENT, true));
            data.Add(new DataBuyComputer("12", DataBuyComputer.Age._31_TO_40, DataBuyComputer.Income.MEDIUM, false, DataBuyComputer.CreditRating.EXCELLENT, true));
            data.Add(new DataBuyComputer("13", DataBuyComputer.Age._31_TO_40, DataBuyComputer.Income.HIGH, true, DataBuyComputer.CreditRating.FAIR, true));
            data.Add(new DataBuyComputer("14", DataBuyComputer.Age.OVER_40, DataBuyComputer.Income.MEDIUM, false, DataBuyComputer.CreditRating.EXCELLENT, false));
        }

    }



}
