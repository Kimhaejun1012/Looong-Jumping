using Palmmedia.ReportGenerator.Core.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance; // �̱�������

    public PlayerInfo nowPlayer = new PlayerInfo(); // �÷��̾� ������ ����
    public ShopData shopData = new ShopData();

    public string path; // ���

    private void Awake()
    {
        #region �̱���
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/save";	// ��� ����
        print(path);
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path, data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path);
        nowPlayer = JsonUtility.FromJson<PlayerInfo>(data);
    }

    public void DataClear()
    {
        nowPlayer = new PlayerInfo();
    }
}
