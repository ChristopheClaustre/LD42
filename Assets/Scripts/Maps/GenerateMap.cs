﻿/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class GenerateMap :
    MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/



    #endregion
    #region Constants
    /***************************************************/
    /***  CONSTANTS             ************************/
    /***************************************************/



    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    [SerializeField]
    private Transform m_prefabParent;
    [SerializeField]
    private GameObject m_prefab;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        List<Vector3> used = new List<Vector3>();
        used.Add(m_prefabParent.position);

        for (int i = 0; i < SettingsManager.Inst.m_initialPlateformCount; i++)
        {
            Vector3 position;
            int iter = 0;
            do
            {
                iter++;
                position = GenerateRandomPolarCoordinates(SettingsManager.Inst.m_rayonCore, SettingsManager.Inst.m_rayonSphere);
            } while ( ! CheckCoordinates(position, used) && iter < SettingsManager.Inst.m_mapGenerationCheck);

            if (iter >= SettingsManager.Inst.m_mapGenerationCheck)
            {
                Debug.LogAssertion("Impossible to generate map ! Change settings !");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                break;
            }

            GenerateOnePlateform(position);
            used.Add(position);
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    // Generate Random polar coordinates
    public static Vector3 GenerateRandomPolarCoordinates(float p_minRho, float p_maxRho)
    {
        return new Vector3(
            Random.Range(p_minRho, p_maxRho), // rho
            Random.Range(-180.0f, 180.0f), // phi
            Random.Range(-90.0f, 90)  // theta
            );
    }

    // cartesian coordinates to rayon-longitude-latitude
    public static Vector3 CartesianToPolar(Vector3 p_cartesian)
    {
        return new Vector3(
            p_cartesian.magnitude,
            Mathf.Acos(p_cartesian.z / p_cartesian.magnitude),
            Mathf.Atan(p_cartesian.y / p_cartesian.x)
            );
    }

    // rayon-longitude-latitude to cartesian coordinates
    public static Vector3 PolarToCartesian(Vector3 p_polarCoordinates)
    {
        return new Vector3(
            p_polarCoordinates.x * Mathf.Cos(p_polarCoordinates.y) * Mathf.Cos(p_polarCoordinates.z),
            p_polarCoordinates.x * Mathf.Sin(p_polarCoordinates.y) * Mathf.Cos(p_polarCoordinates.z),
            p_polarCoordinates.x * Mathf.Sin(p_polarCoordinates.z)
            );
    }

    // distance between two points
    public static float PolarDistance(Vector3 p_p1, Vector3 p_p2)
    {
        return Vector3.Distance(PolarToCartesian(p_p1), PolarToCartesian(p_p2));
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private bool CheckCoordinates(Vector3 p_polarPosition, List<Vector3> p_previous)
    {
        return p_previous.TrueForAll(x => PolarDistance(x, p_polarPosition) > SettingsManager.Inst.m_minimalDistanceBetweenPlateform);
    }

    private void GenerateOnePlateform(Vector3 p_polarPosition)
    {
        Quaternion quat = new Quaternion
        {
            eulerAngles = new Vector3(Random.Range(-90, 90), Random.Range(-180, 180), Random.Range(-180, 180))
        };
        Instantiate(m_prefab, PolarToCartesian(p_polarPosition), quat, m_prefabParent);

        // change color
    }

    #endregion
}
