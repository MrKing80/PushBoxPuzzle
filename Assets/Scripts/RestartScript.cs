using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    void Update()
    {
        ReStart(); // 毎フレーム再スタート処理をチェック
    }

    private void ReStart()
    {
        // Rキーが押されたらシーンをリスタート
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainLoop"); // "MainLoop" シーンを再読み込み
        }
    }
}
