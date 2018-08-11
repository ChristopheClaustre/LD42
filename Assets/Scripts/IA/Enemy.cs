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

    private EnemyData m_initial = new EnemyData { m_vitesse = 0.1f, m_health = 12 };
    private int m_health = 12;

    private float timer;
    private float initial = 5;

    [SerializeField]
    private Transform m_healthbar = null;
    [SerializeField]
    private Transform m_canvas = null;

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

        timer = initial;

        Debug.Assert(m_healthbar, "Why there isn't any healthbar on this enemy ?");
    }

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) { Hit(5); timer = initial; }

        transform.position += - transform.position.normalized * m_initial.m_vitesse * Time.deltaTime;
        
        transform.LookAt(Vector3.zero);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        m_canvas.LookAt(Camera.main.transform);
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
        if (m_healthbar)
            m_healthbar.localScale = new Vector3((float)m_health / m_initial.m_health, 1, 1);
    }

    #endregion
}
