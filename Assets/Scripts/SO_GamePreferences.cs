using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GamePreferences", fileName = "SO_GamePreferences")]
public class SO_GamePreferences : ScriptableObject
{
    #region QOLå¸è„èàóù
    public static readonly string PATH = "SO_GamePreferences";

    private static SO_GamePreferences _entity = null;
    public static SO_GamePreferences Entity
    {
        get
        {
            if (_entity == null)
            {
                _entity = Resources.Load<SO_GamePreferences>(PATH);

                if (_entity == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }
            
            return _entity;
        }
    }
    #endregion

    public Vector2Int Resolution;
    public bool IsFullScreen;
}