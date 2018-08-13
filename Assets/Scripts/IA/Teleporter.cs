/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Teleporter :
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

    private bool m_used = false;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void TeleporterUsed()
    {
        m_used = true;
    }

    /********  PROTECTED        ************************/

    protected override TurretsDataManager.TurretData[] GetLevelList()
    {
        return TurretsDataManager.Inst.m_tpLevels;
    }

    protected override bool TryToShoot()
    {
        if (m_used)
        {
            m_used = false;
            return true;
        }

        return false;
    }

    /********  PRIVATE          ************************/

    #endregion
}
