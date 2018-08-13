/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Area :
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

    public GameObject m_effects;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    protected override TurretsDataManager.TurretData[] GetLevelList()
    {
        return TurretsDataManager.Inst.m_areaLevels;
    }

    protected override bool TryToShoot()
    {
        m_effects.transform.localScale = Vector3.one * (m_data as TurretsDataManager.DamageTurretData).m_radius;

        List<GameObject> reachables = GameManager.Inst.Enemies.FindAll(
            x => {
                // in range ?
                float distance = Vector3.Distance(x.transform.position, transform.position);
                return (distance < (m_data as TurretsDataManager.DamageTurretData).m_radius);
            });

        foreach (GameObject reachable in reachables)
        {
            Shoot(reachable);
        }

        return true;
    }

    /********  PRIVATE          ************************/

    private void Shoot(GameObject p_enemy)
    {
        p_enemy.GetComponent<Enemy>().Hit((m_data as TurretsDataManager.DamageTurretData).m_hit);
    }

    #endregion
}
