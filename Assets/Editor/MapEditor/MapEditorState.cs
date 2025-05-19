using UnityEngine;

/// <summary>
/// マップエディターでのステータスを管理するクラス
/// </summary>
public class MapEditorState
{
    public GameObject selectedPrefab;      // 現在選択中のプレハブ
    public float gridSize = 1f;            // グリッドの大きさ（スナップ単位）

    public enum EditMode
    {
        Place,                             // 配置モード
        Delete                             // 削除モード
    }

    public EditMode currentMode = EditMode.Place; // 現在の編集モード（初期値：配置）
}
