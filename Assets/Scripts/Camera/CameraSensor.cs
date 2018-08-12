﻿/***************************************************/
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
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/
    
    private GameObject PreviousFaceHit;
    #endregion

    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/
    // Use this for initialization
    void Start () {
        PreviousFaceHit = null;
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Vector3 middle = new Vector3(0.5f, 0.5f, 0.0f);
        Ray ray = Camera.main.ViewportPointToRay(middle);


        //Raycast platform;
        int layerMask = LayerMask.GetMask("PlatformeFace", "Default");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) )
        {
            Transform objectHit = hit.transform;
            Debug.Log(objectHit.gameObject.tag);
            if (objectHit.gameObject.tag == "PlatformeFace" && hit.distance < 2.0f)
            {
                
                if (PreviousFaceHit != objectHit.gameObject)
                {
                    if(PreviousFaceHit)
                    {
                        PreviousFaceHit.layer = 8;
                    }
                    objectHit.gameObject.layer = 0;
                    PreviousFaceHit = objectHit.gameObject;
                    ONEPlayerInteraction.Instance.CurrentInteractiveObject = PreviousFaceHit.transform.parent.gameObject;
                }
            }
            else if(objectHit.gameObject.tag == "TpTurret")
            {
                Debug.Log("Hello there");
                ONEPlayerInteraction.Instance.CurrentInteractiveObject = objectHit.gameObject;
            }
            else if (objectHit.gameObject.tag == "Enemy")
            {
                ONEPlayerInteraction.Instance.CurrentInteractiveObject = objectHit.gameObject;
            }
            else // Default (or future obstacles)
            {
                if (PreviousFaceHit)
                {
                    PreviousFaceHit.layer = 8;
                }
                ONEPlayerInteraction.Instance.CurrentInteractiveObject = null;
            }
        }
        else
        {
            if (PreviousFaceHit)
            {
                PreviousFaceHit.layer = 8;
                PreviousFaceHit = null;
            }
            ONEPlayerInteraction.Instance.CurrentInteractiveObject = null;
        }
    }
    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/
    #endregion
}
