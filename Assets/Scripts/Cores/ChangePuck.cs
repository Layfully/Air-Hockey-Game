using UnityEngine;
using System.Collections.Generic;

public class ChangePuck : MonoBehaviour {

    [SerializeField]
    private List<GameObject> playerPuckList = new List<GameObject>(4);
    [SerializeField]
    private List<GameObject> enemyPuckList = new List<GameObject>(4);
    [SerializeField]
    private int activePlayerPuck;
    [SerializeField]
    private int activeEnemyPuck;
    public List<GameObject> PlayerPuckList
    {
        get
        {
            return playerPuckList;
        }

        set
        {
            playerPuckList = value;
        }
    }
    public List<GameObject> EnemyPuckList
    {
        get
        {
            return enemyPuckList;
        }

        set
        {
            enemyPuckList = value;
        }
    }
    public int ActivePlayerPuck
    {
        get
        {
            return activePlayerPuck;
        }

        set
        {
            activePlayerPuck = value;
        }
    }
    public int ActiveEnemyPuck
    {
        get
        {
            return activeEnemyPuck;
        }

        set
        {
            activeEnemyPuck = value;
        }
    }
}
