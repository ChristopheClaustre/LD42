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
public class AnimateConsole :
    MonoBehaviour
{
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    [SerializeField]
    private float m_animTime;

    private float m_timer;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_timer = m_animTime;
    }

    // Update is called once per frame
    private void Update()
    {
        m_timer -= Time.deltaTime;
        
        if (m_timer <= 0) // then animate
        {
            Text ui = GetComponent<Text>();
            if (ui.text.EndsWith("_"))
                ui.text = ui.text.Substring(0, ui.text.Length - 1);
            else
                ui.text = ui.text + "_";

            m_timer = m_animTime;
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
