using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class SaveData
{
    public int Version { get; set; }

    public abstract SaveData VersionUp();
    public abstract void ClearData();
}

public class SaveDataV1 : SaveData
{
    public SaveDataV1()
    {
        Version = 1;
    }

    public PlayerData playerData { get; set; } = new PlayerData();
    public ShopData shopData { get; set; } = new ShopData();
    public GameData gameData { get; set; } = new GameData();
    public override SaveData VersionUp()
    {
        //var data = new SaveDataV2();
        //data.Gold = Gold;
        Debug.Log("VersionUp");
        return null;
    }

    public override void ClearData()
    {
        playerData = new PlayerData();
        shopData = new ShopData();
        gameData = new GameData();
        
    }
}
//public class SaveDataV2 : SaveData
//{
//    public SaveDataV2()
//    {
//        Version = 2;
//    }

//    public int Gold { get; set; }
//    public string Name { get; set; } = "Unknown";


//    public override SaveData VersionUp()
//    {
//        return null;
//    }
//}

//public class SaveDataV3 : SaveData
//{
//    public SaveDataV3()
//    {
//        Version = 3;
//    }

//    public int Gold { get; set; }
//    public string Name { get; set; }

//    //public List<CubeInfo> CubeInfo { get; set; } = new List<CubeInfo>();

//    public override SaveData VersionUp()
//    {
//        return null;
//    }
//}