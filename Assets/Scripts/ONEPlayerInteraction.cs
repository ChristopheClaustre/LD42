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

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/
    private static ONEPlayerInteraction m_instance = null;
    
    private Transform m_turretSelector;
    private int m_selectedTower;
    private float m_gunCooldown = 0;

    private int m_gameStateUi;

    private CameraSensor m_cameraSensor;

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
        m_cameraSensor = GetComponent<CameraSensor>();

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

        // display pluzicatore
        if (m_cameraSensor.m_kind == CameraSensor.ObjectHitted.Turret)
        {
            Turret turret = m_cameraSensor.m_lastHittedObject.GetComponent<Turret>();
            m_turretSelector.GetComponent<TurretSelector>().ActiveUpgrader = turret.GetCurrentLevel() < turret.MaxLevel();
        }
        else
        {
            m_turretSelector.GetComponent<TurretSelector>().ActiveUpgrader = false;
        }

        // TP to turret
        if (Input.GetButtonDown("Fire1"))
        {
            if (m_cameraSensor.m_kind == CameraSensor.ObjectHitted.TpTurret)
            {
                Teleporter teleporter = m_cameraSensor.m_lastHittedObject.GetComponent<Teleporter>();
                if (teleporter.TurretState == Turret.State.CanShoot)
                {
                    Vector3 deplacement = m_cameraSensor.m_lastHittedObject.transform.position - transform.position;
                    deplacement = deplacement.normalized * (deplacement.magnitude - 3);
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
        if (Input.GetButton("Fire1") && m_cameraSensor.m_kind != CameraSensor.ObjectHitted.TpTurret)
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
            if (m_cameraSensor.m_kind == CameraSensor.ObjectHitted.PlateformFace)
            {
                PlatformeFace selectedPlatforme = m_cameraSensor.m_lastHittedObject.GetComponent<PlatformeFace>();
                if (selectedPlatforme)
                {
                    GameObject newTurret = m_turretSelector.GetComponent<TurretSelector>().GetRealTurret;

                    if (newTurret.transform.GetComponent<Turret>())
                    {
                        int cost = newTurret.GetComponent<Turret>().FirstLevelCost();
                        if (cost <= GameManager.Inst.Resource)
                        {
                            GameManager.Inst.RemoveResources(cost);
                            selectedPlatforme.ConstructTurret(m_turretSelector.GetComponent<TurretSelector>().GetRealTurret);
                        }
                        else
                        {
                            m_ErrorSound.Play();
                        }
                    }
                }
            }

            // Upgrade turret
            if (m_cameraSensor.m_kind == CameraSensor.ObjectHitted.Turret)
            {
                Turret turret = m_cameraSensor.m_lastHittedObject.GetComponent<Turret>();
                int cost = turret.NextLevelCost(turret.GetCurrentLevel());

                if (cost <= GameManager.Inst.Resource && turret.GetCurrentLevel() < turret.MaxLevel())
                {
                    GameManager.Inst.RemoveResources(cost);
                    turret.NextLevel();
                }
                else
                {
                    m_ErrorSound.Play();
                }
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            m_turretSelector.GetComponent<TurretSelector>().NextTurret();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            m_turretSelector.GetComponent<TurretSelector>().PreviousTurret();
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
