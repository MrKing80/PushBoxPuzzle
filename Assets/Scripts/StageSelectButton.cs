using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ステージセレクト画面からメインループへ遷移するスクリプト
/// </summary>
public class StageSelectButton : MonoBehaviour
{
    /// <summary>
    /// ボタンが押されたらコルーチンを呼び出すメソッド
    /// </summary>
    /// <param name="stageNum">ステージ番号</param>
    public void OnClickStageSelect(int stageNum)
    {
        StartCoroutine(LoadStage(stageNum));
    }

    /// <summary>
    /// シーンを遷移する前にステージ番号を保持する
    /// </summary>
    /// <param name="stageNum">ステージ番号</param>
    private IEnumerator LoadStage(int stageNum)
    {
        StageSelectManager.Instance.GetStageNumber(stageNum);

        yield return new WaitForSecondsRealtime(0.5f);

        Debug.Log("ゲームシーンへ遷移します");
        SceneManager.LoadScene("MainLoop");
    }
}
