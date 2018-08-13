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

    public bool ActiveUpgrader
    {
        get
        {
            return m_activeUpgrader;
        }
        set
        {
            m_activeUpgrader = value;
        }
    }
    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/


    public List<GameObject> m_turrets = new List<GameObject>();
    public GameObject m_upgrader;
    private bool m_activeUpgrader = false;
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
        UpdatePistole();
    }

    public void SelectUpgrader(bool p_value)
    {
        m_activeUpgrader = p_value;
    }

    public void NextTurret()
    {
        
        if (m_currentTurretIndex < m_turrets.Count - 1 && !m_activeUpgrader)
        {
            m_currentTurretIndex++;
        }
        else
        {
            m_currentTurretIndex = 0;
        }
    }

    public void PreviousTurret()
    {
        if (m_currentTurretIndex > 0 && !m_activeUpgrader)
        {
            m_currentTurretIndex--;
        }
        else
        {
            m_currentTurretIndex = m_turrets.Count - 1;
        }
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
            GameObject tower;
            if (m_activeUpgrader)
                tower = Instantiate(m_upgrader);
            else
                tower = Instantiate(m_turrets[m_currentTurretIndex]);

            tower.name = "tower";
            tower.transform.parent = m_turretSelector;
            tower.transform.position = m_turretSelector.position;
            tower.transform.rotation = m_turretSelector.rotation;
            tower.transform.localScale = new Vector3(1, 1, 1);
        }

    }
    #endregion
}
