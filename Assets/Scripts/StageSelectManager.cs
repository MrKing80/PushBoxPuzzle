using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    private int _stageNum = 0;

    public static StageSelectManager Instance { get; private set; }

    public void GetStageNumber(int stageNum)
    {
        _stageNum = stageNum;
    }

    public int SetStageNumber()
    {
        return _stageNum;
    }
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
