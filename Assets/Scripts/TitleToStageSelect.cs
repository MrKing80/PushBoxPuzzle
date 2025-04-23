using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトル画面からステージセレクトへ遷移するスクリプト
/// </summary>
public class TitleToStageSelect : MonoBehaviour
{
    void Update()
    {
        if(Input.anyKey)
        {
            SceneManager.LoadScene("StageSelectScene");
        }
    }
}
