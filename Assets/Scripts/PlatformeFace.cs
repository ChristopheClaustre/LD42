/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class PlatformeFace : MonoBehaviour {

    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/
    public GameObject Tower
    {
        get
        {
            return m_tower;
        }
    }
    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    private GameObject m_tower;
    public GameObject m_TowerPH;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    {

    }
    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void ConstructTurret()
    {
        if (! m_tower)
        {
            m_tower = Instantiate(m_TowerPH);
            m_tower.transform.parent = this.transform;
            m_tower.transform.position = this.transform.position;
            m_tower.transform.rotation = this.transform.rotation;
            m_tower.transform.localScale = this.transform.localScale;
        }
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}