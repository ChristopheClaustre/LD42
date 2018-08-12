/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class TurretSelector: MonoBehaviour
{
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/
    public GameObject GetRealTurret
    {
        get
        {
            return GameManager.Inst.Turrets[m_currentTurretIndex];
        }
    }
    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/


    public List<GameObject> m_turrets = new List<GameObject>();
    private int m_currentTurretIndex = 0;
    private Transform m_turretSelector;
    
    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/
    // Use this for initialization
    void Start()
    {
        m_turretSelector = transform.Find("Turret Selector");
        UpdatePistole();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextTurret()
    {
        
        if (m_currentTurretIndex < m_turrets.Count - 1)
        {
            m_currentTurretIndex++;
        }
        else
        {
            m_currentTurretIndex = 0;
        }
        UpdatePistole();
    }

    public void PreviousTurret()
    {
        if (m_currentTurretIndex < m_turrets.Count - 1)
        {
            m_currentTurretIndex++;
        }
        else
        {
            m_currentTurretIndex = 0;
        }
        UpdatePistole();
    }
    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/
    void UpdatePistole()
    {
        if (m_turretSelector)
        {
            Transform currentTower = m_turretSelector.Find("tower");
            if (currentTower)
                Destroy(currentTower.gameObject);
            GameObject tower = Instantiate(m_turrets[m_currentTurretIndex]);
            tower.name = "tower";
            tower.transform.parent = m_turretSelector;
            tower.transform.position = m_turretSelector.position;
            tower.transform.rotation = m_turretSelector.rotation;
            tower.transform.localScale = new Vector3(1, 1, 1);
        }

    }
    #endregion
}
