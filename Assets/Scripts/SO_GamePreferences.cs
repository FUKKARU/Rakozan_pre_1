using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GamePreferences", fileName = "SO_GamePreferences")]
public class SO_GamePreferences : ScriptableObject
{
    #region QOL���㏈��
    // CakeParamsSO���ۑ����Ă���ꏊ�̃p�X
    public const string PATH = "SO_GamePreferences";

    // CakeParamsSO�̎���
    private static SO_GamePreferences _entity = null;
    public static SO_GamePreferences Entity
    {
        get
        {
            // ���A�N�Z�X���Ƀ��[�h����
            if (_entity == null)
            {
                _entity = Resources.Load<SO_GamePreferences>(PATH);

                //���[�h�o���Ȃ������ꍇ�̓G���[���O��\��
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