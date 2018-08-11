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
    private int selectedTower;


    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Fire ?
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (m_currentInteractiveObject.tag == "PlatformeFace")
            {
                PlatformeFace selectedPlatforme = m_currentInteractiveObject.GetComponent<PlatformeFace>();
                if (selectedPlatforme)
                {
                    //Depend of selected tower
                    //selectedPlatforme.Tower = selectedTower

                    selectedPlatforme.ConstructTurret();
                }
            }
        }

        if (Input.GetButtonDown("Mouse ScrollWheel"))
        {
            //Change tower list index
        }
    }
    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/
    #endregion
}
