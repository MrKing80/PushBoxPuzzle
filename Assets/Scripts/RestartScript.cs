using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    void Update()
    {
        ReStart(); // ���t���[���ăX�^�[�g�������`�F�b�N
    }

    private void ReStart()
    {
        // R�L�[�������ꂽ��V�[�������X�^�[�g
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainLoop"); // "MainLoop" �V�[�����ēǂݍ���
        }
    }
}
