using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

// マップエディターのシーン内操作を処理するクラス
public class MapEditorSceneHandler
{
    private MapEditorState state;

    // コンストラクタ：エディタの状態を受け取って保持
    public MapEditorSceneHandler(MapEditorState editorState)
    {
        state = editorState;
    }

    // SceneビューでのGUIイベントを処理
    public void OnSceneGUI(SceneView sceneView)
    {
        Event guiEvent = Event.current;

        // 左クリックかつプレハブが選択されている場合、処理を行う
        if (!(guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && state.selectedPrefab != null))
        {
            return;
        }

        // マウス位置からレイを生成
        Ray ray = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);

        // レイキャストで地面などにヒットしていない場合、処理を中断する
        if (!(Physics.Raycast(ray, out RaycastHit hit)))
        {
            return;
        }


        Vector3 pos = hit.point;

        // スナップ設定が有効ならグリッドに合わせて位置調整
        if (state.snapEnabled)
        {
            pos.x = Mathf.Round(pos.x / state.gridSize) * state.gridSize;
            pos.y = Mathf.Round(pos.y / state.gridSize) * state.gridSize;
            pos.z = 0f; // Z座標は固定
        }

        // 配置モードか削除モードで処理を分岐
        if (state.currentMode == MapEditorState.EditMode.Place)
        {
            PlaceAt(pos); // プレハブを配置
        }
        else
        {
            DeleteAt(pos); // オブジェクトを削除
        }

        guiEvent.Use(); // イベントを消費

        // カーソル下にプレビュー表示などの拡張したい
    }

    // 指定位置にプレハブを配置する処理
    private void PlaceAt(Vector3 position)
    {
        // すでに同じ位置にオブジェクトがある場合は配置しない
        var existing = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (var obj in existing)
        {
            if (Vector3.Distance(obj.transform.position, position) < 0.1f)
                return;
        }

        // プレハブをインスタンス化して配置
        GameObject placed = (GameObject)PrefabUtility.InstantiatePrefab(state.selectedPrefab);
        placed.transform.position = position;

        // Undo機能に対応させる
        Undo.RegisterCreatedObjectUndo(placed, "Place Prefab");

        // シーンを変更済みにマーク
        EditorSceneManager.MarkSceneDirty(placed.scene);
    }

    // 指定位置のオブジェクトを削除する処理
    private void DeleteAt(Vector3 position)
    {
        foreach (var obj in GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            // 対象位置のオブジェクトを発見したら削除
            if (Vector3.Distance(obj.transform.position, position) < 0.1f)
            {
                Undo.DestroyObjectImmediate(obj); // Undoに対応した削除
                break;
            }
        }
    }
}
