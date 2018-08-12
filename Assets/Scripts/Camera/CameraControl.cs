/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class CameraControl : MonoBehaviour {

    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    public float m_mainSpeed;
    public float m_camSens;
    [Range(0.0f, 1.0f)]
    public float m_radius = 0.1f;
    public Texture2D crosshairImage;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    private void Start()
    {
        m_mainSpeed = SettingsManager.Inst.m_playerSpeed;
        m_camSens = SettingsManager.Inst.m_playerAngularSpeed;
    }

    private void Update()
    {

        float h = m_camSens/100 * Input.GetAxis("Mouse X");
        float v = m_camSens/100 * Input.GetAxis("Mouse Y");
        transform.Rotate(-v, h, 0);

        //**************************************
        //Other camera setting DO NOT DELETE
        //**************************************
        //Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //mousePos.x -= 0.5f;
        //mousePos.y -= 0.5f;

        //// No effect circle zone 
        //if ( (mousePos.x*mousePos.x) + (mousePos.y * mousePos.y) < (m_radius * m_radius))
        //{
        //    mousePos.x = 0;
        //    mousePos.y = 0;
        //}

        ////Pitch 
        //Vector3 pitch = new Vector3(-mousePos.normalized.y,0,0);
        //transform.Rotate(pitch * Time.deltaTime * m_camSens * Mathf.InverseLerp(0, 0.5f, Mathf.Abs(mousePos.y)));
        ////Yaw
        //Vector3 yaw = new Vector3(0, mousePos.normalized.x, 0);
        //if (transform )
        //transform.Rotate(yaw * Time.deltaTime * m_camSens * Mathf.InverseLerp(0, 0.5f, Mathf.Abs(mousePos.x)));

        //**************************************
        //Roll
        if (Input.GetAxis("Roll") < 0)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * m_camSens);
        }
        if (Input.GetAxis("Roll") > 0)
        {
            transform.Rotate(Vector3.back * Time.deltaTime * m_camSens);
        }

        //Keyboard commands
        Vector3 p = GetBaseInput();
        p = p * m_mainSpeed;
        p = p * Time.deltaTime;
        transform.Translate(p);

    }

    void OnGUI()
    {
        float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
        float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
    }
    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/
    private Vector3 GetBaseInput()
    {
        Vector3 p_Velocity = new Vector3();
        if (Input.GetButton("Forward"))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetButton("Backward"))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetButton("Left"))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetButton("Right"))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        if (Input.GetButton("Up"))
        {
            p_Velocity += new Vector3(0, 1, 0);
        }
        if (Input.GetButton("Down"))
        {
            p_Velocity += new Vector3(0, -1, 0);
        }
        return p_Velocity;
    }
    #endregion
}