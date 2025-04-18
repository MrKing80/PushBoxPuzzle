using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class MapEditorWindow : EditorWindow
{
    private MapEditorState state;
    private MapEditorSceneHandler sceneHandler;

    [MenuItem("Tools/Map 配置エディタ")]
    public static void Open()
    {
        GetWindow<MapEditorWindow>("Map 配置エディタ");
    }

    private void OnEnable()
    {
        state = new MapEditorState();
        sceneHandler = new MapEditorSceneHandler(state);
        SceneView.duringSceneGui += sceneHandler.OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= sceneHandler.OnSceneGUI;
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("マップ配置ツール", EditorStyles.boldLabel);
        state.selectedPrefab = (GameObject)EditorGUILayout.ObjectField("プレハブ", state.selectedPrefab, typeof(GameObject), false);
        state.gridSize = EditorGUILayout.FloatField("マスサイズ", state.gridSize);
        state.snapEnabled = EditorGUILayout.Toggle("スナップON", state.snapEnabled);
        state.currentMode = (MapEditorState.EditMode)EditorGUILayout.EnumPopup("モード選択", state.currentMode);

        EditorGUILayout.Space();

        // 保存ボタン
        if (GUILayout.Button("マップをJSONで保存"))
        {
            SaveMapToJson();
        }

        if (GUILayout.Button("JSONを読み込む"))
        {
            MapLoader mapLoader = GameObject.FindGameObjectWithTag("MapLoader").GetComponent<MapLoader>();

            mapLoader.LoadMapFromJson();
        }


    }

    private void SaveMapToJson()
    {
        string path = EditorUtility.SaveFilePanel(
            "マップデータを保存",
            "Assets/Resources/Json/",
            "StageData.json",
            "json"
        );

        if (string.IsNullOrEmpty(path)) return;

        List<MapObjectData> dataList = new();

        // 対象となるオブジェクトを取得（プレハブに"MapObject"タグなどを付けると便利）
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.CompareTag("Player"))
            {
                continue;
            }

            if (PrefabUtility.GetPrefabAssetType(obj) != PrefabAssetType.NotAPrefab)
            {
                MapObjectData data = new MapObjectData
                {
                    prefabName = obj.name.Replace("(Clone)", "").Trim(), // クローン名の除去
                    position = new Vector2(obj.transform.position.x, obj.transform.position.y)
                };
                dataList.Add(data);
            }
        }

        MapDataSerializer.SaveToJson(dataList, path);
        Debug.Log($"マップを保存しました！ ({path})");
    }

}
