///////////////////////////////////////////
//  CameraFilterPack v2.0 - by VETASOFT 2015 ///
///////////////////////////////////////////
using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
[AddComponentMenu ("Camera Filter Pack/VHS/VHS_Rewind")]
public class CameraFilterPack_TV_VHS_Rewind : MonoBehaviour {
#region Variables
public Shader SCShader;
private float TimeX = 1.0f;
private Material SCMaterial;
[Range(0f, 1f)]
public float Cryptage = 1f;
[Range(-20f, 20f)]
public float Parasite = 9f;
[Range(-20f, 20f)]
public float Parasite2 = 12f;
[Range(0f, 1f)]
private float WhiteParasite = 1f;
public static float ChangeValue;
public static float ChangeValue2;
public static float ChangeValue3;
public static float ChangeValue4;
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
ChangeValue = Cryptage;
ChangeValue2 = Parasite;
ChangeValue3 = Parasite2;
ChangeValue4 = WhiteParasite;
SCShader = Shader.Find("CameraFilterPack/TV_VHS_Rewind");
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
material.SetFloat("_Value", Cryptage);
material.SetFloat("_Value2", Parasite);
material.SetFloat("_Value3", Parasite2);
material.SetFloat("_Value4", WhiteParasite);
material.SetVector("_ScreenResolution",new Vector4(sourceTexture.width,sourceTexture.height,0.0f,0.0f));
Graphics.Blit(sourceTexture, destTexture, material);
}
else
{
Graphics.Blit(sourceTexture, destTexture);
}
}
void OnValidate(){ChangeValue=Cryptage;ChangeValue2=Parasite;ChangeValue3=Parasite2;ChangeValue4=WhiteParasite;}void Update ()
{
if (Application.isPlaying)
{
Cryptage = ChangeValue;
Parasite = ChangeValue2;
Parasite2 = ChangeValue3;
WhiteParasite = ChangeValue4;
}
#if UNITY_EDITOR
if (Application.isPlaying!=true)
{
SCShader = Shader.Find("CameraFilterPack/TV_VHS_Rewind");
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
