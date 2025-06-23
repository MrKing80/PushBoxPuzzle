using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ステージセレクト画面からメインループへ遷移するスクリプト
/// </summary>
public class StageSelectButton : MonoBehaviour
{
    private const float WAIT_SEC = 0.5f;                //一定時間待機する時間を格納した変数
    private const string SCENE_NAME = "MainLoop";       //遷移先のシーン名を格納した変数

    /// <summary>
    /// ボタンが押されたらコルーチンを呼び出すメソッド
    /// </summary>
    /// <param name="stageNum">ステージ番号</param>
    public void OnClickStageSelect(int stageNum)
    {
        StartCoroutine(LoadStage(stageNum)); // ステージ読み込み用のコルーチンを開始
    }

    /// <summary>
    /// シーンを遷移する前にステージ番号を保持する
    /// </summary>
    /// <param name="stageNum">ステージ番号</param>
    private IEnumerator LoadStage(int stageNum)
    {
        StageSelectManager.Instance.SetStageNumber(stageNum); // ステージ番号を管理クラスに渡す

        yield return new WaitForSecondsRealtime(WAIT_SEC); // 少し待ってから遷移

        SceneManager.LoadScene(SCENE_NAME); // メインループシーンに遷移
    }
}
