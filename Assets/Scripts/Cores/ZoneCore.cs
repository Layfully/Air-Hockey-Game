using UnityEngine;

public class ZoneCore : MonoBehaviour {

    #region Variables

    [SerializeField]
    private GameObject pucks;
    [SerializeField]
    private string markTag;
    public delegate void EndStage();
    public static event EndStage OnEndStage;
    #endregion

    #region Properties
    public MarkBehaviour MarkBehaviour { get; set; }

    public GameObject Pucks
    {
        get { return pucks; }

        set { pucks = value; }
    }

    public string MarkTag
    {
        get { return markTag; }

        set { markTag = value; }
    }

    public ChangePuck ChangePuck { get; set; }

    #endregion

    #region Functions
    public virtual void Start()
    {
        MarkBehaviour = GameObject.FindGameObjectWithTag(MarkTag).GetComponent<MarkBehaviour>();
        ChangePuck = FindObjectOfType<ChangePuck>();
    }

    void OnEnable()
    {
        OnEndStage += StartNewStage;
    }
    void OnDisable()
    {
        OnEndStage -= StartNewStage;
    }

    void StartNewStage()
    {

    }

    protected void OnEvent()
    {
        EndStage handler = OnEndStage;
        if (handler != null)
            handler();
    }
    #endregion
}
