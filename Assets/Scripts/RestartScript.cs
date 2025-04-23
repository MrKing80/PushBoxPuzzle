using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{ 
    void Update()
    {
        ReStart();
    }

    private void ReStart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainLoop");
        }
    }

}
