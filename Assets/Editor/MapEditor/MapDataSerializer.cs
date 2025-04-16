using UnityEngine;
using System.Collections.Generic;


public static class MapDataSerializer
{
    public static void SaveToJson(List<MapObjectData> dataList, string path)
    {
        string json = JsonUtility.ToJson(new Wrapper { list = dataList }, true);
        System.IO.File.WriteAllText(path, json);
    }

    public static List<MapObjectData> LoadFromJson(string path)
    {
        if (!System.IO.File.Exists(path)) return new List<MapObjectData>();
        string json = System.IO.File.ReadAllText(path);
        return JsonUtility.FromJson<Wrapper>(json).list;
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<MapObjectData> list;
    }
}
