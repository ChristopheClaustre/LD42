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
public class SettingsManager :
    MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    [System.Serializable]
    public class Settings
    {
        public float m_rayonSphere;
        public float m_rayonExternalSphere;
        public float m_rayonCore;
        public int m_initialPlateformCount;
        public float m_minimalDistanceBetweenPlateform;
        public float m_roundDuration;
        public float m_playerSpeed;
        public float m_playerAngularSpeed;
        public int m_gunDamage;
        public float m_gunCooldown;
        public int m_startingResource;
        public int m_resourceGainSpeed;
        public int m_resourceGainQuantity;
        public float m_distanceTurretManagement;

        public int m_mapGenerationCheck;
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

    public static Settings Inst
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


    private static Settings m_settings = null;

    [SerializeField]
    private TextAsset m_settingsFile;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        Debug.Assert(m_settingsFile != null, "Settings file not found...");

        m_settings = JsonUtility.FromJson<Settings>(m_settingsFile.text);
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
