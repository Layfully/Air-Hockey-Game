using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;

public class NewGame : MonoBehaviour
{
    public static GameMode mode;

    public enum GameMode
    {
        Singleplayer,
        LocalMultiplayer,
        Multiplayer,
        MainMenu
    }
    [SerializeField] private Button playMultiplayer;
    [SerializeField] private Button playVersusAI;
    [SerializeField] private Button playInternetGame;
    [SerializeField] private Button playLocalSplitScreen;
    [SerializeField] private Button hostGame;
    [SerializeField] private Button joinGame;
    [SerializeField] private EasyTween[] easyTween;
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private AudioSource clickSound = null;
    [SerializeField] private Button createMatch;
    [SerializeField] private InputField matchName;
    [SerializeField] private Text matchText;
    [SerializeField] private Text joinResult;
    [SerializeField] private GameObject joinButtonPrefab;
    [SerializeField] private Transform prefabParent;
    public Text displayPage;
    public InputField MatchName {
        get { return matchName; }
        set { matchName = value; }
    }
    public Button CreateMatch {
        get { return createMatch; }
        set { createMatch = value; }
    }
    public Button PlayMultiplayer
    {
        get { return playMultiplayer; }

        set { playMultiplayer = value; }
    }
    public Button PlayVersusAI
    {
        get { return playVersusAI; }

        set { playVersusAI = value; }
    }
    public Button PlayInternetGame {
        get { return playInternetGame; }

        set { playInternetGame = value; } }
    public Button PlayLocalSplitScreen
    {
        get { return playLocalSplitScreen; }

        set { playLocalSplitScreen = value; }
    }
    public Button JoinGame
    {
        get { return joinGame; }
        set { joinGame = value; }
    }
    public Button HostGame
    {
        get { return hostGame; }
        set { hostGame = value; }
    }
    public EasyTween[] EasyTween
    {
        get { return easyTween; }

        set { easyTween = value; }
    }
    public Button StartButton
    {
        get { return startButton; }

        set { startButton = value; }
    }
    public Button QuitButton
    {
        get { return quitButton; }

        set { quitButton = value; }
    }
    public Text MatchText
    {
        get { return matchText; }
        set { matchText = value; }
    }
    public Text JoinResult
    {
        get { return joinResult; }
        set { joinResult = value; }
    }
    public Button returnButton;
    private bool toggle = true;
    private float ySnap = 0.65f;
    public List<GameObject> buttons;

    void Start()
    {
        PlayMultiplayer.gameObject.SetActive(false);
        PlayVersusAI.gameObject.SetActive(false);
        PlayInternetGame.gameObject.SetActive(false);
        PlayLocalSplitScreen.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(true);
        QuitButton.gameObject.SetActive(true);
    } 

    public void CreateInternetMatch()
    {
        NetworkManager.singleton.matchMaker.CreateMatch(matchName.text, NetworkManager.singleton.matchSize, true, "","","",0,0,OnMatchCreate);
    }
    public void HostInternetGame() {
        NetworkManager.singleton.StartMatchMaker();
        StartCoroutine(waiter4());
    }
    public void OnMatchListFound(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        if (success)
        {
            if (matchList.Count != 0)
            {
                joinGame.gameObject.SetActive(false);
                hostGame.gameObject.SetActive(false);


                foreach (var match in matchList)
                {
                    buttons.Add(CreateButton(joinButtonPrefab, prefabParent, new Vector2(0.5f, ySnap), new Vector2(0.5f, ySnap), match));
                    ySnap -= 0.15f;
                }
            }
            else
            {
                Debug.Log("No matches in requested room!");
            }
        }
        else
        {
            Debug.LogError("Couldn't connect to match maker");
        }
    }
    public void OnjoinedMatch(bool success, string info, MatchInfo matchInfoData)
    {
        if (success)
        {
            NewGame.mode = NewGame.GameMode.Multiplayer;
            NetworkManager.singleton.StartClient(matchInfoData);
            Debug.Log("Joined");
        }
        else
        {
            Debug.LogError("Join match failed");
        }
    }
    public void OnMatchCreate(bool success,string extendedinfo, MatchInfo matchInfo)
    {
        Debug.Log(success);
        if (success)
        {
            Debug.Log("Create match succeeded");
            NewGame.mode = NewGame.GameMode.Multiplayer;
            NetworkServer.Listen(matchInfo, 9000);
            NetworkManager.singleton.StartHost(matchInfo);
        }
        else
        {
            Debug.LogError("Create match failed");
        }
    }

