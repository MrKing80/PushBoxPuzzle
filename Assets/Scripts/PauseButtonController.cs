using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseCanvas = default;     //ポーズメニューのオブジェクトを格納する変数


    /// <summary>
    /// ポーズメニューを閉じる
    /// </summary>
    public void CloseButton()
    {
        _pauseCanvas.SetActive(false);
    }

    /// <summary>
    /// タイトルへ戻る
    /// </summary>
    public void ExitButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
