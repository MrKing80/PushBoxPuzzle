using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeStage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("ステージクリア");
        }
    }
}
