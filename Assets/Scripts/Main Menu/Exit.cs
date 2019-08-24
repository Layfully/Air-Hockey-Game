using UnityEngine;
public class Exit : MonoBehaviour
{

    [SerializeField] private AudioSource clickSound = null;

    public void Quit()
    {
        clickSound.Play();

        Application.Quit();
    }
}
