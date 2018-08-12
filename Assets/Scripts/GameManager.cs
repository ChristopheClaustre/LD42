/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class GameManager :
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

    public List<GameObject> Enemies
    {
        get
        {
            return m_enemies;
        }
    }

    public List<GameObject> Turrets
    {
        get
        {
            return m_turret;
        }
    }

    public static GameManager Inst
    {
        get
        {
            return m_instance;
        }
    }

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

    private static GameManager m_instance = null;
    
    private int m_round = -1;
    private float m_nextRoundIn = 0;

    //Audio
    public List<AudioClip> m_musicTracks = new List<AudioClip>();
    private int m_musicTracksSelected = 0;

    private List<GameObject> m_enemies = new List<GameObject>();

    public List<GameObject> m_turret = new List<GameObject>();

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_instance = this;
        Cursor.visible = false;
        SoundManager.Instance.PlaySingle(m_musicTracks[m_musicTracksSelected]);
    }

    // Update is called once per frame
    private void Update()
    {
        // update ennmies list
        m_enemies.Clear();
        m_enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        // manage round
        m_nextRoundIn -= Time.deltaTime;
        if (m_nextRoundIn <= 0)
            NextRound();
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private void NextRound()
    {
        ++m_round;
        Debug.Log("Beginning of round " + (m_round + 1));

        m_nextRoundIn = SettingsManager.Inst.m_roundDuration;
    }

    #endregion
}