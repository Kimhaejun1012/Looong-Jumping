using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SaveDataVC = SaveDataV1;

public static class SaveLoadSystem
{
    public enum Mods
    {
        Json,
        Binary,
        EncryptedBinary,
    };
    public static Mods FileMode { get; } = Mods.Json;
    public static int SaveDataVersion { get; } = 1;
    private static string[] SaveSlotFileNames =
    {
        "Save0.json",
        "Save1.json",
        "Save2.json"
    };
    public static string AutoSaveFileName { get; } = "AutoSave";

    public static string SaveDirectory
    {
        get
        {
            return $"{Application.persistentDataPath}/Save";
        }
    }

    public static void AutoSave(SaveData data)
    {
        Save(data, AutoSaveFileName);
    }

    public static SaveData AutoLoad()
    {
        return Load(AutoSaveFileName);
    }

    public static void Save(SaveData data, int slot)
    {
        Save(data, SaveSlotFileNames[slot]);
    }

    public static SaveData Load(int slot)
    {
        return Load(SaveSlotFileNames[slot]);
    }

    public static void Save(SaveData data, string filename)
    {
        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }

        var path = Path.Combine(SaveDirectory, filename);
        using (var writer = new JsonTextWriter(new StreamWriter(path)))
        {
            var serialize = new JsonSerializer();
            //serialize.Converters.Add(new Vector3Converter());
            //serialize.Converters.Add(new QuaternionConverter());
            serialize.Serialize(writer, data);
        }
    }

    public static SaveData Load(string filename)
    {
        var path = Path.Combine(SaveDirectory, filename);
        if (!File.Exists(path))
        {
            Debug.Log("Load ������ �ߴµ� null ��ȯ");
            return null;
        }

        SaveData data = null;

        int version = 0;
        var json = File.ReadAllText(path);

        using (var reader = new JsonTextReader(new StringReader(json)))
        {
            var jObj = JObject.Load(reader);
            version = jObj["Version"].Value<int>();
        }

        using (var reader = new JsonTextReader(new StringReader(json)))
        {
            var serialize = new JsonSerializer();
            switch (version)
            {
                case 1:
                    data = serialize.Deserialize<SaveDataV1>(reader);
                    break;
                //case 2:
                //    data = serialize.Deserialize<SaveDataV2>(reader);
                //    break;
                //case 3:
                //    serialize.Converters.Add(new Vector3Converter());
                //    serialize.Converters.Add(new QuaternionConverter());
                //    data = serialize.Deserialize<SaveDataV3>(reader);
                //    break;
            }
        }

        while (data.Version < SaveDataVersion)
        {
            data = data.VersionUp();
        }
        //Debug.Log(data);
        return data;
    }
}

//�̱������� ����Ƽ���� ����ҋ��� ���۳�Ʈ�� �̱�������
//�� ���� ����� ����ƽ ����� ����ϴ� ���
//���ӿ�����Ʈ�� �ʿ��ϰ� ������Ʈ�� ���ƾߵǸ� ���۳�Ʈ�� �̱����ϰ�
//���ӿ�����Ʈ ���� �ܵ����� ���Ƶ� �� ���̺�ε峪 �׷������ ����ƽ �������� �����

