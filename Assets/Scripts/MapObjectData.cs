using UnityEngine;

/// <summary>
/// マップ上に配置されるオブジェクトのデータ構造（シリアライズ可能）
/// </summary>
[System.Serializable]
public class MapObjectData
{
    // 使用するプレハブ名（Resources/Prefabs にある名前と一致させる）
    public string prefabName = "";

    // オブジェクトの2D座標（x, y）
    public Vector2 position = Vector2.zero;
}
