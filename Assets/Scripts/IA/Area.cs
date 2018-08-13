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

    private TurretsDataManager.DamageTurretData m_damageData = null;

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
        List<GameObject> reachables = GameManager.Inst.Enemies.FindAll(
            x => {
                // in range ?
                float distance = Vector3.Distance(x.transform.position, transform.position);
                return (distance < m_damageData.m_radius);
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
        p_enemy.GetComponent<Enemy>().Hit(m_damageData.m_hit);
    }

    #endregion
}
