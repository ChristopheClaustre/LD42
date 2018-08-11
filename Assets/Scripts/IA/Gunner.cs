/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Gunner :
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

    [SerializeField]
    private GameObject m_canon = null;

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
        m_data = TurretsDataManager.Inst.m_gunnerLevels[0];
        m_damageData = TurretsDataManager.Inst.m_gunnerLevels[0];
    }

    protected override bool TryToShoot()
    {
        List<GameObject> reachables = GameManager.Inst.Enemies.FindAll(
            x => {
                Vector3 direction = x.transform.position - m_canon.transform.position;
                float distance = Vector3.Distance(x.transform.position, m_canon.transform.position);

                // in range ?
                if (distance > m_damageData.m_radius) return false;

                // can see it ?
                RaycastHit hit;
                Ray ray = new Ray(m_canon.transform.position, direction);
                Debug.DrawRay(ray.origin, ray.direction, Color.cyan, Time.deltaTime);

                return Physics.Raycast(ray, out hit, distance + 2) && hit.transform.gameObject == x;
            });

        if (reachables.Count > 0)
        {
            GameObject mostDangerous = reachables[0];
            foreach (GameObject reachable in reachables)
            {
                if (mostDangerous.transform.position.magnitude > reachable.transform.position.magnitude)
                    mostDangerous = reachable;
            }
            Shoot(mostDangerous);

            return true;
        }

        return false;
    }

    /********  PRIVATE          ************************/

    private void Shoot(GameObject p_enemy)
    {
        m_canon.transform.LookAt(p_enemy.transform);
        p_enemy.GetComponent<Enemy>().Hit(m_damageData.m_hit);
    }

    #endregion
}
