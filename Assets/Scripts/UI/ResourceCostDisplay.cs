/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class ResourceCostDisplay : MonoBehaviour
{
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    public Text m_ResourceCostText;
    public GameObject m_towel;
    public CameraSensor m_cameraSensor;


    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_ResourceCostText.text = "";
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_cameraSensor.m_kind == CameraSensor.ObjectHitted.PlateformFace)
        {
            GameObject newTurret = m_towel.GetComponent<TurretSelector>().GetRealTurret;
            if (newTurret.transform.GetComponent<Turret>())
            {
                int cost = newTurret.transform.GetComponent<Turret>().FirstLevelCost();
                m_ResourceCostText.text = "-" + cost.ToString();
            }
        }
        else if (m_cameraSensor.m_kind == CameraSensor.ObjectHitted.Turret)
        {
            Turret turret = m_cameraSensor.m_lastHittedObject.GetComponent<Turret>();

            if (turret.GetCurrentLevel() < turret.MaxLevel())
            {
                int cost = turret.NextLevelCost(turret.GetCurrentLevel());

                m_ResourceCostText.text = "-" + cost.ToString();
            }
            else
            {
                m_ResourceCostText.text = "";
            }
        }
        else
        {
            m_ResourceCostText.text = "";
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
