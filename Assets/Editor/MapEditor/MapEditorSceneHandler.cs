using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class MapEditorSceneHandler
{
    private MapEditorState state;

    public MapEditorSceneHandler(MapEditorState editorState)
    {
        state = editorState;
    }

    public void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 0 && state.selectedPrefab != null)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 pos = hit.point;
                if (state.snapEnabled)
                {
                    pos.x = Mathf.Round(pos.x / state.gridSize) * state.gridSize;
                    pos.y = Mathf.Round(pos.y / state.gridSize) * state.gridSize;
                    pos.z = 0f;
                }

                if (state.currentMode == MapEditorState.EditMode.Place)
                    PlaceAt(pos);
                else
                    EraseAt(pos);

                e.Use();
            }
        }

        // カーソル下にプレビュー表示など追加可能
    }

    private void PlaceAt(Vector3 position)
    {
        var existing = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (var obj in existing)
        {
            if (Vector3.Distance(obj.transform.position, position) < 0.1f)
                return;
        }

        GameObject placed = (GameObject)PrefabUtility.InstantiatePrefab(state.selectedPrefab);
        placed.transform.position = position;

        Undo.RegisterCreatedObjectUndo(placed, "Place Prefab");
        EditorSceneManager.MarkSceneDirty(placed.scene);
    }

    private void EraseAt(Vector3 position)
    {
        foreach (var obj in GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            if (Vector3.Distance(obj.transform.position, position) < 0.1f)
            {
                Undo.DestroyObjectImmediate(obj);
                break;
            }
        }
    }
}
