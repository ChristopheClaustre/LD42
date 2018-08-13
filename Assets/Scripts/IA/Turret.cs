/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public abstract class Turret :
    MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public enum State
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

    public State TurretState
    {
        get
        {
            return m_state;
        }

        set
        {
            m_state = value;
        }
    }

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

    public TurretsDataManager.TurretData m_data = new TurretsDataManager.TurretData { m_cooldown = 0.5f, m_cost = 5 };

    private State m_state = State.CanShoot;

    private float m_cooldown = 0;

    private int m_level = 0;

    [SerializeField]
    private ManageBar m_cooldownBar;

    [SerializeField]
    private ShowLevels m_levelsUI;
    [SerializeField]
    private ShowLevels m_levelsAvailableUI;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_data = GetLevelList()[0];

        m_state = State.Cooldown;
        m_cooldown = m_data.m_cooldown;

        UpdateBar();

        m_levelsUI.Value = m_level+1;
        m_levelsAvailableUI.Value = MaxLevel();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (m_state)
        {
            case State.CanShoot:
                if (TryToShoot())
                {
                    m_state = State.Cooldown;
                    m_cooldown = m_data.m_cooldown;

                    UpdateBar();
                }

                break;
            case State.Cooldown:
                m_cooldown -= Time.deltaTime;

                if (m_cooldown <= 0)
                {
                    m_cooldown = 0;
                    m_state = State.CanShoot;
                }

                UpdateBar();
                break;
        }

        UpdateMe(m_state == State.CanShoot, m_cooldown);
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    // get the next level cost
    public int NextLevelCost(int p_currentLevel)
    {
        if (p_currentLevel+1 >= MaxLevel()) return 0;
        else return GetLevelList()[p_currentLevel+1].m_cost;
    }

    // get the first level cost
    public int FirstLevelCost()
    {
        return GetLevelList()[0].m_cost;
    }

    // get the first level cost
    public int GetCurrentLevel()
    {
        return m_level;
    }

    // get the first level cost
    public int MaxLevel()
    {
        return GetLevelList().Length - 1;
    }

    public void NextLevel()
    {
        m_level++;

        var lists = GetLevelList();
        m_data = lists[m_level];

        m_levelsUI.Value = m_level+1;
    }

    /********  PROTECTED        ************************/

    // return true, if action is done and cooldown must begin
    protected abstract bool TryToShoot();

    // Retrieve data from TurretsDataManager
    protected abstract TurretsDataManager.TurretData[] GetLevelList();

    // called at the end of turret update
    protected virtual void UpdateMe(bool p_canShoot, float p_cooldown) { }

    /********  PRIVATE          ************************/
    
    private void UpdateBar()
    {
        if (m_cooldownBar)
            m_cooldownBar.m_value = 1.0f - (m_cooldown / m_data.m_cooldown);
    }

    #endregion
}
