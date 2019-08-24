using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum Winner
    {
        Player,
        Enemy,
        Player1,
        Player2,
        None
    }

    public static Winner winner;
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }
    public delegate void RoundEnd();
    public static event RoundEnd OnRoundEnd = () => { };
    public  static TextControl textControl;
    private ChangePuck changePuck;
    private MarkBehaviour[] markBehaviour;
    private bool won;
    public static int roundNumber = 0;
    public static int playerWins;
    public static int enemyWins;
    public static int player1Wins;
    public static int player2Wins;
    public GameObject endGamePanel;
    private EnemyAI enemyAI;
    private ShootPuck shootPuck;
    private ShootPuck[] localShootPuck;
    private EnemyShootPuck enemyShootPuck;
    private Text text;

    void Start()
    {

        changePuck = FindObjectOfType<ChangePuck>();
        textControl = FindObjectOfType<TextControl>();
        won = false;
        SceneManager.sceneLoaded += Setup;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= Setup;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            return;
        }

        if (NewGame.mode == NewGame.GameMode.Singleplayer)
        {
            if (changePuck.EnemyPuckList.Count == 0 && !won)
            {
                winner = Winner.Enemy;
                won = true;
                roundNumber++;
                OnRoundEnd();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            else if (changePuck.PlayerPuckList.Count == 0 && !won)
            {
                winner = Winner.Player;
                won = true;
                roundNumber++;
                OnRoundEnd();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        else if(NewGame.mode == NewGame.GameMode.LocalMultiplayer)
        {
            if (changePuck.EnemyPuckList.Count == 0 && !won)
            {
                winner = Winner.Player2;
                won = true;
                roundNumber++;
                OnRoundEnd();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            else if (changePuck.PlayerPuckList.Count == 0 && !won)
            {
                winner = Winner.Player1;
                won = true;
                roundNumber++;
                OnRoundEnd();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    
    void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        textControl = FindObjectOfType<TextControl>();
    }

    void Setup(Scene scene,LoadSceneMode mode)
    {
        endGamePanel = GameObject.FindGameObjectWithTag("End");
        changePuck = FindObjectOfType<ChangePuck>();
        textControl = FindObjectOfType<TextControl>();
        won = false;
        textControl.SpritesOfNumbers[5] = textControl.RoundSprites[roundNumber];


        if (NewGame.mode == NewGame.GameMode.Singleplayer)
        {
            markBehaviour = FindObjectsOfType<MarkBehaviour>();
            enemyShootPuck = FindObjectOfType<EnemyShootPuck>();
            shootPuck = FindObjectOfType<ShootPuck>();
            enemyAI = FindObjectOfType<EnemyAI>();
        }
        else if(NewGame.mode == NewGame.GameMode.LocalMultiplayer)
        {
            markBehaviour = FindObjectsOfType<MarkBehaviour>();
            localShootPuck = FindObjectsOfType<ShootPuck>();
        }


        switch (winner)
        {
            case Winner.Player:

                playerWins++;
                winner = Winner.None;

                if (playerWins == 2)
                {
                    endGamePanel.SetActive(true);
                    text = endGamePanel.GetComponentInChildren<Text>();
                    textControl.gameObject.SetActive(false);
                    markBehaviour[0].enabled = false;
                    markBehaviour[1].enabled = false;
                    shootPuck.enabled = false;
                    enemyAI.enabled = false;
                    enemyShootPuck.enabled = false;
                    for (int i = 0; i < endGamePanel.transform.childCount; ++i)
                    {
                        endGamePanel.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    text.text = "Player Wins";
                }

                break;
            case Winner.Enemy:
           
                enemyWins++;
                winner = Winner.None;

                if (enemyWins == 2)
                {
                    markBehaviour[0].enabled = false;
                    markBehaviour[1].enabled = false;
                    enemyAI.enabled = false;
                    shootPuck.enabled = false;
                    enemyShootPuck.enabled = false;
                    endGamePanel.SetActive(true);
                    textControl.gameObject.SetActive(false);
                    for (int i = 0; i < endGamePanel.transform.childCount; ++i)
                    {
                        endGamePanel.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    text = endGamePanel.GetComponentInChildren<Text>();
                    text.text = "Enemy Wins";
                }

                break;

            case Winner.Player1:

                player1Wins++;
                winner = Winner.None;
                if (player1Wins == 2)
                {
                    markBehaviour[0].enabled = false;
                    markBehaviour[1].enabled = false;
                    localShootPuck[0].enabled = false;
                    localShootPuck[1].enabled = false;
                    endGamePanel.SetActive(true);
                    textControl.gameObject.SetActive(false);

                    for (int i = 0; i < endGamePanel.transform.childCount; ++i)
                    {
                        endGamePanel.transform.GetChild(i).gameObject.SetActive(true);
                    }

                    text = endGamePanel.GetComponentInChildren<Text>();
                    text.text = "Player right wins";
                }
                break;

            case Winner.Player2:

                player2Wins++;
                winner = Winner.None;
                if (player2Wins == 2)
                {
                    markBehaviour[0].enabled = false;
                    markBehaviour[1].enabled = false;
                    localShootPuck[0].enabled = false;
                    localShootPuck[1].enabled = false;
                    endGamePanel.SetActive(true);
                    textControl.gameObject.SetActive(false);

                    for (int i = 0; i < endGamePanel.transform.childCount; ++i)
                    {
                        endGamePanel.transform.GetChild(i).gameObject.SetActive(true);
                    }

                    text = endGamePanel.GetComponentInChildren<Text>();
                    text.text = "Player left Wins";
                }

                break;
        }
    }
}
