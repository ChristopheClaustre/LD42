/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class SetLightColor :
    MonoBehaviour
{
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    [SerializeField]
    private ParticleSystem m_particle;
    [SerializeField]
    private Light m_light;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        Vector3 polar = GenerateMap.CartesianToPolar(transform.position);
        SetColor(Color.HSVToRGB(
            polar.y / Mathf.PI,
            1 - (polar.x / SettingsManager.Inst.m_rayonSphere) * 0.5f,
            1 - (polar.z / 2 * Mathf.PI) * 0.5f
            ));
    }

    // Update is called once per frame
    private void Update()
    {

    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private void SetColor(Color p_color)
    {
        m_light.color = p_color;
#pragma warning disable CS0618 // Le type ou le membre est obsolète
        m_particle.startColor = p_color;
#pragma warning restore CS0618 // Le type ou le membre est obsolète
    }

    #endregion
}
