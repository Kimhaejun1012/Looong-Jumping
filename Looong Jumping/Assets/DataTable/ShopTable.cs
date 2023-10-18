using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

public class ShopTable : DataTable
{
    public class Data
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Require { get; set; }
        public int UpGrade { get; set; }
        
    }

    protected Dictionary<int, Data> dic = new Dictionary<int, Data>();

    public ShopTable()
    {
        path = "Tables/TestTable";
        //Load();
    }

    public override void Load()
    {
        var csvStr = Resources.Load<TextAsset>(path); // Resources에 요청할때 확장자 지워줘야함

        TextReader reader = new StringReader(csvStr.text);
        var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

        var records = csv.GetRecords<Data>();

        foreach (var record in records)
        {
            dic.Add(record.ID, record);
        }
    }

    public Data GetValue(int id)
    {
        if (!dic.ContainsKey(id))
        {
            return null;
        }
        return dic[id];
    }
}
