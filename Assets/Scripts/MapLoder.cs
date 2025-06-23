using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// JSON�t�@�C������}�b�v�f�[�^��ǂݍ��݁A�I�u�W�F�N�g��z�u����X�N���v�g
/// </summary>
public class MapLoader : MonoBehaviour
{
    [SerializeField] private Transform parentForObjects;                    // �z�u�I�u�W�F�N�g�̐e�i��I�u�W�F�N�g�Ȃǁj
    [SerializeField] private int _stageNum = DEFAULT_STAGE_NUM;             // �Ăяo���X�e�[�W�̎��ʔԍ�
 
    private const string JSON_FILE_PATH = "Json/StageData";         // Resources/Json/StageData.json ��Ώہi�X�e�[�W�ԍ��Ŋg���j
    private const string PREFAB_FILE_PATH = "Prefabs/";             // prefab�̃I�u�W�F�N�g�t�@�C���p�X
    private const int DEFAULT_STAGE_NUM = 1;                        // �X�e�[�W�ԍ��̃f�t�H���g�l
    private const int EXCEPTION�QNUM = 0;                           // �X�e�[�W�ԍ��̗�O�̒l
    private const float Z_POSITION_FIXED = 0f;

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
        // �X�e�[�W�ԍ����ǂݍ��ݑΏۊO�Ȃ�X�L�b�v
        if (_stageNum == EXCEPTION�QNUM)
        {
            return;
        }

        // �X�e�[�W�I���}�l�[�W���[�����݂���΁A���ݑI�𒆂̃X�e�[�W�ԍ����擾
        if (StageSelectManager.Instance != null)
        {
            _stageNum = StageSelectManager.Instance.GetStageNumber();
        }

        // ���ۂɓǂݍ���JSON�t�@�C���p�X��g�ݗ��Ă�
        string readFilePath = JSON_FILE_PATH + _stageNum;

        // Resources�t�H���_����JSON�f�[�^��ǂݍ���
        TextAsset jsonText = Resources.Load<TextAsset>(readFilePath);

        // �ǂݍ��߂Ȃ������ꍇ�̓G���[���o���ăX�e�[�W�I����ʂɖ߂�
        if (jsonText == null)
        {
            Debug.LogError($"�}�b�v�f�[�^ {readFilePath}.json �� Resources �Ɍ�����܂���ł����B");

#if UNITY_EDITOR

            SceneManager.LoadScene("StageSelectScene");

#endif
            return;
        }

        // �ǂݍ���JSON�����񂩂�}�b�v�f�[�^�̃��X�g�𐶐�
        List<MapObjectData> objects = MapDataSerializer.LoadFromJsonText(jsonText.text);

        // �}�b�v�I�u�W�F�N�g��1���z�u
        foreach (var objData in objects)
        {
            // Resources/Prefabs ����v���n�u��ǂݍ���
            GameObject prefab = Resources.Load<GameObject>(PREFAB_FILE_PATH + objData.prefabName);

            // �v���n�u��������Ȃ��ꍇ�̓X�L�b�v
            if (prefab == null)
            {
                Debug.LogWarning($"�v���n�u '{objData.prefabName}' �� Resources �Ɍ�����܂���B�X�L�b�v���܂��B");
                continue;
            }

            // JSON�Ŏw�肳�ꂽ�ʒu�ɃI�u�W�F�N�g�𐶐��iZ���W��0�Œ�j
            Vector3 pos = new Vector3(objData.position.x, objData.position.y, Z_POSITION_FIXED);
            GameObject instance = Instantiate(prefab, pos, Quaternion.identity);

            // �w�肪����ΐe�I�u�W�F�N�g�̎q�ɐݒ�
            if (parentForObjects != null)
            {
                instance.transform.parent = parentForObjects;
            }
        }

        // �z�u�����̃��O��\��
        Debug.Log($"�}�b�v '{JSON_FILE_PATH}' �̓ǂݍ��݊����I�z�u��: {objects.Count}");
    }
}
