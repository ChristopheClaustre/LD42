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

    [SerializeField]
    private AudioSource m_pewSound;

    [SerializeField]
    private ParticleSystem m_pew;

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
        return TurretsDataManager.Inst.m_gunnerLevels;
    }

    protected override bool TryToShoot()
    {
        List<GameObject> reachables = GameManager.Inst.Enemies.FindAll(
            x => {
                Vector3 direction = x.transform.position - m_canon.transform.position;
                float distance = Vector3.Distance(x.transform.position, m_canon.transform.position);

                // in range ?
                if (distance > (m_data as TurretsDataManager.DamageTurretData).m_radius) return false;

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
        p_enemy.GetComponent<Enemy>().Hit((m_data as TurretsDataManager.DamageTurretData).m_hit);

        m_pew.Play();
        m_pewSound.Play();
    }

    #endregion
}
