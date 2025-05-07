using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// �}�b�v�f�[�^�iMapObjectData�j�̕ۑ��E�ǂݍ��݂��s���ÓI�N���X
/// </summary>
public static class MapDataSerializer
{
    /// <summary>
    /// �}�b�v�f�[�^��JSON�`���ŕۑ�����
    /// </summary>
    /// <param name="dataList">�ۑ�����}�b�v�f�[�^�̃��X�g</param>
    /// <param name="path">�ۑ���̃t�@�C���p�X</param>
    public static void SaveToJson(List<MapObjectData> dataList, string path)
    {
        // ���b�p�[�N���X�ŕ�񂾃f�[�^��JSON�ɕϊ��i���`����j
        string json = JsonUtility.ToJson(new Wrapper { list = dataList }, true);
        
        // �w�肳�ꂽ�p�X��JSON������������o��
        System.IO.File.WriteAllText(path, json);
    }

    /// <summary>
    /// JSON�t�@�C������}�b�v�f�[�^��ǂݍ���
    /// </summary>
    /// <param name="path">�ǂݍ��ރt�@�C���̃p�X</param>
    /// <returns>�ǂݍ��܂ꂽ�}�b�v�f�[�^�̃��X�g</returns>
    public static List<MapObjectData> LoadFromJson(string path)
    {
        // �t�@�C�������݂��Ȃ��ꍇ�͋�̃��X�g��Ԃ�
        if (!System.IO.File.Exists(path)) return new List<MapObjectData>();
        
        // �t�@�C���̒��g��ǂݎ��
        string json = System.IO.File.ReadAllText(path);
        
        // JSON���f�V���A���C�Y���ăf�[�^���X�g���擾
        return JsonUtility.FromJson<Wrapper>(json).list;
    }

    /// <summary>
    /// JSON�����񂩂�}�b�v�f�[�^��ǂݍ��ށi�t�@�C���ł͂Ȃ�������𒼐ڎg�p�j
    /// </summary>
    /// <param name="json">JSON�`���̕�����</param>
    /// <returns>�f�V���A���C�Y���ꂽ�}�b�v�f�[�^�̃��X�g</returns>
    public static List<MapObjectData> LoadFromJsonText(string json)
    {
        // JSON���f�V���A���C�Y���ăf�[�^���X�g���擾
        return JsonUtility.FromJson<Wrapper>(json).list;
    }

    /// <summary>
    /// List���ރ��b�p�[�N���X�iJsonUtility�ł͒���List�������Ȃ����ߎg�p�j
    /// </summary>
    [System.Serializable]
    private class Wrapper
    {
        public List<MapObjectData> list;  // �}�b�v�f�[�^�̃��X�g
    }
}
