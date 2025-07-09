using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseCanvas = default;     //�|�[�Y���j���[�̃I�u�W�F�N�g���i�[����ϐ�


    /// <summary>
    /// �|�[�Y���j���[�����
    /// </summary>
    public void CloseButton()
    {
        _pauseCanvas.SetActive(false);
    }

    /// <summary>
    /// �^�C�g���֖߂�
    /// </summary>
    public void ExitButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
