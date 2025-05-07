using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

// マップエディター用のカスタムウィンドウクラス（Unityエディタ拡張）
public class MapEditorWindow : EditorWindow
{
    private MapEditorState state;               // マップエディタの状態を保持するオブジェクト
    private MapEditorSceneHandler sceneHandler; // Sceneビューでの処理を担当するハンドラー

    // メニューに「Map 配置エディタ」を追加
    [MenuItem("Tools/Map 配置エディタ")]
    public static void Open()
    {
        // ウィンドウを開く（またはフォーカスする）
        GetWindow<MapEditorWindow>("Map 配置エディタ");
    }

    // ウィンドウが有効化されたときに呼び出される
    private void OnEnable()
    {
        state = new MapEditorState(); // エディタ状態の初期化
        sceneHandler = new MapEditorSceneHandler(state); // Sceneビュー操作用のハンドラーを作成
        SceneView.duringSceneGui += sceneHandler.OnSceneGUI; // SceneGUIイベントに登録
    }

    // ウィンドウが無効化されたときに呼び出される
    private void OnDisable()
    {
        SceneView.duringSceneGui -= sceneHandler.OnSceneGUI; // SceneGUIイベントから登録解除
    }

    // エディタウィンドウのGUIを描画する
    private void OnGUI()
    {
        EditorGUILayout.LabelField("マップ配置ツール", EditorStyles.boldLabel);

        // プレハブの選択フィールド
        state.selectedPrefab = (GameObject)EditorGUILayout.ObjectField("プレハブ", state.selectedPrefab, typeof(GameObject), false);

        // グリッドサイズの指定
        state.gridSize = EditorGUILayout.FloatField("マスサイズ", state.gridSize);

        // スナップの有効/無効切り替え
        state.snapEnabled = EditorGUILayout.Toggle("スナップON", state.snapEnabled);

        // 配置モードと削除モードの切り替え
        state.currentMode = (MapEditorState.EditMode)EditorGUILayout.EnumPopup("モード選択", state.currentMode);

        EditorGUILayout.Space();

        // マップデータをJSONで保存するボタン
        if (GUILayout.Button("マップをJSONで保存"))
        {
            SaveMapToJson();
        }

        // JSONからマップデータを読み込むボタン
        if (GUILayout.Button("JSONを読み込む"))
        {
            // "MapLoader"タグがついたオブジェクトからMapLoaderコンポーネントを取得
            MapLoader mapLoader = GameObject.FindGameObjectWithTag("MapLoader").GetComponent<MapLoader>();
            mapLoader.LoadMapFromJson(); // マップデータの読み込み
        }

        // 現在のマップ上の全オブジェクトを削除するボタン
        if (GUILayout.Button("すべてのオブジェクトを削除"))
        {
            DeleteStageObjects();
        }
    }

    // 現在のマップオブジェクトをJSON形式で保存
    private void SaveMapToJson()
    {
        // ファイル保存ダイアログを表示
        string path = EditorUtility.SaveFilePanel(
            "マップデータを保存",
            "Assets/Resources/Json/",
            "StageData.json",
            "json"
        );

        // キャンセルされた場合は中止
        if (string.IsNullOrEmpty(path)) return;

        List<MapObjectData> dataList = new();

        // シーン内のすべてのGameObjectを走査
        foreach (GameObject obj in GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            // プレイヤーオブジェクトは除外
            if (obj.CompareTag("Player"))
            {
                continue;
            }

            // プレハブインスタンスのみ対象にする
            if (PrefabUtility.GetPrefabAssetType(obj) != PrefabAssetType.NotAPrefab)
            {
                MapObjectData data = new MapObjectData
                {
                    // "(Clone)"を取り除いてプレハブ名を記録
                    prefabName = obj.name.Replace("(Clone)", "").Trim(),
                    // 位置情報を記録（Z軸は使用しない）
                    position = new Vector2(obj.transform.position.x, obj.transform.position.y)
                };
                dataList.Add(data);
            }
        }

        // JSONファイルに保存
        MapDataSerializer.SaveToJson(dataList, path);
        Debug.Log($"マップを保存しました！ ({path})");
    }

    // シーン内のマップオブジェクトをすべて削除
    private void DeleteStageObjects()
    {
        // シーン内のすべてのGameObjectを走査
        foreach (GameObject obj in GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            // プレイヤーオブジェクトは除外
            if (obj.CompareTag("Player"))
            {
                continue;
            }

            // プレハブまたは"(Clone)"を含む名前のオブジェクトを対象に削除
            if (PrefabUtility.GetPrefabAssetType(obj) != PrefabAssetType.NotAPrefab || obj.name.Contains("Clone"))
            {
                Undo.DestroyObjectImmediate(obj); // Undo対応で削除
            }
        }
    }
}
