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
        GameObject currentObject = ONEPlayerInteraction.Instance.CurrentInteractiveObject;
        if (currentObject && currentObject.tag == "PlatformeFace")
        {
            GameObject newTurret = m_towel.GetComponent<TurretSelector>().GetRealTurret;
            if (newTurret.transform.GetComponent<Turret>())
            {
                int cost = newTurret.transform.GetComponent<Turret>().m_data.m_cost;
                m_ResourceCostText.text = "-" + cost.ToString();
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
