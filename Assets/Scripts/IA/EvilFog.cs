/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class EvilFog :
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

    public List<GameObject> m_availableEnemies;
    public int m_numberOfEnemies;
    private EnemiesDataManager.FogData m_data;

    private float m_timer = 0;
    
    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_data = EnemiesDataManager.Inst.m_fogData;

        Gizmos.DrawSphere(transform.position, m_data.m_radius);
    }

    // Update is called once per frame
    private void Update()
    {
        m_timer -= Time.deltaTime;

        if (m_timer <= 0)
        {
            CreateAnEnemy();

            m_timer = m_data.m_timeBetweenEnemies;
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private void CreateAnEnemy()
    {
        int index = Random.Range(0, m_availableEnemies.Count - 1);
        Vector3 relativePosition = GenerateMap.PolarToCartesian(GenerateMap.GenerateRandomPolarCoordinates(0, m_data.m_radius));

        // generate enemy
        GameObject enemy = Instantiate(m_availableEnemies[index]);
        enemy.transform.position = transform.position + relativePosition;

        m_numberOfEnemies--;

        if (m_numberOfEnemies <= 0)
            Destroy(gameObject);
    }

    #endregion
}
