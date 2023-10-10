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
            Debug.Log("Load 들어오긴 했는데 null 반환");
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

//싱글톤으로 유니티에서 사용할떄는 컴퍼넌트를 싱글톤으로
//그 외의 방법은 스태틱 멤버로 사용하는 방법
//게임오브젝트가 필요하고 업데이트도 돌아야되면 컴퍼넌트를 싱글톤하고
//게임오브젝트 없이 단독으로 돌아도 돼 세이브로드나 그런놈들은 스태틱 버전으로 만들고

