using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;

public abstract class DataTable
{
    protected string path = string.Empty;

    public abstract void Load();

    //private static Dictionary<DataTableIds>

    //public enum Ids
    //{
    //    None = -1,
    //    Test
    //}

    //private void Awake()
    //{
    //    if (this != null)
    //    {

    //    }
    //}

    //public Ids tableId = Ids.None;

    //public DataTable(Ids id)
    //{
    //    tableId = id;
    //}

    //public virtual bool Load()
    //{
    //    try
    //    {
    //        // CSV 파일에서 데이터 읽기
    //        using (var reader = new StreamReader("your_csv_file.csv"))
    //        using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
    //        {
    //            var data = csv.GetRecords<TestTable>().ToList();
    //        }

    //        return true;
    //    }
    //    catch (System.Exception e)
    //    {
    //        Debug.LogError("Error loading data from CSV: " + e.Message);
    //        return false;
    //    }
    //}

    //public virtual void Release()
    //{
    //    // 구현해야 할 내용
    //}
}
