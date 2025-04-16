using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private string jsonFileName = "MapData"; // Resources/MapData.json を対象
    [SerializeField] private Transform parentForObjects; // 配置オブジェクトの親（空オブジェクトでもOK）

    private void Start()
    {
        LoadMapFromJson();
    }

    public void LoadMapFromJson()
    {
        TextAsset jsonText = Resources.Load<TextAsset>(jsonFileName);

        if (jsonText == null)
        {
            Debug.LogError($"マップデータ {jsonFileName}.json が Resources に見つかりませんでした。");
            return;
        }

        List<MapObjectData> objects = MapDataSerializer.LoadFromJson(jsonText.text);

        foreach (var objData in objects)
        {
            GameObject prefab = Resources.Load<GameObject>(objData.prefabName);
            if (prefab == null)
            {
                Debug.LogWarning($"プレハブ '{objData.prefabName}' が Resources に見つかりません。スキップします。");
                continue;
            }

            Vector3 pos = new Vector3(objData.position.x, objData.position.y, 0f);
            GameObject instance = Instantiate(prefab, pos, Quaternion.identity);
            if (parentForObjects != null)
            {
                instance.transform.parent = parentForObjects;
            }
        }

        Debug.Log($"マップ '{jsonFileName}' の読み込み完了！配置数: {objects.Count}");
    }
}
