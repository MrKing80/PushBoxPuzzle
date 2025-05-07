using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// マップデータ（MapObjectData）の保存・読み込みを行う静的クラス
/// </summary>
public static class MapDataSerializer
{
    /// <summary>
    /// マップデータをJSON形式で保存する
    /// </summary>
    /// <param name="dataList">保存するマップデータのリスト</param>
    /// <param name="path">保存先のファイルパス</param>
    public static void SaveToJson(List<MapObjectData> dataList, string path)
    {
        // ラッパークラスで包んだデータをJSONに変換（整形あり）
        string json = JsonUtility.ToJson(new Wrapper { list = dataList }, true);
        
        // 指定されたパスにJSON文字列を書き出す
        System.IO.File.WriteAllText(path, json);
    }

    /// <summary>
    /// JSONファイルからマップデータを読み込む
    /// </summary>
    /// <param name="path">読み込むファイルのパス</param>
    /// <returns>読み込まれたマップデータのリスト</returns>
    public static List<MapObjectData> LoadFromJson(string path)
    {
        // ファイルが存在しない場合は空のリストを返す
        if (!System.IO.File.Exists(path)) return new List<MapObjectData>();
        
        // ファイルの中身を読み取り
        string json = System.IO.File.ReadAllText(path);
        
        // JSONをデシリアライズしてデータリストを取得
        return JsonUtility.FromJson<Wrapper>(json).list;
    }

    /// <summary>
    /// JSON文字列からマップデータを読み込む（ファイルではなく文字列を直接使用）
    /// </summary>
    /// <param name="json">JSON形式の文字列</param>
    /// <returns>デシリアライズされたマップデータのリスト</returns>
    public static List<MapObjectData> LoadFromJsonText(string json)
    {
        // JSONをデシリアライズしてデータリストを取得
        return JsonUtility.FromJson<Wrapper>(json).list;
    }

    /// <summary>
    /// Listを包むラッパークラス（JsonUtilityでは直接Listを扱えないため使用）
    /// </summary>
    [System.Serializable]
    private class Wrapper
    {
        public List<MapObjectData> list;  // マップデータのリスト
    }
}
