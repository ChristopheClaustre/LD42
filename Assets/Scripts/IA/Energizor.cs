﻿/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Energizor :
    Turret
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

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



    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    private TurretsDataManager.DamageTurretData m_damageData = null;

    public GameObject m_energie;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    protected override void GetData()
    {
        m_data = TurretsDataManager.Inst.m_areaLevels[0];
        m_damageData = TurretsDataManager.Inst.m_areaLevels[0];
    }

    protected override bool TryToShoot()
    {
        m_energie.SetActive(true);

        if (Vector3.Distance(GameManager.Inst.Player.transform.position, transform.position) > m_damageData.m_radius) return false;

        GameManager.Inst.addResources(m_damageData.m_hit);

        m_energie.SetActive(false);

        return true;
    }

    /********  PRIVATE          ************************/

    #endregion
}