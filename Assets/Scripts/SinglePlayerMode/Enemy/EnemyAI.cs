using UnityEngine;
using System.Collections;
public class EnemyAI : MonoBehaviour {
    public bool fired;

    public enum State
    {
        Init,
        Setup,
        Aim,
        Shoot
    }

    public bool playing = true;
    public State _state;
    public EnemyShootPuck shootPuck;
    private EnemyPointer enemyPointer;
    private WaitForSeconds reactionTime = new WaitForSeconds(0.5f);

	void Start () {
        _state = State.Init;
        StartCoroutine(FSM());
	}

    private void Init()
    {
        enemyPointer = FindObjectOfType<EnemyPointer>();
        shootPuck = FindObjectOfType<EnemyShootPuck>();
        _state = State.Setup;
    }

    private void Setup()
    {
        _state = State.Aim;
    }


    private void Aim()
    {
        enemyPointer.Aim();
        _state = State.Shoot;
    }

    private void Shoot()
    {
        shootPuck.Shoot();
        fired = true;
        _state = State.Setup;
    }

    private IEnumerator FSM()
    {
        while (playing)
        {

            switch (_state)
            {
                case State.Init:
                    Init();
                    break;
                case State.Setup:
                    Setup();
                    break;
                case State.Aim:
                    Aim();
                    break;
                case State.Shoot:
                    Aim();
                    Shoot();
                    break;
            }
            yield return reactionTime;
        }
    }
}
