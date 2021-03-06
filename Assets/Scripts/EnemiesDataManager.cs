﻿/***************************************************/
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
        public EnemyData (EnemyData p_enemyData)
        {
            m_health = p_enemyData.m_health;
            m_velocity = p_enemyData.m_velocity;
        }

        public int m_health;
        public float m_velocity;
    }

    [System.Serializable]
    public class EvilFogData
    {
        public float m_timeBetweenEnemies;
        public float m_radius;
    }

    [System.Serializable]
    public class EvilFogGenerationData
    {
        public int[] m_availableEnemies; // indices selon le type des enemies
        public int m_numberOfEnemies;
    }

    [System.Serializable]
    public class RoundData
    {
        public EvilFogGenerationData[] m_fogs;
    }

    [System.Serializable]
    public struct Data
    {
        public EnemyData[] m_enemies;

        public EvilFogData m_fogData;

        public RoundData[] m_firstRounds;

        public EvilFogGenerationData m_laterRoundFogs;
        public int m_minimalNumberOfLaterRoundFogs;

        public float m_percentageOfAddedHealthPerTurn;
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
