using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// JSONファイルからマップデータを読み込み、オブジェクトを配置するスクリプト
/// </summary>
public class MapLoader : MonoBehaviour
{
    [SerializeField] private Transform parentForObjects;                    // 配置オブジェクトの親（空オブジェクトなど）
    [SerializeField] private int _stageNum = DEFAULT_STAGE_NUM;             // 呼び出すステージの識別番号
 
    private const string JSON_FILE_PATH = "Json/StageData";         // Resources/Json/StageData.json を対象（ステージ番号で拡張）
    private const string PREFAB_FILE_PATH = "Prefabs/";             // prefabのオブジェクトファイルパス
    private const int DEFAULT_STAGE_NUM = 1;                        // ステージ番号のデフォルト値
    private const int EXCEPTION＿NUM = 0;                           // ステージ番号の例外の値
    private const float Z_POSITION_FIXED = 0f;

    /// <summary>
    /// 起動時にマップデータを読み込む
    /// </summary>
    private void Awake()
    {
        LoadMapFromJson();
    }

    /// <summary>
    /// JSONからマップデータを読み込み、オブジェクトを配置する処理
    /// </summary>
    public void LoadMapFromJson()
    {
        // ステージ番号が読み込み対象外ならスキップ
        if (_stageNum == EXCEPTION＿NUM)
        {
            return;
        }

        // ステージ選択マネージャーが存在すれば、現在選択中のステージ番号を取得
        if (StageSelectManager.Instance != null)
        {
            _stageNum = StageSelectManager.Instance.GetStageNumber();
        }

        // 実際に読み込むJSONファイルパスを組み立てる
        string readFilePath = JSON_FILE_PATH + _stageNum;

        // ResourcesフォルダからJSONデータを読み込む
        TextAsset jsonText = Resources.Load<TextAsset>(readFilePath);

        // 読み込めなかった場合はエラーを出してステージ選択画面に戻る
        if (jsonText == null)
        {
            Debug.LogError($"マップデータ {readFilePath}.json が Resources に見つかりませんでした。");

#if UNITY_EDITOR

            SceneManager.LoadScene("StageSelectScene");

#endif
            return;
        }

        // 読み込んだJSON文字列からマップデータのリストを生成
        List<MapObjectData> objects = MapDataSerializer.LoadFromJsonText(jsonText.text);

        // マップオブジェクトを1つずつ配置
        foreach (var objData in objects)
        {
            // Resources/Prefabs からプレハブを読み込み
            GameObject prefab = Resources.Load<GameObject>(PREFAB_FILE_PATH + objData.prefabName);

            // プレハブが見つからない場合はスキップ
            if (prefab == null)
            {
                Debug.LogWarning($"プレハブ '{objData.prefabName}' が Resources に見つかりません。スキップします。");
                continue;
            }

            // JSONで指定された位置にオブジェクトを生成（Z座標は0固定）
            Vector3 pos = new Vector3(objData.position.x, objData.position.y, Z_POSITION_FIXED);
            GameObject instance = Instantiate(prefab, pos, Quaternion.identity);

            // 指定があれば親オブジェクトの子に設定
            if (parentForObjects != null)
            {
                instance.transform.parent = parentForObjects;
            }
        }

        // 配置完了のログを表示
        Debug.Log($"マップ '{JSON_FILE_PATH}' の読み込み完了！配置数: {objects.Count}");
    }
}
