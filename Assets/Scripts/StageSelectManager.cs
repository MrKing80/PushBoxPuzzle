using UnityEngine;

/// <summary>
/// ステージセレクトに関するマネージャー
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    // jsonファイルに対応した番号を格納する変数
    private int _stageNum = 0;


    public void GetStageNumber(int stageNum)
    {
        _stageNum = stageNum;
    }

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
