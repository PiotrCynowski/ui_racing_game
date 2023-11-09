using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;

public class Advertisement : MonoBehaviour
{
    public Text advertisementText;

    void Start()
    {
        string url = "http://api.nbp.pl/api/exchangerates/rates/c/usd/2016-04-04/?format=json";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream stream = response.GetResponseStream();
        StreamReader reader = new StreamReader(stream);
        string json = reader.ReadToEnd();
        response.Close();

        var Data = JsonConvert.DeserializeObject<ExchangeRate>(json);

        string kantorName = "Kantor \"Tanie siano\"";
        string usdExchangeRate = Data.rates[0].ask.ToString("0.00");
        string advertisement = "—---------------------------\n" +
                               "reklama\n" +
                               "—----------------------------\n" +
                               kantorName + "\n" +
                               "U nas USD po " + usdExchangeRate + "\n" +                         
                               "—----------------------------";

        advertisementText.text = advertisement;
    }

    public class ExchangeRate
    {
        public string table;
        public string currency;
        public string code;
        public List<Rate> rates;
    }

    public class Rate
    {
        public string no;
        public string effectiveDate;
        public double bid;
        public double ask;
    }
}