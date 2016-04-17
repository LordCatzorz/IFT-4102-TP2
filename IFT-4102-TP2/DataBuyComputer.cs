using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT_4102_TP2
{
    public class DataBuyComputer
    {
        public List<Attribut> attributs;
        public Attribut classifier;
        public DataBuyComputer(string _RID, Age _age, Income _income, bool _student, CreditRating _credit_rating, bool _buys_computer = false)
        {
            this.attributs = new List<Attribut>();
            //this.attributs.Add(new RID(_RID));
            this.attributs.Add(new age(_age));
            this.attributs.Add(new income(_income));
            this.attributs.Add(new student(_student));
            this.attributs.Add(new credit_rating(_credit_rating));
            this.classifier = new buys_computer(_buys_computer);
        }

        public enum Age
        {
            LOWER_OR_EQUALS_30,
            _31_TO_40,
            OVER_40
        }

        public enum Income
        {
            HIGH, MEDIUM, LOW
        }

        public enum CreditRating
        {
            EXCELLENT, FAIR
        }

        public interface Attribut
        {
            string GetName();
            object GetValue();

            string Print();
        }

        struct RID : Attribut
        {
            private string value;

            public string GetName()
            {
                return "RID";
            }

            public object GetValue()
            {
                return value;
            }

            public string Print()
            {
                return value;
            }

            public RID(string value)
            {
                this.value = value;
            }
        }

        struct age : Attribut
        {
            private Age value;

            public string GetName()
            {
                return "age";
            }

            public object GetValue()
            {
                return value;
            }

            public age(Age value)
            {
                this.value = value;
            }
            public string Print()
            {
                switch (value)
                {
                    case Age.LOWER_OR_EQUALS_30:
                        return "<=30";
                        break;
                    case Age._31_TO_40:
                        return "31....40";
                        break;
                    case Age.OVER_40:
                        return ">40";
                        break;
                    default:
                        return "failure";
                        break;
                }
            }
        }

        struct income : Attribut
        {
            private Income value;

            public string GetName()
            {
                return "income";
            }

            public object GetValue()
            {
                return value;
            }

            public string Print()
            {
                switch (value)
                {
                    case Income.HIGH:
                        return "High";
                        break;
                    case Income.MEDIUM:
                        return "Medium";
                        break;
                    case Income.LOW:
                        return "Low";
                        break;
                    default:
                        return "Failure";
                        break;
                }
            }

            public income(Income value)
            {
                this.value = value;
            }


        }

        struct student : Attribut
        {
            private bool value;

            public string GetName()
            {
                return "student";
            }

            public object GetValue()
            {
                return value;
            }

            public string Print()
            {
                if (value)
                {
                    return "Yes";
                }
                return "No";
            }

            public student(bool value)
            {
                this.value = value;
            }
        }

        struct credit_rating : Attribut
        {
            private CreditRating value;

            public string GetName()
            {
                return "credit_rating";
            }

            public object GetValue()
            {
                return value;
            }

            public string Print()
            {
                switch (value)
                {
                    case CreditRating.EXCELLENT:
                        return "Excellent";
                        break;
                    case CreditRating.FAIR:
                        return "Fair";
                        break;
                    default:
                        return "Failure";
                        break;
                }
            }

            public credit_rating(CreditRating value)
            {
                this.value = value;
            }
        }

        struct buys_computer : Attribut
        {
            private bool value;

            public string GetName()
            {
                return "buys_computer";
            }

            public object GetValue()
            {
                return value;
            }

            public string Print()
            {
                if (value)
                {
                    return "Yes";
                }
                return "No";

            }

            public buys_computer(bool value)
            {
                this.value = value;
            }
        }
    }
}
