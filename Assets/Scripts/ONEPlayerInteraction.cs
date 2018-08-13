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

    private int m_gameStateUi;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            m_pew.Play();
            m_pewSound.Play();
            if (m_currentInteractiveObject && m_currentInteractiveObject.tag == "Enemy")
            {
                m_currentInteractiveObject.GetComponent<Enemy>().Hit(SettingsManager.Inst.m_gunDamage);
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if (m_currentInteractiveObject && m_currentInteractiveObject.tag == "PlatformeFace")
            {
                PlatformeFace selectedPlatforme = m_currentInteractiveObject.GetComponent<PlatformeFace>();
                if (selectedPlatforme)
                {
                    GameObject newTurret = m_turretSelector.gameObject.GetComponent<TurretSelector>().GetRealTurret;
                    if(newTurret.transform.GetComponent<Turret>())
                    {
                        int cost = newTurret.transform.GetComponent<Turret>().m_data.m_cost;
                        if (cost < GameManager.Inst.Resource)
                        {
                            GameManager.Inst.RemoveResources(cost);
                            selectedPlatforme.ConstructTurret(m_turretSelector.gameObject.GetComponent<TurretSelector>().GetRealTurret);
                        }
                    }
                    
                }
            }
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
            switch (m_gameStateUi)
            {
                case 0:
                    Camera.main.cullingMask |= LayerMask.GetMask("CooldownBar");
                    m_gameStateUi++;
                    break;
                case 1:
                    Camera.main.cullingMask |= LayerMask.GetMask("HealthBar");
                    Camera.main.cullingMask &= ~ LayerMask.GetMask("CooldownBar");
                    m_gameStateUi++;
                    break;
                case 2:
                    Camera.main.cullingMask |= LayerMask.GetMask("CooldownBar");
                    m_gameStateUi++;
                    break;
                default:
                    Camera.main.cullingMask &= ~LayerMask.GetMask("CooldownBar");
                    Camera.main.cullingMask &= ~LayerMask.GetMask("HealthBar");
                    m_gameStateUi = 0;
                    break;
            }
        }
    }
    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
