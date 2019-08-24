using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextControl : MonoBehaviour
{

    #region Variables
    [SerializeField]
    private RotatePointerWithMouse rotatePointerWithMouse;
    [SerializeField]
    private ShootPuck shootPuck;
    [SerializeField]
    private EnemyAI enemyAi;
    [SerializeField]
    private AudioSource clip;
    [SerializeField]
    private AudioSource clip1;
    [SerializeField]
    private Image display;
    [SerializeField]
    private RectTransform displayRect;
    [SerializeField]
    private ShootPuck[] shootpuck;

    private readonly Vector2 displaySize = new Vector2(300, 100);
    private int index;
    private bool isPlayingClip;
    private int seconds;
    [SerializeField]
    private Sprite[] roundSprites;
    [SerializeField]
    private Sprite[] spritesOfNumbers;
    private float timeLeft;
    private readonly WaitForSeconds wait = new WaitForSeconds(1);
    #endregion

    #region Properites
    public AudioSource Clip
    {
        get { return clip; }
    }
    public AudioSource Clip1
    {
        get { return clip1; }
    }
    public Image Display
    {
        get { return display; }

    }
    public RectTransform DisplayRect
    {
        get { return displayRect; }
    }
    public bool IsPlaying { get; set; }
    public Sprite[] SpritesOfNumbers
    {
        get { return spritesOfNumbers; }
    }
    public float TimeLeft
    {
        get { return timeLeft; }

        set { timeLeft = value; }
    }
    public RotatePointerWithMouse RotatePointerWithMouse
    {
        get { return rotatePointerWithMouse; }

        set { rotatePointerWithMouse = value; }
    }
    public EnemyAI EnemyAi
    {
        get { return enemyAi; }

        set { enemyAi = value; }
    }
    public ShootPuck[] ShootPuck
    {
        set { shootpuck = value; }
        get { return shootpuck; }
    }

    #endregion

    #region Functions

    private void Start()
    {
        if (NewGame.mode == NewGame.GameMode.Singleplayer)
        {
            shootPuck = FindObjectOfType<ShootPuck>();
            EnemyAi = FindObjectOfType<EnemyAI>();
            RotatePointerWithMouse = FindObjectOfType<RotatePointerWithMouse>();
        }

        else if(NewGame.mode == NewGame.GameMode.LocalMultiplayer)
        {
            ShootPuck = FindObjectsOfType<ShootPuck>();
        }

        Setup();
    }

    private void Update()
    {
        TimeLeft -= Time.deltaTime;
        TimeLeft = Mathf.Clamp(TimeLeft, 0, 6);
        seconds = (int)TimeLeft;
        Display.sprite = SpritesOfNumbers[seconds];

        if (Display.sprite == SpritesOfNumbers[0])
        {
            DisplayRect.sizeDelta = displaySize;
            StartCoroutine(WaitSecondAndDestroy());
        }
        if (display.sprite == SpritesOfNumbers[SpritesOfNumbers.Length - 2])
        {
            DisplayRect.sizeDelta = new Vector2(100, 100);
        }
    }

    #endregion

    #region Functions

    public void Setup()
    {
        if (NewGame.mode == NewGame.GameMode.Singleplayer)
        {
            shootPuck.enabled = false;
            EnemyAi.enabled = false;
            RotatePointerWithMouse.enabled = false;
        }
        else if(NewGame.mode == NewGame.GameMode.LocalMultiplayer)
        {
            for (int i = 0; i < ShootPuck.Length; i++)
            {
                ShootPuck[i].enabled = false;
            }
        }

        index = 0;
        gameObject.SetActive(true);
        isPlayingClip = true;
        TimeLeft = 6;
        IsPlaying = true;
        DisplayRect.sizeDelta = displaySize;
        Display.sprite = SpritesOfNumbers[0];
        StartCoroutine(Play);
    }
    #endregion

    #region IEnumerators
    private IEnumerator WaitSecondAndDestroy() { 
    
        yield return wait;

        if (NewGame.mode == NewGame.GameMode.Singleplayer) {
            RotatePointerWithMouse.enabled = true;
            EnemyAi.enabled = true;
            shootPuck.enabled = true;
        }

        else if (NewGame.mode == NewGame.GameMode.LocalMultiplayer)
        {
            for (int i = 0; i < ShootPuck.Length; i++)
            {
                ShootPuck[i].enabled = true;
            }
        }

        gameObject.SetActive(false);
    }
    private IEnumerator Play
    {
        get
        {
            while (IsPlaying)
            {
                if (isPlayingClip)
                    Clip.Play();
                else
                    Clip1.Play();
                index++;
                if (index == 5)
                    isPlayingClip = false;
                yield return wait;
            }
        }
    }
    public Sprite[] RoundSprites
    {
        get
        {
            return roundSprites;
        }

        set
        {
            roundSprites = value;
        }
    }
    #endregion
}