using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �^�C�g����ʂ���X�e�[�W�Z���N�g�֑J�ڂ���X�N���v�g
/// </summary>
public class TitleToStageSelect : MonoBehaviour
{
    void Update()
    {
        //�����ꂩ�̃L�[�����͂���Ă���΃V�[����J�ڂ���
        if(Input.anyKey)
        {
            SceneManager.LoadScene("StageSelectScene");
        }
    }
}
