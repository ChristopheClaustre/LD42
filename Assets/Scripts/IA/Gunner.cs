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
    MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public class GunnerData
    {
        public float m_range; // m
        public int m_damage; // hp
        public float m_firingRate; // shoot/second
    }

    enum State
    {
        CanShoot,
        Cooldown
    }

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

    private GunnerData m_data = new GunnerData { m_damage = 2, m_firingRate = 0.5f, m_range = 5 };

    private State m_state = State.CanShoot;

    private float m_cooldown = 0;

    [SerializeField]
    private GameObject m_canon = null;

    [SerializeField]
    private ManageBar m_cooldownBar;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_state = State.Cooldown;
        m_cooldown = 1 / m_data.m_firingRate;

        UpdateBar();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (m_state)
        {
            case State.CanShoot:
                List<GameObject> reachables = GameManager.Inst.Enemies.FindAll(
                    x => {
                        Vector3 direction = x.transform.position - m_canon.transform.position;
                        float distance = Vector3.Distance(x.transform.position, m_canon.transform.position);

                        // in range ?
                        if (distance > m_data.m_range) return false;

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
                    m_state = State.Cooldown;
                    m_cooldown = 1 / m_data.m_firingRate;

                    UpdateBar();
                }

                break;
            case State.Cooldown:
                m_cooldown -= Time.deltaTime;
                UpdateBar();

                if (m_cooldown <= 0)
                {
                    m_state = State.CanShoot;
                }

                break;
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private void Shoot(GameObject p_enemy)
    {
        m_canon.transform.LookAt(p_enemy.transform);
        p_enemy.GetComponent<Enemy>().Hit(m_data.m_damage);
    }

    private void UpdateBar()
    {
        m_cooldownBar.m_value = 1.0f - (m_cooldown / (1 / m_data.m_firingRate));
    }

    #endregion
}
