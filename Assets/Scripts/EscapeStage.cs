using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeStage : MonoBehaviour
{
    /// <summary>
    /// �S�[������̃G���A�ɓ���ƃN���A�̔����Ԃ�
    /// </summary>
    /// <param name="other">�Փː�̃I�u�W�F�N�g</param>
    private void OnTriggerEnter(Collider other)
    {
        //�Փ˂������肪�v���C���[�ł���Δ����Ԃ�
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene("StageSelectScene");
        }
    }
}
