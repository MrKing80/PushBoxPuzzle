using UnityEngine;

public class MapEditorState
{
    public GameObject selectedPrefab;
    public float gridSize = 1f;
    public bool snapEnabled = true;

    public enum EditMode 
    {
        Place, 
        Delete
    }
    public EditMode currentMode = EditMode.Place;
}
