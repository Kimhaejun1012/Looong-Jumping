using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using Unity.VisualScripting;
using System.Data;


public static class DataTableManager
{
    private static Dictionary<System.Type, DataTable> tables = new Dictionary<System.Type, DataTable>();

    static DataTableManager()
    {
        tables.Clear();

        var stringTable = new StringTable();
        var shopTable = new ShopTable();

        stringTable.Load();
        shopTable.Load();
        tables.Add(typeof(StringTable), stringTable);
        tables.Add(typeof(ShopTable), shopTable);
    }

    public static T GetTable<T>() where T : DataTable
    {
        var id = typeof(T);
        if(!tables.ContainsKey(id))
        {
            return null;
        }
        return tables[id] as T;
    }


    
    //public T Get<T>(DataTable.Ids id)
    //{
    //    var find = tables.TryGetValue(id, out DataTable dataTable);
    //    if (find == tables.end())
    //        return default;
    //    return <T>(find->second);

    //    return default;
    //}
}
