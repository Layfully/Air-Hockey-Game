using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour
{
    public GameManager gameManager;
    public enum job
    {
        quit,
        start,
        leave
    }
    public job jobs;
    void Start ()
	{
	    gameManager = FindObjectOfType<GameManager>();
	    switch (jobs)
	    {
            case job.start:
            UnityEngine.Events.UnityAction startAction = () => { this.OneMoreGame(); };
            gameObject.GetComponent<Button>().onClick.AddListener(startAction);
                break;
            case job.leave:
                UnityEngine.Events.UnityAction leaveAction = () => { this.BackToMainMenu(); };
                gameObject.GetComponent<Button>().onClick.AddListener(leaveAction);
                break;
            case job.quit:
                UnityEngine.Events.UnityAction quitAction = () => { this.ExitToWindows(); };
                gameObject.GetComponent<Button>().onClick.AddListener(quitAction);
                break;
	    }
    }
	public  void OneMoreGame()
    {
        GameManager.roundNumber = 0;
        GameManager.enemyWins = 0;
        GameManager.playerWins = 0;
        GameManager.player1Wins = 0;
        GameManager.player2Wins = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public  void BackToMainMenu()
    {
        Destroy(gameManager);
        GameManager.roundNumber = 0;
        GameManager.enemyWins = 0;
        GameManager.playerWins = 0;
        GameManager.player1Wins = 0;
        GameManager.player2Wins = 0;
        SceneManager.LoadScene("MainMenu");
    }
    public  void ExitToWindows()
    {
        Application.Quit();
    }
}
