/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class ONEPlayerInteraction : MonoBehaviour
{
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public static ONEPlayerInteraction Instance
    {
        get
        {
            if (m_instance == null) m_instance = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ONEPlayerInteraction>();
            return m_instance;
        }
    }

    public GameObject CurrentInteractiveObject
    {
        get
        {
            return m_currentInteractiveObject;
        }
        set
        {
            m_currentInteractiveObject = value;
        }
    }

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/
    private static ONEPlayerInteraction m_instance = null;
    
    private GameObject m_currentInteractiveObject;
    private Transform m_turretSelector;
    private int m_selectedTower;
    private float m_gunCooldown = 0;

    private int m_gameStateUi;

    [SerializeField]
    private AudioSource m_ErrorSound;

    [SerializeField]
    private AudioSource m_pewSound;

    [SerializeField]
    private ParticleSystem m_pew;

    [SerializeField]
    private AudioSource m_beammeup;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    void Start()
    {
        m_turretSelector = transform.Find("T.O.W.E.L");

        m_pew.GetComponent<InflictDamageParticle>().m_damage = SettingsManager.Inst.m_gunDamage;

        m_gameStateUi = 1;
        DisplayGameState();
    }

    // Update is called once per frame
    void Update()
    {
        // gun cooldown
        m_gunCooldown -= Time.deltaTime;

        if (m_currentInteractiveObject 
            && (m_currentInteractiveObject.tag == "TpTurret" || m_currentInteractiveObject.tag == "Turret")
            && ((m_currentInteractiveObject.transform.position - transform.position).magnitude < 2.0f)
            )
        {
            GameObject currentTurret = m_currentInteractiveObject.transform.parent.gameObject;
            int turretLevel = currentTurret.transform.GetComponent<Turret>().GetCurrentLevel();
            if (turretLevel < currentTurret.transform.GetComponent<Turret>().MaxLevel())
            {
                m_turretSelector.gameObject.GetComponent<TurretSelector>().ActiveUpgrader = true;
            }
            else
            {
                m_turretSelector.gameObject.GetComponent<TurretSelector>().ActiveUpgrader = false;
            }
        }
        else
        {
            m_turretSelector.gameObject.GetComponent<TurretSelector>().ActiveUpgrader = false;
        }

        // TP to turret
        if (Input.GetButtonDown("Fire1"))
        {
            if (m_currentInteractiveObject && m_currentInteractiveObject.tag == "TpTurret")
            {
                var teleporter = m_currentInteractiveObject.transform.parent.gameObject.GetComponent<Teleporter>();
                if (teleporter.TurretState == Turret.State.CanShoot)
                {
                    Vector3 deplacement = m_currentInteractiveObject.transform.position - transform.position;
                    deplacement = deplacement.normalized * (deplacement.magnitude - 2);
                    transform.position = transform.position + deplacement;

                    // reset cooldown
                    teleporter.TeleporterUsed();
                    m_beammeup.Play();
                }
                else
                {
                    m_ErrorSound.Play();
                }
            }
        }

        // shoot with gun
        if (Input.GetButton("Fire1"))
        {
            if (m_gunCooldown <= 0)
            {
                m_pew.Play();
                m_pewSound.Play();
                m_gunCooldown = SettingsManager.Inst.m_gunCooldown;
            }
        }

        // Turret management
        if (Input.GetButtonDown("Fire2"))
        {
            // Turret placement
            if (m_currentInteractiveObject && m_currentInteractiveObject.tag == "PlatformeFace")
            {
                PlatformeFace selectedPlatforme = m_currentInteractiveObject.GetComponent<PlatformeFace>();
                if (selectedPlatforme)
                {
                    GameObject newTurret = m_turretSelector.gameObject.GetComponent<TurretSelector>().GetRealTurret;
                    if(newTurret.transform.GetComponent<Turret>())
                    {
                        int cost = newTurret.transform.GetComponent<Turret>().FirstLevelCost();
                        if (cost <= GameManager.Inst.Resource)
                        {
                            GameManager.Inst.RemoveResources(cost);
                            selectedPlatforme.ConstructTurret(m_turretSelector.gameObject.GetComponent<TurretSelector>().GetRealTurret);
                        }
                        else
                        {
                            m_ErrorSound.Play();
                        }
                    }
                }
            }

            // Upgrade turret
            if (m_currentInteractiveObject && (m_currentInteractiveObject.tag == "TpTurret" || m_currentInteractiveObject.tag == "Turret"))
            {
                GameObject currentTurret = m_currentInteractiveObject.transform.parent.gameObject;
                if ((currentTurret.transform.position - transform.position).magnitude < 2.0f)
                {
                    int turretLevel = currentTurret.transform.GetComponent<Turret>().GetCurrentLevel();
                    int cost = currentTurret.transform.GetComponent<Turret>().NextLevelCost(turretLevel);
                    if (cost <= GameManager.Inst.Resource
                        && turretLevel < currentTurret.transform.GetComponent<Turret>().MaxLevel())
                    {
                        GameManager.Inst.RemoveResources(cost);
                        currentTurret.transform.GetComponent<Turret>().NextLevel();
                    }
                    else
                    {
                        m_ErrorSound.Play();
                    }
                }
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            m_turretSelector.gameObject.GetComponent<TurretSelector>().NextTurret();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            m_turretSelector.gameObject.GetComponent<TurretSelector>().PreviousTurret();
        }

        if (Input.GetButtonDown("Switch Game State"))
        {
            m_gameStateUi = (m_gameStateUi + 1) % 4;
            DisplayGameState();
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private void DisplayGameState()
    {
        switch (m_gameStateUi)
        {
            case 0:
                Camera.main.cullingMask &= ~LayerMask.GetMask("CooldownBar");
                Camera.main.cullingMask &= ~LayerMask.GetMask("HealthBar");
                break;
            case 1:
                Camera.main.cullingMask |= LayerMask.GetMask("HealthBar");
                Camera.main.cullingMask &= ~LayerMask.GetMask("CooldownBar");
                break;
            case 2:
                Camera.main.cullingMask &= ~LayerMask.GetMask("HealthBar");
                Camera.main.cullingMask |= LayerMask.GetMask("CooldownBar");
                break;
            case 3:
                Camera.main.cullingMask |= LayerMask.GetMask("CooldownBar");
                Camera.main.cullingMask |= LayerMask.GetMask("HealthBar");
                break;
        }
    }

    #endregion
}
