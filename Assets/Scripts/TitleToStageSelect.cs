using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �^�C�g����ʂ���X�e�[�W�Z���N�g�֑J�ڂ���X�N���v�g
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
