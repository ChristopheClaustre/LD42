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
public class TurretsDataManager :
    MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    [System.Serializable]
    public class EnemyData
    {
        public int m_cost;
        public float m_cooldown;
    }

    [System.Serializable]
    public class DamageTurretData : EnemyData
    {
        public int m_hit;
        public float m_radius;
    }

    [System.Serializable]
    public class FreezerData : EnemyData
    {
        public float m_radius;
        public float m_frozePower;
        public float m_speedUpPower;
    }

    [System.Serializable]
    public class Data
    {
        public DamageTurretData[] m_gunnerLevels;
        public DamageTurretData[] m_areaLevels;
        public DamageTurretData[] m_energizorLevels;
        public EnemyData[] m_tpLevels;
        public FreezerData[] m_freezerLevels;
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


    private static Data m_settings = null;

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
