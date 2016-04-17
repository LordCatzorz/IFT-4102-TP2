namespace IFT_4102_TP2
{
    interface IData
    {
        DataPlayTennis.Ciel ciel { get; set; }
        DataPlayTennis.Humidite humidite { get; set; }
        string journee { get; set; }
        DataPlayTennis.Temperature temperature { get; set; }
        DataPlayTennis.Vent vent { get; set; }

        double similarity(DataPlayTennis data1, DataPlayTennis data2);
    }
}