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

    public TurretsDataManager.TurretData m_data = new TurretsDataManager.TurretData { m_cooldown = 0.5f, m_cost = 5 };

    private State m_state = State.CanShoot;

    private float m_cooldown = 0;

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
        GetData();

        m_state = State.Cooldown;
        m_cooldown = m_data.m_cooldown;

        UpdateBar();
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
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    // return true, if action is done and cooldown must begin
    protected abstract bool TryToShoot();

    // Retrieve data from TurretsDataManager
    protected abstract void GetData();

    /********  PRIVATE          ************************/

    private void UpdateBar()
    {
        m_cooldownBar.m_value = 1.0f - (m_cooldown / m_data.m_cooldown);
    }

    #endregion
}
