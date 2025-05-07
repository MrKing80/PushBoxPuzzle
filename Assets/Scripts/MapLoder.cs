using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// JSON�t�@�C������}�b�v�f�[�^��ǂݍ��݁A�I�u�W�F�N�g��z�u����X�N���v�g
/// </summary>
public class MapLoader : MonoBehaviour
{
    private string jsonFileName = "Json/StageData";         // Resources/Json/StageData.json ��Ώہi�X�e�[�W�ԍ��Ŋg���j

    [SerializeField] private Transform parentForObjects;    // �z�u�I�u�W�F�N�g�̐e�i��I�u�W�F�N�g�Ȃǁj
    [SerializeField] private int _stageNum = 1;             // �Ăяo���X�e�[�W�̎��ʔԍ��i�f�t�H���g��1�j

    /// <summary>
    /// �N�����Ƀ}�b�v�f�[�^��ǂݍ���
    /// </summary>
    private void Awake()
    {
        LoadMapFromJson();
    }

    /// <summary>
    /// JSON����}�b�v�f�[�^��ǂݍ��݁A�I�u�W�F�N�g��z�u���鏈��
    /// </summary>
    public void LoadMapFromJson()
    {
        // �X�e�[�W�ԍ���0�Ȃ珈�����X�L�b�v�i�ǂݍ��ݑΏۂȂ��j
        if (_stageNum == 0)
        {
            return;
        }

        // �X�e�[�W�I���}�l�[�W���[�����݂���΁A���ݑI�𒆂̃X�e�[�W�ԍ����擾
        if (StageSelectManager.Instance != null)
        {
            _stageNum = StageSelectManager.Instance.SetStageNumber();
        }

        // ���ۂɓǂݍ���JSON�t�@�C������g�ݗ��Ă�i��: Json/StageData1�j
        string fileName = jsonFileName + _stageNum;

        // Resources�t�H���_����JSON�f�[�^��ǂݍ���
        TextAsset jsonText = Resources.Load<TextAsset>(fileName);

        // �ǂݍ��߂Ȃ������ꍇ�̓G���[���o���ăX�e�[�W�I����ʂɖ߂�
        if (jsonText == null)
        {
            Debug.LogError($"�}�b�v�f�[�^ {fileName}.json �� Resources �Ɍ�����܂���ł����B");
            SceneManager.LoadScene("StageSelectScene");
            return;
        }

        // �ǂݍ���JSON�����񂩂�}�b�v�f�[�^�̃��X�g�𐶐�
        List<MapObjectData> objects = MapDataSerializer.LoadFromJsonText(jsonText.text);

        // �}�b�v�I�u�W�F�N�g��1���z�u
        foreach (var objData in objects)
        {
            // Resources/Prefabs ����v���n�u��ǂݍ���
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + objData.prefabName);

            // �v���n�u��������Ȃ��ꍇ�̓X�L�b�v
            if (prefab == null)
            {
                Debug.LogWarning($"�v���n�u '{objData.prefabName}' �� Resources �Ɍ�����܂���B�X�L�b�v���܂��B");
                continue;
            }

            // JSON�Ŏw�肳�ꂽ�ʒu�ɃI�u�W�F�N�g�𐶐��iZ���W��0�Œ�j
            Vector3 pos = new Vector3(objData.position.x, objData.position.y, 0f);
            GameObject instance = Instantiate(prefab, pos, Quaternion.identity);

            // �w�肪����ΐe�I�u�W�F�N�g�̎q�ɐݒ�
            if (parentForObjects != null)
            {
                instance.transform.parent = parentForObjects;
            }
        }

        // �z�u�����̃��O��\��
        Debug.Log($"�}�b�v '{jsonFileName}' �̓ǂݍ��݊����I�z�u��: {objects.Count}");
    }
}
