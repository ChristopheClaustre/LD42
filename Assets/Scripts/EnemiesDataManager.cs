/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class EnemiesDataManager :
    MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    public enum EnemyType
    {
        NORMAL,
        FAST,
        SHIELD
    }

    [System.Serializable]
    public class EnemyData
    {
        public int m_health;
        public float m_velocity;
    }

    [System.Serializable]
    public struct Data
    {
        public EnemyData[] m_enemies;
        
        public int m_initialNumberOfFogs;
        public int m_initialNumberOfFoes;
        public int m_finalNumberOfFoes;
        public int m_turnoverRound;
    }

    #endregion
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/



    #endregion
    #region Constants
    /***************************************************/
    /***  CONSTANTS             ************************/
    /***************************************************/

    public static Data Inst
    {
        get
        {
            return m_settings;
        }
    }

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/


    private static Data m_settings;

    [SerializeField]
    private TextAsset m_file;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        Debug.Assert(m_file != null, "Settings file not found...");

        m_settings = JsonUtility.FromJson<Data>(m_file.text);
    }

    // Update is called once per frame
    private void Update()
    {

    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
