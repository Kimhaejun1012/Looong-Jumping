using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

public class StringTable : DataTable
{
    public class Data
    {
        public string ID { get; set; }
        public string STRING { get; set; }
    }

    protected Dictionary<string, string> dic = new Dictionary<string, string>();

    public StringTable()
    {
        path = "Tables/StringTable";
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
            dic.Add(record.ID, record.STRING);
        }
    }

    public string GetValue(string id)
    {
        if(!dic.ContainsKey(id))
        {
            return string.Empty;
        }
        return dic[id];
    }
}
