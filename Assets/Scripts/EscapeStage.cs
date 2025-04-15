using UnityEngine;

public class EscapeStage : MonoBehaviour
{
    /// <summary>
    /// ゴール判定のエリアに入るとクリアの判定を返す
    /// </summary>
    /// <param name="other">衝突先のオブジェクト</param>
    private void OnTriggerEnter(Collider other)
    {
        //衝突した相手がプレイヤーであれば判定を返す
        if(other.CompareTag("Player"))
        {
            //ゲームプレイ終了
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