    public void JoinInternetGame()
    {
        NetworkManager.singleton.StartMatchMaker();
        NetworkManager.singleton.matchMaker.ListMatches(0, 5, "", false, 0, 0, OnMatchListFound );
    }
    public void StartInternetGame()
    {
        StartCoroutine(waiter3());
    }
    public void StartMultiplayer()
    {
        StartCoroutine(waiter2());

    }
    public void StartLocalMultiplayer()
    {
        mode = GameMode.LocalMultiplayer;
        StartCoroutine(wait("LocalMultiplayer"));
    }
    public void StartSinglePlayer()
    {
        mode = GameMode.Singleplayer;
        StartCoroutine(wait("SinglePlayer"));
    }
    public void StartGame()
    {
        StartCoroutine(waiter());
    }
    public void muteSound() {

        toggle = !toggle;
        if (toggle)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private IEnumerator waiter()
    {
        clickSound.Play();

        yield return new WaitForSeconds(.29f);
        StartButton.gameObject.SetActive(false);
        PlayInternetGame.gameObject.SetActive(false);
        PlayLocalSplitScreen.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);
        for (int i = 0; i < 2; i++)
        {
            EasyTween[i].OpenCloseObjectAnimation();
        }
        EasyTween[10].OpenCloseObjectAnimation();
    }
    private IEnumerator waiter2()
    {
        clickSound.Play();

        yield return new WaitForSeconds(.29f);

        PlayMultiplayer.gameObject.SetActive(false);
        PlayVersusAI.gameObject.SetActive(false);
        for (int i = 2; i < 4; i++)
        {
            EasyTween[i].OpenCloseObjectAnimation();
        }
    }
    private IEnumerator waiter3()
    {
        clickSound.Play();

        yield return new WaitForSeconds(.29f);

        PlayLocalSplitScreen.gameObject.SetActive(false);
        PlayInternetGame.gameObject.SetActive(false);
        for (int i = 4; i < 7; i++)
        {
            EasyTween[i].OpenCloseObjectAnimation();
        }
    }
    private IEnumerator waiter4()
    {
        clickSound.Play();

        yield return new WaitForSeconds(.29f);

        JoinResult.gameObject.SetActive(false);
        HostGame.gameObject.SetActive(false);
        JoinGame.gameObject.SetActive(false);
        for (int i = 7; i < 10; i++)
        {
            EasyTween[i].OpenCloseObjectAnimation();
        }
    }
    private IEnumerator wait(string scene)
    {

        clickSound.Play();

        yield return new WaitForSeconds(.29f);

        SceneManager.LoadScene(scene);

    }
    private IEnumerator wait1()
    {
        yield return new WaitForSeconds(1);
        
    }
    public  GameObject CreateButton(GameObject buttonPrefab, Transform canvas, Vector2 cornerTopRight, Vector2 cornerBottomLeft , MatchInfoSnapshot matchName)
    {
        var button = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        var rectTransform = button.GetComponent<RectTransform>();
        var presser = button.GetComponent<Button>();
        Text text = button.GetComponentInChildren<Text>();
        text.text = "Join : " + matchName.name;
        rectTransform.SetParent(canvas.transform);
        rectTransform.anchorMax = cornerTopRight;
        rectTransform.anchorMin = cornerBottomLeft;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0);
        rectTransform.sizeDelta = new Vector2(190 *.7f, 50*.7f);
        rectTransform.localScale = Vector3.one;
        buttonSetup(presser, matchName);
        return button;
    }

    public void JoinMatch(MatchInfoSnapshot match)
    {
        NetworkManager.singleton.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 9000, OnjoinedMatch);
    }
    void buttonSetup(Button button , MatchInfoSnapshot match)
    {
        var x = match;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { JoinMatch(match); });
    }
}
