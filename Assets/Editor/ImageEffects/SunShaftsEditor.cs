using System;
using UnityEditor;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    [CustomEditor (typeof(SunShafts))]
    class SunShaftsEditor : Editor
    {
        SerializedObject serObj;

        SerializedProperty sunTransform;
        SerializedProperty radialBlurIterations;
        SerializedProperty sunColor;
        SerializedProperty sunThreshold;
        SerializedProperty sunShaftBlurRadius;
        SerializedProperty sunShaftIntensity;
        SerializedProperty useDepthTexture;
        SerializedProperty resolution;
        SerializedProperty screenBlendMode;
        SerializedProperty maxRadius;

        void OnEnable () {
            serObj = new SerializedObject (target);

            screenBlendMode = serObj.FindProperty("screenBlendMode");

            sunTransform = serObj.FindProperty("sunTransform");
            sunColor = serObj.FindProperty("sunColor");
            sunThreshold = serObj.FindProperty("sunThreshold");

            sunShaftBlurRadius = serObj.FindProperty("sunShaftBlurRadius");
            radialBlurIterations = serObj.FindProperty("radialBlurIterations");

            sunShaftIntensity = serObj.FindProperty("sunShaftIntensity");

            resolution =  serObj.FindProperty("resolution");

            maxRadius = serObj.FindProperty("maxRadius");

            useDepthTexture = serObj.FindProperty("useDepthTexture");
        }


        public override void OnInspectorGUI () {
            serObj.Update ();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField (useDepthTexture, new GUIContent ("Rely on Z Buffer?"));
            var sunShafts = target as SunShafts;
            if (sunShafts != null && sunShafts.GetComponent<Camera>())
                GUILayout.Label("Current camera mode: "+ ((SunShafts) target).GetComponent<Camera>().depthTextureMode, EditorStyles.miniBoldLabel);

            EditorGUILayout.EndHorizontal();

            // depth buffer need
            /*
		bool  newVal = useDepthTexture.boolValue;
		if (newVal != oldVal) {
			if (newVal)
				(target as SunShafts).camera.depthTextureMode |= DepthTextureMode.Depth;
			else
				(target as SunShafts).camera.depthTextureMode &= ~DepthTextureMode.Depth;
		}
		*/

            EditorGUILayout.PropertyField (resolution,  new GUIContent("Resolution"));
            EditorGUILayout.PropertyField (screenBlendMode, new GUIContent("Blend mode"));

            EditorGUILayout.Separator ();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField (sunTransform, new GUIContent("Shafts caster", "Chose a transform that acts as a root point for the produced sun shafts"));
            var o = target as SunShafts;
            if (o != null && (o.sunTransform && o.GetComponent<Camera>()))
            {
                var shafts = (SunShafts) target;
                if (shafts != null && GUILayout.Button("Center on " + shafts.GetComponent<Camera>().name)) {
                    if (EditorUtility.DisplayDialog ("Move sun shafts source?", "The SunShafts caster named "+ ((SunShafts) target).sunTransform.name +"\n will be centered along "+((SunShafts) target).GetComponent<Camera>().name+". Are you sure? ", "Please do", "Don't")) {
                        Ray ray = ((SunShafts) target).GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f,0.5f,0));
                        ((SunShafts) target).sunTransform.position = ray.origin + ray.direction * 500.0f;
                        ((SunShafts) target).sunTransform.LookAt (((SunShafts) target).transform);
                    }
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator ();

            EditorGUILayout.PropertyField (sunThreshold,  new GUIContent ("Threshold color"));
            EditorGUILayout.PropertyField (sunColor,  new GUIContent ("Shafts color"));
            maxRadius.floatValue = 1.0f - EditorGUILayout.Slider ("Distance falloff", 1.0f - maxRadius.floatValue, 0.1f, 1.0f);

            EditorGUILayout.Separator ();

            sunShaftBlurRadius.floatValue = EditorGUILayout.Slider ("Blur size", sunShaftBlurRadius.floatValue, 1.0f, 10.0f);
            radialBlurIterations.intValue = EditorGUILayout.IntSlider ("Blur iterations", radialBlurIterations.intValue, 1, 3);

            EditorGUILayout.Separator ();

            EditorGUILayout.PropertyField (sunShaftIntensity,  new GUIContent("Intensity"));

            serObj.ApplyModifiedProperties();
        }
    }
}
