/***************************************************/
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

    protected override void UpdateMe(bool p_canShoot, float p_cooldown, float p_cooldownInitial)
    {
        if (p_canShoot)
            m_energie.transform.localScale = Vector3.one;
        else
        {
            float scale = 0.2f + (1 - p_cooldown / p_cooldownInitial) * 0.6f;
            m_energie.transform.localScale = Vector3.one * scale;
        }
    }

    protected override TurretsDataManager.TurretData[] GetLevelList()
    {
        return TurretsDataManager.Inst.m_energizorLevels;
    }

    protected override bool TryToShoot()
    {
        if (Vector3.Distance(GameManager.Inst.Player.transform.position, transform.position) > (m_data as TurretsDataManager.DamageTurretData).m_radius) return false;

        GameManager.Inst.addResources((m_data as TurretsDataManager.DamageTurretData).m_hit);

        return true;
    }

    /********  PRIVATE          ************************/

    #endregion
}
