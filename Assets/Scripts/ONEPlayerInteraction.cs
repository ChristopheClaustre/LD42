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
    private GameObject m_pistole;
    private int m_currentTurretIndex = 0;
    private int m_selectedTower;


    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/
    // Use this for initialization
    void Start()
    {
        m_pistole = transform.Find("T.O.W.E.L").gameObject;
        m_pistole.GetComponent<Renderer>().material = GameManager.Inst.Turrets[m_currentTurretIndex].GetComponent<Renderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (m_currentInteractiveObject && m_currentInteractiveObject.tag == "Enemy")
            {
                m_currentInteractiveObject.GetComponent<Enemy>().Hit(2/*TODO USE DATA*/);
            }
        }
        if(m_currentInteractiveObject)
            Debug.Log(m_currentInteractiveObject.tag);
        if (Input.GetButtonDown("Fire2"))
        {
            if (m_currentInteractiveObject && m_currentInteractiveObject.tag == "PlatformeFace")
            {
                PlatformeFace selectedPlatforme = m_currentInteractiveObject.GetComponent<PlatformeFace>();
                if (selectedPlatforme)
                {
                    selectedPlatforme.ConstructTurret(GameManager.Inst.Turrets[m_currentTurretIndex]);
                }
            }
            if (m_currentInteractiveObject && m_currentInteractiveObject.tag == "TpTurret")
            {
                Vector3 deplacement = m_currentInteractiveObject.transform.position - transform.position;
                deplacement = deplacement.normalized * (deplacement.magnitude-2);
                transform.position = transform.position + deplacement;
            }

        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (m_currentTurretIndex < GameManager.Inst.Turrets.Count-1)
            {
                m_currentTurretIndex++;
            }
            else
            {
                m_currentTurretIndex = 0;
            }
            m_pistole.GetComponent<Renderer>().material = GameManager.Inst.Turrets[m_currentTurretIndex].GetComponent<Renderer>().sharedMaterial;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (m_currentTurretIndex > 0)
            {
                --m_currentTurretIndex;
            }
            else
            {
                m_currentTurretIndex = GameManager.Inst.Turrets.Count-1;
            }
            m_pistole.GetComponent<Renderer>().material = GameManager.Inst.Turrets[m_currentTurretIndex].GetComponent<Renderer>().sharedMaterial;
        }

        if (Input.GetButtonDown("Show Game State"))
        {
            Camera.main.cullingMask |= LayerMask.GetMask("GameState");
        }
        else if (Input.GetButtonUp("Show Game State"))
        {
            Camera.main.cullingMask &= ~ LayerMask.GetMask("GameState");
        }
    }
    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/
    #endregion
}
