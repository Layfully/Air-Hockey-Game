﻿////////////////////////////////////////////////////////////////////////////////////
//  CameraFilterPack v2.0 - by VETASOFT 2015 //////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu ("Camera Filter Pack/AAA/Super Hexagon")]
public class CameraFilterPack_AAA_SuperHexagon : MonoBehaviour {
	#region Variables
	public Shader SCShader;
	[Range(0.0f, 1.0f)]
	public float _AlphaHexa = 1.0f;
	private float TimeX = 1.0f;
	private Material SCMaterial;
	[Range(0.2f, 10.0f)]
	public float HexaSize = 2.5f;
	public float _BorderSize = 1.0f;
	public Color _BorderColor = new Color (0.75f, 0.75f, 1, 1);
	public Color _HexaColor = new Color (0, 0.5f, 1, 1);
	public float _SpotSize = 2.5f;

	public static float ChangeBorderSize = 1.0f;
	public static Color ChangeBorderColor;
	public static Color ChangeHexaColor;
	public static float ChangeSpotSize = 1.0f;
	public static float ChangeAlphaHexa = 1.0f;
	public static float ChangeValue;
	public Vector2 center = new Vector2(0.5f,0.5f);
	public float Radius = 0.25f;

	public static Vector2 Changecenter;
	public static float ChangeRadius;
	

	#endregion
	
	#region Properties
	Material material
	{
		get
		{
			if(SCMaterial == null)
			{
				SCMaterial = new Material(SCShader);
				SCMaterial.hideFlags = HideFlags.HideAndDontSave;	
			}
			return SCMaterial;
		}
	}
	#endregion
	void Start () 
	{
		Changecenter = center;
		ChangeRadius = Radius;
		ChangeValue = HexaSize;
		ChangeAlphaHexa = _AlphaHexa;

		ChangeBorderSize = _BorderSize;
		ChangeBorderColor = _BorderColor;
		ChangeHexaColor = _HexaColor;
		ChangeSpotSize = _SpotSize;

		SCShader = Shader.Find("CameraFilterPack/AAA_Super_Hexagon");

		if(!SystemInfo.supportsImageEffects)
		{
			enabled = false;
			return;
		}
	}
	
	void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if(SCShader != null)
		{
			TimeX+=Time.deltaTime;
			if (TimeX>100)  TimeX=0;
			material.SetFloat("_TimeX", TimeX);
			material.SetFloat("_Value", HexaSize);
			material.SetFloat("_PositionX", center.x);
			material.SetFloat("_PositionY", center.y);
			material.SetFloat("_Radius", Radius);
			material.SetFloat("_BorderSize", _BorderSize);
			material.SetColor("_BorderColor", _BorderColor);
			material.SetColor("_HexaColor", _HexaColor);
			material.SetFloat("_AlphaHexa", _AlphaHexa);
			material.SetFloat("_SpotSize", _SpotSize);
			material.SetVector("_ScreenResolution",new Vector4(sourceTexture.width,sourceTexture.height,0.0f,0.0f));
			Graphics.Blit(sourceTexture, destTexture, material);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);	
		}

	}
	void OnValidate()
{
		ChangeValue=HexaSize;
		Changecenter=center;
		ChangeRadius=Radius;
		ChangeBorderSize=_BorderSize;
		ChangeBorderColor=_BorderColor;
		ChangeHexaColor=_HexaColor;
		ChangeSpotSize=_SpotSize;
		ChangeAlphaHexa=_AlphaHexa;
}
	// Update is called once per frame
	void Update () 
	{
		if (Application.isPlaying)
		{
			HexaSize = ChangeValue;
			center = Changecenter;
			Radius = ChangeRadius;
			_BorderSize = ChangeBorderSize;
			_BorderColor = ChangeBorderColor;
			_HexaColor = ChangeHexaColor;
			_SpotSize = ChangeSpotSize;
			_AlphaHexa = ChangeAlphaHexa;
		}
		#if UNITY_EDITOR
		if (Application.isPlaying!=true)
		{
			SCShader = Shader.Find("CameraFilterPack/AAA_Super_Hexagon");
		}
		#endif

	}
	
	void OnDisable ()
	{
		if(SCMaterial)
		{
			DestroyImmediate(SCMaterial);	
		}
	}

}