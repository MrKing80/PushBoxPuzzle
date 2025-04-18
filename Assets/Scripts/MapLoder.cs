using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private string jsonFileName = "Json/StageData"; // Resources/StageData.json を対象
    [SerializeField] private Transform parentForObjects; // 配置オブジェクトの親（空オブジェクトでもOK）
    [SerializeField] private int _stageNum = 1;
    private void Awake()
    {
        LoadMapFromJson();
    }

    public void LoadMapFromJson()
    {
        if (StageSelectManager.Instance != null)
        {
            _stageNum = StageSelectManager.Instance.SetStageNumber();
        }

        string fileName = jsonFileName + _stageNum;

        TextAsset jsonText = Resources.Load<TextAsset>(fileName);

        if (jsonText == null)
        {
            Debug.LogError($"マップデータ {fileName}.json が Resources に見つかりませんでした。");
            SceneManager.LoadScene("StageSelectScene");
            return;
        }

        List<MapObjectData> objects = MapDataSerializer.LoadFromJsonText(jsonText.text);

        foreach (var objData in objects)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + objData.prefabName);
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
