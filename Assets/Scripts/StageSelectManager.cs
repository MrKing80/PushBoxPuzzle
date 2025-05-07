using UnityEngine;

/// <summary>
/// �X�e�[�W�Z���N�g�Ɋւ���}�l�[�W���[
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    // json�t�@�C���ɑΉ������ԍ����i�[����ϐ�
    private int _stageNum = 0;


    /// <summary>
    /// �X�e�[�W�ԍ����擾���郁�\�b�h
    /// </summary>
    /// <param name="stageNum"></param>
    public void GetStageNumber(int stageNum)
    {
        _stageNum = stageNum;
    }

    /// <summary>
    /// �X�e�[�W�ԍ���Ԃ����\�b�h
    /// </summary>
    public int SetStageNumber()
    {
        return _stageNum;
    }


    // �X�e�[�W�Z���N�g�}�l�[�W���[�̃V���O���g��
    public static StageSelectManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����ŃI�u�W�F�N�g��ێ�
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // �d������C���X�^���X��j��
        }
    }

}
