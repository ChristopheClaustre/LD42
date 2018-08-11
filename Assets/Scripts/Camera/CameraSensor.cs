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

        int layerMask = LayerMask.GetMask("PlatformeFace", "Default");

        Debug.DrawRay(transform.position, ray.direction, Color.red);
        if (Physics.Raycast(ray, out hit, 2.0f, layerMask) )
        {
            Transform objectHit = hit.transform;
            if (objectHit.gameObject.tag == "PlatformeFace")
            {
                if (PreviousFaceHit != objectHit.gameObject)
                {
                    if(PreviousFaceHit)
                    {
                        PreviousFaceHit.layer = 8;
                    }
                    objectHit.gameObject.layer = 0;
                    PreviousFaceHit = objectHit.gameObject;
                }
            }
            else // Default (or future obstacles)
            {
                if (PreviousFaceHit)
                {
                    PreviousFaceHit.layer = 8;
                }
            }
        }
        else
        {
            if (PreviousFaceHit)
            {
                PreviousFaceHit.layer = 8;
                PreviousFaceHit = null;
            }
        }

        if(PreviousFaceHit)
        {
            ONEPlayerInteraction.Instance.CurrentInteractiveObject = PreviousFaceHit;
        }
    }
    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/
    #endregion
}
