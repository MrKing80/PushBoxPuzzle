using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private string jsonFileName = "StageData"; // Resources/StageData.json ��Ώ�
    [SerializeField] private Transform parentForObjects; // �z�u�I�u�W�F�N�g�̐e�i��I�u�W�F�N�g�ł�OK�j
    private int _stageNum = 1;
    private void Awake()
    {
        LoadMapFromJson();
    }

    public void LoadMapFromJson()
    {
        if (StageSelectManager.Instance != null)
        {
            _stageNum = StageSelectManager.Instance.SetStageNumber();
        }

        jsonFileName = jsonFileName + _stageNum;

        TextAsset jsonText = Resources.Load<TextAsset>(jsonFileName);

        if (jsonText == null)
        {
            Debug.LogError($"�}�b�v�f�[�^ {jsonFileName}.json �� Resources �Ɍ�����܂���ł����B");
            SceneManager.LoadScene("StageSelectScene");
            return;
        }

        List<MapObjectData> objects = MapDataSerializer.LoadFromJsonText(jsonText.text);

        foreach (var objData in objects)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + objData.prefabName);
            if (prefab == null)
            {
                Debug.LogWarning($"�v���n�u '{objData.prefabName}' �� Resources �Ɍ�����܂���B�X�L�b�v���܂��B");
                continue;
            }

            Vector3 pos = new Vector3(objData.position.x, objData.position.y, 0f);
            GameObject instance = Instantiate(prefab, pos, Quaternion.identity);

            if (parentForObjects != null)
            {
                instance.transform.parent = parentForObjects;
            }
        }

        Debug.Log($"�}�b�v '{jsonFileName}' �̓ǂݍ��݊����I�z�u��: {objects.Count}");
    }
}
