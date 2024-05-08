using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GamePreferences", fileName = "SO_GamePreferences")]
public class SO_GamePreferences : ScriptableObject
{
    #region QOL向上処理
    // CakeParamsSOが保存してある場所のパス
    public const string PATH = "SO_GamePreferences";

    // CakeParamsSOの実体
    private static SO_GamePreferences _entity = null;
    public static SO_GamePreferences Entity
    {
        get
        {
            // 初アクセス時にロードする
            if (_entity == null)
            {
                _entity = Resources.Load<SO_GamePreferences>(PATH);

                //ロード出来なかった場合はエラーログを表示
                if (_entity == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }

            return _entity;
        }
    }
    #endregion

    [SerializeField] Vector2Int resolution;
    public Vector2Int Resolution { get => resolution; }
    
    [SerializeField] bool isFullScreen;
    public bool IsFullScreen { get => isFullScreen; }
}