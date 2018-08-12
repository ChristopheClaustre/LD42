/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    enum GameState
    {
        Start,
        Fighting,
        Waiting,
        Defeat
    }

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

    private GameState State
    {
        get
        {
            return m_state;
        }
    }

    public GameObject Player
    {
        get
        {
            return m_player;
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

    private List<GameObject> m_enemies = new List<GameObject>();
    private List<GameObject> m_plateforms = new List<GameObject>();

    public List<GameObject> m_turret = new List<GameObject>();

    private GameState m_state;

    public GameObject m_cameraStartMenu;
    public GameObject m_cameraDefeatMenu;

    private GameObject m_player;

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
        SoundManager.Instance.PlaySingle(m_musicTracks[1]);

        m_state = GameState.Start;

        m_player = GameObject.FindGameObjectWithTag("MainCamera");

        m_player.SetActive(false);
        m_cameraStartMenu.SetActive(true);

        // update plateforms list
        m_plateforms.AddRange(GameObject.FindGameObjectsWithTag("Plateform"));
    }

    void OnApplicationFocus(bool hasFocus)
    {
        Cursor.visible = !hasFocus;
    }

    // Update is called once per frame
    private void Update()
    {
        // update enemies list
        m_enemies.Clear();
        m_enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        switch (m_state)
        {
            case GameState.Start:
                if (Input.GetButtonDown("Submit"))
                {
                    StartGame();
                    m_state = GameState.Waiting;
                }
                break;
            case GameState.Fighting:
                ManageRound();
                if (m_enemies.Count == 0)
                {
                    SoundManager.Instance.PlaySingle(m_musicTracks[1]);
                    m_state = GameState.Waiting;
                }
                if (m_plateforms.Count == 0)
                {
                    Defeat();
                    m_state = GameState.Defeat;
                }
                break;
            case GameState.Waiting:
                ManageRound();
                if (m_enemies.Count > 0)
                {
                    SoundManager.Instance.PlaySingle(m_musicTracks[0]);
                    m_state = GameState.Fighting;
                }
                break;
            case GameState.Defeat:
                if (Input.GetButtonDown("Submit"))
                {
                    SceneManager.LoadScene("cricri_scene");
                }
                break;
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void CoreHitted(GameObject p_enemy)
    {
        m_enemies.Remove(p_enemy);
        p_enemy.GetComponent<Enemy>().Hit(9001);

        int index = Random.Range(0, m_plateforms.Count);
        GameObject toDestroy = m_plateforms[index];
        m_plateforms.RemoveAt(index);

        Destroy(toDestroy);
    }

    public void Defeat()
    {
        m_player.SetActive(false);
        m_cameraDefeatMenu.SetActive(true);
    }

    public void StartGame()
    {
        m_player.SetActive(true);
        m_cameraStartMenu.SetActive(false);
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private void NextRound()
    {
        ++m_round;
        Debug.Log("Beginning of round " + (m_round + 1));

        m_nextRoundIn = SettingsManager.Inst.m_roundDuration;
    }

    private void ManageRound()
    {
        // manage round
        m_nextRoundIn -= Time.deltaTime;
        if (m_nextRoundIn <= 0)
            NextRound();
    }

    #endregion
}