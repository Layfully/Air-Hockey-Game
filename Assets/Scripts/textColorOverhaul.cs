using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class textColorOverhaul : MonoBehaviour
{
    public float changeInterval = 2;
    private float timer = 0;
    public Color[] colors;
    private Text text;
	// Use this for initialization
	void Start ()
	{
	    text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime; 
        if (timer >= changeInterval)
        {
            text.color = colors[Random.Range(0,15)];
            timer = 0;
        }
    }
}
