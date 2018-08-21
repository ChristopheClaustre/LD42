/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class CameraSensor : MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public enum ObjectHitted
    {
        NotInteresting,
        Turret,
        TpTurret,
        PlateformFace
    }

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    public GameObject m_lastHittedObject;
    public ObjectHitted m_kind;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/
    // Use this for initialization
    void Start () {
        m_lastHittedObject = null;
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Vector3 middle = new Vector3(0.5f, 0.5f, 0.0f);
        Ray ray = Camera.main.ViewportPointToRay(middle);

        //Raycast platform;
        DeactivatePreviousFace();
        int layerMask = LayerMask.GetMask("PlatformeFace", "Default");
        if (Physics.Raycast(ray, out hit, 2 * SettingsManager.Inst.m_rayonExternalSphere, layerMask))
        {
            GameObject objectHit = hit.transform.gameObject;

            if (hit.distance < SettingsManager.Inst.m_distanceTurretManagement)
            {
                if (objectHit.tag == "PlatformeFace")
                {
                    m_kind = ObjectHitted.PlateformFace;
                    objectHit = objectHit.transform.parent.gameObject; // collider is on one of its child
                    ActivateFace(objectHit);
                }
                else if (objectHit.tag == "TpTurret" || objectHit.tag == "Turret")
                {
                    m_kind = ObjectHitted.Turret;
                    objectHit = objectHit.transform.parent.gameObject; // collider is on one of its child
                }
                else
                {
                    m_kind = ObjectHitted.NotInteresting;
                    objectHit = null;
                }
            }
            else if (objectHit.tag == "TpTurret")
            {
                m_kind = ObjectHitted.TpTurret;
                objectHit = objectHit.transform.parent.gameObject; // collider is on one of its child
            }
            else // Nothing interesting
            {
                m_kind = ObjectHitted.NotInteresting;
                objectHit = null;
            }

            m_lastHittedObject = objectHit;
        }
        else
        {
            DeactivatePreviousFace();
            m_kind = ObjectHitted.NotInteresting;
            m_lastHittedObject = null;
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private void DeactivatePreviousFace()
    {
        if (m_lastHittedObject)
        {
            MeshRenderer meshRenderer = m_lastHittedObject.transform.GetComponentInChildren<MeshRenderer>();
            if (meshRenderer && meshRenderer.tag == "PlatformeFace")
            {
                m_lastHittedObject.transform.GetComponentInChildren<MeshRenderer>().enabled = false; // hide
            }
        }
    }

    private void ActivateFace(GameObject p_face)
    {
        p_face.transform.GetComponentInChildren<MeshRenderer>().enabled = true; // show
    }

    #endregion
}
