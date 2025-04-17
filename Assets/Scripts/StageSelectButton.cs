using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectButton : MonoBehaviour
{
    public void OnClickStageSelect(int stageNum)
    {
        StartCoroutine(LoadStage(stageNum));
    }

    private IEnumerator LoadStage(int stageNum)
    {
        StageSelectManager.Instance.GetStageNumber(stageNum);

        yield return new WaitForSecondsRealtime(0.5f);

        Debug.Log("�Q�[���V�[���֑J�ڂ��܂�");
        SceneManager.LoadScene("MainLoop");
    }
}
