using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialColorChange : MonoBehaviour {

    public Color[] colors;
    public Material mat;
    private float timer;

	// Use this for initialization
	void Start () {
        mat.color = colors[Random.Range(0,colors.Length)];		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            mat.color = colors[Random.Range(0, colors.Length)];

            timer = 0;
        }
	}
}
