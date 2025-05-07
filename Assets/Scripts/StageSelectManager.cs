using UnityEngine;

/// <summary>
/// ステージセレクトに関するマネージャー
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    // jsonファイルに対応した番号を格納する変数
    private int _stageNum = 0;


    /// <summary>
    /// ステージ番号を取得するメソッド
    /// </summary>
    /// <param name="stageNum"></param>
    public void GetStageNumber(int stageNum)
    {
        _stageNum = stageNum;
    }

    /// <summary>
    /// ステージ番号を返すメソッド
    /// </summary>
    public int SetStageNumber()
    {
        return _stageNum;
    }


    // ステージセレクトマネージャーのシングルトン
    public static StageSelectManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでオブジェクトを保持
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 重複するインスタンスを破棄
        }
    }

}
