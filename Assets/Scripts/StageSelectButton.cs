using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �X�e�[�W�Z���N�g��ʂ��烁�C�����[�v�֑J�ڂ���X�N���v�g
/// </summary>
public class StageSelectButton : MonoBehaviour
{
    /// <summary>
    /// �{�^���������ꂽ��R���[�`�����Ăяo�����\�b�h
    /// </summary>
    /// <param name="stageNum">�X�e�[�W�ԍ�</param>
    public void OnClickStageSelect(int stageNum)
    {
        StartCoroutine(LoadStage(stageNum));
    }

    /// <summary>
    /// �V�[����J�ڂ���O�ɃX�e�[�W�ԍ���ێ�����
    /// </summary>
    /// <param name="stageNum">�X�e�[�W�ԍ�</param>
    private IEnumerator LoadStage(int stageNum)
    {
        StageSelectManager.Instance.GetStageNumber(stageNum);

        yield return new WaitForSecondsRealtime(0.5f);

        Debug.Log("�Q�[���V�[���֑J�ڂ��܂�");
        SceneManager.LoadScene("MainLoop");
    }
}
