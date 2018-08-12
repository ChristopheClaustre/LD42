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
        Vector3 polar = CartesianToPolar(transform.position);
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

    private Vector3 CartesianToPolar(Vector3 p_cartesian)
    {
        return new Vector3(
            p_cartesian.magnitude,
            Mathf.Acos(p_cartesian.z / p_cartesian.magnitude),
            Mathf.Atan(p_cartesian.y / p_cartesian.x)
            );
    }

    private void SetColor(Color p_color)
    {
        m_light.color = p_color;
        m_particle.startColor = p_color;
    }

    #endregion
}
