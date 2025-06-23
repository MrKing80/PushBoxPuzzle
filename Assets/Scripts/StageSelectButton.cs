using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �X�e�[�W�Z���N�g��ʂ��烁�C�����[�v�֑J�ڂ���X�N���v�g
/// </summary>
public class StageSelectButton : MonoBehaviour
{
    private const float WAIT_SEC = 0.5f;                //��莞�ԑҋ@���鎞�Ԃ��i�[�����ϐ�
    private const string SCENE_NAME = "MainLoop";       //�J�ڐ�̃V�[�������i�[�����ϐ�

    /// <summary>
    /// �{�^���������ꂽ��R���[�`�����Ăяo�����\�b�h
    /// </summary>
    /// <param name="stageNum">�X�e�[�W�ԍ�</param>
    public void OnClickStageSelect(int stageNum)
    {
        StartCoroutine(LoadStage(stageNum)); // �X�e�[�W�ǂݍ��ݗp�̃R���[�`�����J�n
    }

    /// <summary>
    /// �V�[����J�ڂ���O�ɃX�e�[�W�ԍ���ێ�����
    /// </summary>
    /// <param name="stageNum">�X�e�[�W�ԍ�</param>
    private IEnumerator LoadStage(int stageNum)
    {
        StageSelectManager.Instance.SetStageNumber(stageNum); // �X�e�[�W�ԍ����Ǘ��N���X�ɓn��

        yield return new WaitForSecondsRealtime(WAIT_SEC); // �����҂��Ă���J��

        SceneManager.LoadScene(SCENE_NAME); // ���C�����[�v�V�[���ɑJ��
    }
}
