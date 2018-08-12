/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class SoundManager : MonoBehaviour {

    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public static SoundManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    public static SoundManager m_instance = null;
    public AudioSource m_music;

    #endregion


    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    // Use this for initialization
    void Start ()
    {
        m_instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void PlaySingle(AudioClip clip)
    {
        m_music.clip = clip;
        m_music.loop = true;
        m_music.Play();
    }
    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/
    #endregion
}
