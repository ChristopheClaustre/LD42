/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Enemy :
    MonoBehaviour
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
    private ManageBar m_healthBar;

    private EnemiesDataManager.EnemyData m_initial;
    private int m_health = 12;

    public EnemiesDataManager.EnemyType m_type;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_initial = new EnemiesDataManager.EnemyData(EnemiesDataManager.Inst.m_enemies[(int)m_type]);
        m_initial.m_health += Mathf.RoundToInt(m_initial.m_health * 0.1f * GameManager.Inst.Round);

        m_health = m_initial.m_health;

        UpdateBar();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += - transform.position.normalized * m_initial.m_velocity * Time.deltaTime;
        
        if (m_initial.m_velocity != 0)
            transform.LookAt(Vector3.zero);

        Rigidbody r = GetComponent<Rigidbody>();
        if (r)
        {
            r.velocity = Vector3.zero;
            r.angularVelocity = Vector3.zero;
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void Hit(int p_damage)
    {
        Transform shieldTr = transform.Find("Shield");
        if (shieldTr)
        {
            shieldTr.gameObject.GetComponentInChildren<Enemy>().Hit(p_damage);
        }
        else
        {
            m_health -= p_damage;

            UpdateBar();

            if (m_health <= 0) Die();
        }
    }

    public void Die()
    {
        // do cool stuff

        // then die
        Destroy(gameObject);
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private void UpdateBar()
    {
        m_healthBar.m_value = (float)m_health / m_initial.m_health;
    }

    #endregion
}
