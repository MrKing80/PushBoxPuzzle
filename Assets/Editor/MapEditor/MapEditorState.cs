using UnityEngine;

/// <summary>
/// �}�b�v�G�f�B�^�[�ł̃X�e�[�^�X���Ǘ�����N���X
/// </summary>
public class MapEditorState
{
    public GameObject selectedPrefab;      // ���ݑI�𒆂̃v���n�u
    public float gridSize = 1f;            // �O���b�h�̑傫���i�X�i�b�v�P�ʁj

    public enum EditMode
    {
        Place,                             // �z�u���[�h
        Delete                             // �폜���[�h
    }

    public EditMode currentMode = EditMode.Place; // ���݂̕ҏW���[�h�i�����l�F�z�u�j
}
