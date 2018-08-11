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

    public class EnemyData
    {
        public float m_vitesse;
        public int m_health;
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

    [SerializeField]
    private ManageBar m_healthBar;

    private EnemyData m_initial = new EnemyData { m_vitesse = 0.1f, m_health = 12 };
    private int m_health = 12;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_health = m_initial.m_health;

        UpdateBar();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += - transform.position.normalized * m_initial.m_vitesse * Time.deltaTime;
        
        transform.LookAt(Vector3.zero);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void Hit(int p_damage)
    {
        m_health -= p_damage;
        Debug.Log("Aïe");

        UpdateBar();

        if (m_health <= 0) Die();
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private void Die()
    {
        // do cool stuff

        // then die
        Destroy(gameObject);
    }

    private void UpdateBar()
    {
        m_healthBar.m_value = (float)m_health / m_initial.m_health;
    }

    #endregion
}
