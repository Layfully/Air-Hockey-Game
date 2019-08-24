using System.Collections;
using UnityEngine;

public class DetectCollisionPuck : MonoBehaviour
{

    #region Variables
    [SerializeField]
    private GameObject particlePrefab;
    [SerializeField]
    private AudioSource collsionSound;
    #endregion

    #region Properties
    public ChangePuck ChangePuck { get; set; }
    public ShootPuck ShootPuck { get; set; }
    public EnemyShootPuck EnemyShootPuck { get; set; }
    public MarkBehaviour PlayerMarkBehaviour { get; set; }
    public MarkBehaviour EnemyMarkBehaviour { get; set; }
    public MarkBehaviour Player1MarkBehaviour { get; set; }
    public MarkBehaviour Player2MarkBehaviour { get; set; }
    public RotatePointerWithMouse PlayerPointer { get; set; }
    public EnemyPointer EnemyPointer { get; set; }
    public EnemyAI EnemyAI { get; set; }
    public RotatePointerAxis Player2RotatePointer { get; set; }
    public RotatePointerAxis Player1RotatePointer { get; set; }
    public ShootPuck ShootPuck1 { get; set; }
    public ShootPuck ShootPuck2 { get; set; }
    #endregion

    void Start()
    {
        ChangePuck = FindObjectOfType<ChangePuck>();
        
        if (NewGame.mode == NewGame.GameMode.Singleplayer)
        {
            PlayerMarkBehaviour = GameObject.FindGameObjectWithTag("PlayerMark").GetComponent<MarkBehaviour>();
            EnemyMarkBehaviour = GameObject.FindGameObjectWithTag("EnemyMark").GetComponent<MarkBehaviour>();
            EnemyPointer = FindObjectOfType<EnemyPointer>();
            PlayerPointer = FindObjectOfType<RotatePointerWithMouse>();
            ShootPuck = FindObjectOfType<ShootPuck>();
            EnemyAI = FindObjectOfType<EnemyAI>();
            EnemyShootPuck = FindObjectOfType<EnemyShootPuck>();
        }
        else if (NewGame.mode == NewGame.GameMode.LocalMultiplayer)
        {
            ShootPuck2 = GameObject.FindGameObjectWithTag("PucksP2").GetComponent<ShootPuck>();
            ShootPuck1 = GameObject.FindGameObjectWithTag("PucksP1").GetComponent<ShootPuck>();
            Player2MarkBehaviour = GameObject.FindGameObjectWithTag("Player2Mark").GetComponent<MarkBehaviour>();
            Player1MarkBehaviour = GameObject.FindGameObjectWithTag("Player1Mark").GetComponent<MarkBehaviour>();
            Player1RotatePointer = GameObject.FindGameObjectWithTag("PlayerPointer").GetComponent<RotatePointerAxis>();
            Player2RotatePointer = GameObject.FindGameObjectWithTag("Player2Pointer").GetComponent<RotatePointerAxis>();
        }
        else if(NewGame.mode == NewGame.GameMode.Multiplayer){

        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Puck"))
        {


            switch (NewGame.mode)
            {
                case NewGame.GameMode.Singleplayer:

                    if (EnemyAI.fired || ShootPuck.Fired )
                    {
                        StartCoroutine(simulateParticles());
                    }



                    if (ChangePuck.EnemyPuckList[ChangePuck.ActiveEnemyPuck] == gameObject && EnemyAI.fired)
                    {
                        ChangePuck.ActiveEnemyPuck++;
                        if (ChangePuck.ActiveEnemyPuck >= ChangePuck.EnemyPuckList.Count)
                        {
                            ChangePuck.ActiveEnemyPuck = 0;
                        }
                        EnemyMarkBehaviour.target = ChangePuck.EnemyPuckList[ChangePuck.ActiveEnemyPuck].transform;
                        EnemyPointer.target = ChangePuck.EnemyPuckList[ChangePuck.ActiveEnemyPuck].transform;
                        EnemyShootPuck.Puck = ChangePuck.EnemyPuckList[ChangePuck.ActiveEnemyPuck].GetComponent<Rigidbody>();
                        EnemyShootPuck.Pointer.transform.position = EnemyShootPuck.Puck.gameObject.transform.position;
                        EnemyAI.fired = false;
                    }

                    else if (ChangePuck.PlayerPuckList[ChangePuck.ActivePlayerPuck] == gameObject && PlayerPointer != null && ShootPuck.Fired && NewGame.mode == NewGame.GameMode.Singleplayer)
                    {
                        ChangePuck.ActivePlayerPuck++;
                        if (ChangePuck.ActivePlayerPuck >= ChangePuck.PlayerPuckList.Count)
                        {
                            ChangePuck.ActivePlayerPuck = 0;
                        }
                        PlayerMarkBehaviour.target = ChangePuck.PlayerPuckList[ChangePuck.ActivePlayerPuck].transform;
                        PlayerPointer.target = ChangePuck.PlayerPuckList[ChangePuck.ActivePlayerPuck].transform;
                        ShootPuck.Puck = ChangePuck.PlayerPuckList[ChangePuck.ActivePlayerPuck].GetComponent<Rigidbody>();
                        ShootPuck.Pointer.transform.position = ShootPuck.Puck.gameObject.transform.position;
                        ShootPuck.Fired = false;
                    }

                    break;
                case NewGame.GameMode.LocalMultiplayer:

                    if (ShootPuck1.Fired || ShootPuck2.Fired)
                    {
                        StartCoroutine(simulateParticles());
                    }

                    if (ChangePuck.PlayerPuckList[ChangePuck.ActivePlayerPuck] == gameObject && ShootPuck1.Fired)
                    {
                        ChangePuck.ActivePlayerPuck++;
                        if (ChangePuck.ActivePlayerPuck >= ChangePuck.PlayerPuckList.Count)
                        {
                            ChangePuck.ActivePlayerPuck = 0;
                        }
                        Player1MarkBehaviour.target = ChangePuck.PlayerPuckList[ChangePuck.ActivePlayerPuck].transform;
                        Player1RotatePointer.target = ChangePuck.PlayerPuckList[ChangePuck.ActivePlayerPuck].transform;
                        ShootPuck1.Puck = ChangePuck.PlayerPuckList[ChangePuck.ActivePlayerPuck].GetComponent<Rigidbody>();
                        ShootPuck1.Pointer.transform.position = ShootPuck1.Puck.gameObject.transform.position;
                        ShootPuck1.Fired = false;
                    }
                    else if (ChangePuck.EnemyPuckList[ChangePuck.ActiveEnemyPuck] == gameObject && ShootPuck2.Fired /*&& isMultiPlayer*/)
                    {
                        ChangePuck.ActiveEnemyPuck++;
                        if (ChangePuck.ActiveEnemyPuck >= ChangePuck.EnemyPuckList.Count)
                            {
                                ChangePuck.ActiveEnemyPuck = 0;
                            }
                        Player2MarkBehaviour.target = ChangePuck.EnemyPuckList[ChangePuck.ActiveEnemyPuck].transform;
                        Player2RotatePointer.target = ChangePuck.EnemyPuckList[ChangePuck.ActiveEnemyPuck].transform;
                        ShootPuck2.Puck = ChangePuck.EnemyPuckList[ChangePuck.ActiveEnemyPuck].GetComponent<Rigidbody>();
                        ShootPuck2.Pointer.transform.position = ShootPuck2.Puck.gameObject.transform.position;
                        ShootPuck2.Fired = false;
                    }
                    break;
            }
        }
    }

    IEnumerator simulateParticles()
    {
        var particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        collsionSound.Play();
        yield return new WaitForSeconds(collsionSound.clip.length);
        Destroy(particle);
    }
}



