/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public int Resource
    {
        get
        {
            return m_resources;
        }
    }

    public int Round
    {
        get
        {
            return m_round;
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

    public List<GameObject> m_availableEnemies = new List<GameObject>();

    private GameState m_state;

    public GameObject m_cameraStartMenu;
    public Text m_text;
    [TextArea] public string m_endString;
    [TextArea] public string m_startString;

    private GameObject m_player;

    //Resources
    private int m_resources;
    private float m_resourceTimer = 0;

    public GameObject m_prefabFog;

    public AudioSource m_warning;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        ManageCursor(true);

        m_instance = this;
        SoundManager.Instance.PlaySingle(m_musicTracks[1]);

        m_state = GameState.Start;

        m_player = GameObject.FindGameObjectWithTag("MainCamera");

        m_player.SetActive(false);
        m_cameraStartMenu.SetActive(true);
        m_text.text = m_startString;

        m_resourceTimer = SettingsManager.Inst.m_resourceGainSpeed;
        m_resources = SettingsManager.Inst.m_startingResource;
        // update plateforms list
        m_plateforms.AddRange(GameObject.FindGameObjectsWithTag("Plateform"));
    }

    void OnApplicationFocus(bool hasFocus)
    {
        ManageCursor(hasFocus);
    }

    private void OnMouseDown()
    {
        ManageCursor(true);
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
                ManageResources();
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
                ManageResources();
                if (m_enemies.Count > 0)
                {
                    SoundManager.Instance.PlaySingle(m_musicTracks[0]);
                    m_state = GameState.Fighting;
                }
                break;
            case GameState.Defeat:
                if (Input.GetButtonDown("Quit"))
                {
                    Application.Quit();
                }
                if (Input.GetButtonDown("Submit"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                break;
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void CoreHitted(GameObject p_enemy)
    {
        m_enemies.Remove(p_enemy);
        p_enemy.GetComponent<Enemy>().Die();

        if (m_plateforms.Count > 0)
        {
            int index = Random.Range(0, m_plateforms.Count);
            GameObject toDestroy = m_plateforms[index];
            m_plateforms.RemoveAt(index);

            Destroy(toDestroy);
        }
    }

    public void Defeat()
    {
        m_player.SetActive(false);
        m_cameraStartMenu.SetActive(true);
        m_text.text = m_endString.Replace("$round$", "" + m_round);
    }

    public void StartGame()
    {
        m_player.SetActive(true);
        m_cameraStartMenu.SetActive(false);
    }

    public void addResources(int p_quantity)
    {
        m_resources += p_quantity;
    }

    public void RemoveResources(int p_quantity)
    {
        if(m_resources - p_quantity >= 0)
        {
            m_resources -= p_quantity;
        }
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private void ManageCursor(bool p_lock)
    {
        Cursor.lockState = p_lock? CursorLockMode.Locked: CursorLockMode.None;
    }

    private void NextRound()
    {
        ++m_round;
        Debug.Log("Beginning of round " + (m_round + 1));
        m_warning.Play();

        // Create fogs
        if (m_round < EnemiesDataManager.Inst.m_firstRounds.Length)
            CreateRound(EnemiesDataManager.Inst.m_firstRounds[m_round]);
        else
        {
            int delta = m_round - EnemiesDataManager.Inst.m_firstRounds.Length;
            
            for (int i = 0; i < EnemiesDataManager.Inst.m_minimalNumberOfLaterRoundFogs + delta; i++)
            {
                CreateFog(EnemiesDataManager.Inst.m_laterRoundFogs);
            }
        }

        m_nextRoundIn = SettingsManager.Inst.m_roundDuration;
    }

    private void CreateRound(EnemiesDataManager.RoundData p_data)
    {
        foreach (var m_fog in p_data.m_fogs)
        {
            CreateFog(m_fog);
        }
    }

    private void CreateFog(EnemiesDataManager.EvilFogGenerationData p_data)
    {
        GameObject fog = Instantiate(m_prefabFog,
            GenerateMap.PolarToCartesian(GenerateMap.GenerateRandomPolarCoordinates(SettingsManager.Inst.m_rayonExternalSphere, SettingsManager.Inst.m_rayonExternalSphere)),
            Quaternion.identity);

        fog.GetComponent<EvilFog>().m_availableEnemies.Clear();
        foreach (int index in p_data.m_availableEnemies)
        {
            fog.GetComponent<EvilFog>().m_availableEnemies.Add(m_availableEnemies[index]);
        }
        fog.GetComponent<EvilFog>().m_numberOfEnemies = p_data.m_numberOfEnemies;
    }

    private void ManageRound()
    {
        // manage round
        m_nextRoundIn -= Time.deltaTime;
        if (m_nextRoundIn <= 0)
            NextRound();
    }

    private void ManageResources()
    {
        m_resourceTimer -= Time.deltaTime;
        if (m_resourceTimer < 0)
        {
            m_resources = m_resources + SettingsManager.Inst.m_resourceGainQuantity;
            m_resourceTimer = SettingsManager.Inst.m_resourceGainSpeed;
        }
    }

    #endregion
}