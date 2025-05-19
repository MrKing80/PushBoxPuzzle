using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

// �}�b�v�G�f�B�^�[�̃V�[�����������������N���X
public class MapEditorSceneHandler
{
    private MapEditorState state;

    // �R���X�g���N�^�F�G�f�B�^�̏�Ԃ��󂯎���ĕێ�
    public MapEditorSceneHandler(MapEditorState editorState)
    {
        state = editorState;
    }

    // Scene�r���[�ł�GUI�C�x���g������
    public void OnSceneGUI(SceneView sceneView)
    {
        Event guiEvent = Event.current;

        // ���N���b�N���v���n�u���I������Ă���ꍇ�A�������s��
        if (!(guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && state.selectedPrefab != null))
        {
            return;
        }

        // �}�E�X�ʒu���烌�C�𐶐�
        Ray ray = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);

        // ���C�L���X�g�Œn�ʂȂǂɃq�b�g���Ă��Ȃ��ꍇ�A�����𒆒f����
        if (!(Physics.Raycast(ray, out RaycastHit hit)))
        {
            return;
        }


        Vector3 pos = hit.point;

        // �X�i�b�v�ݒ肪�L���Ȃ�O���b�h�ɍ��킹�Ĉʒu����
        if (state.snapEnabled)
        {
            pos.x = Mathf.Round(pos.x / state.gridSize) * state.gridSize;
            pos.y = Mathf.Round(pos.y / state.gridSize) * state.gridSize;
            pos.z = 0f; // Z���W�͌Œ�
        }

        // �z�u���[�h���폜���[�h�ŏ����𕪊�
        if (state.currentMode == MapEditorState.EditMode.Place)
        {
            PlaceAt(pos); // �v���n�u��z�u
        }
        else
        {
            DeleteAt(pos); // �I�u�W�F�N�g���폜
        }

        guiEvent.Use(); // �C�x���g������

        // �J�[�\�����Ƀv���r���[�\���Ȃǂ̊g��������
    }

    // �w��ʒu�Ƀv���n�u��z�u���鏈��
    private void PlaceAt(Vector3 position)
    {
        // ���łɓ����ʒu�ɃI�u�W�F�N�g������ꍇ�͔z�u���Ȃ�
        var existing = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (var obj in existing)
        {
            if (Vector3.Distance(obj.transform.position, position) < 0.1f)
                return;
        }

        // �v���n�u���C���X�^���X�����Ĕz�u
        GameObject placed = (GameObject)PrefabUtility.InstantiatePrefab(state.selectedPrefab);
        placed.transform.position = position;

        // Undo�@�\�ɑΉ�������
        Undo.RegisterCreatedObjectUndo(placed, "Place Prefab");

        // �V�[����ύX�ς݂Ƀ}�[�N
        EditorSceneManager.MarkSceneDirty(placed.scene);
    }

    // �w��ʒu�̃I�u�W�F�N�g���폜���鏈��
    private void DeleteAt(Vector3 position)
    {
        foreach (var obj in GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            // �Ώۈʒu�̃I�u�W�F�N�g�𔭌�������폜
            if (Vector3.Distance(obj.transform.position, position) < 0.1f)
            {
                Undo.DestroyObjectImmediate(obj); // Undo�ɑΉ������폜
                break;
            }
        }
    }
}
