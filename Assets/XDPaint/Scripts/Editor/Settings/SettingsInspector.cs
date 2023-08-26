using System.Linq;
using UnityEditor;
using UnityEngine;
using XDPaint.Core;
using XDPaint.Tools;

namespace XDPaint.Editor
{
    [CustomEditor(typeof(Settings))]
    public class SettingsInspector : UnityEditor.Editor
    {
        private SerializedProperty defaultBrushProperty;
        private SerializedProperty defaultCircleBrushProperty;
        private SerializedProperty defaultPatternTextureProperty;
        private SerializedProperty isVRModeProperty;
        private SerializedProperty pressureEnabledProperty;
        private SerializedProperty checkCanvasRaycastsProperty;
        private SerializedProperty useJobsForRaycastsProperty;
        private SerializedProperty brushDuplicatePartWidthProperty;
        private SerializedProperty pixelPerUnitProperty;
        private SerializedProperty containerGameObjectNameProperty;
        
        void OnEnable()
        {
            defaultBrushProperty = serializedObject.FindProperty("DefaultBrush");
            defaultCircleBrushProperty = serializedObject.FindProperty("DefaultCircleBrush");
            defaultPatternTextureProperty = serializedObject.FindProperty("DefaultPatternTexture");
            isVRModeProperty = serializedObject.FindProperty("IsVRMode");
            pressureEnabledProperty = serializedObject.FindProperty("PressureEnabled");
            checkCanvasRaycastsProperty = serializedObject.FindProperty("CheckCanvasRaycasts");
            useJobsForRaycastsProperty = serializedObject.FindProperty("UseJobsForRaycasts");
            brushDuplicatePartWidthProperty = serializedObject.FindProperty("BrushDuplicatePartWidth");
            pixelPerUnitProperty = serializedObject.FindProperty("PixelPerUnit");
            containerGameObjectNameProperty = serializedObject.FindProperty("ContainerGameObjectName");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(defaultBrushProperty, new GUIContent("Default Brush"));
            EditorGUILayout.PropertyField(defaultCircleBrushProperty, new GUIContent("Default Circle Brush"));
            EditorGUILayout.PropertyField(defaultPatternTextureProperty, new GUIContent("Default Pattern Texture"));
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(isVRModeProperty, new GUIContent("Is VR Mode"));
            if (EditorGUI.EndChangeCheck())
            {
                var group = EditorUserBuildSettings.selectedBuildTargetGroup;
                var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
                var allDefines = defines.Split(';').ToList();
                if (isVRModeProperty.boolValue)
                {
                    allDefines.AddRange(Constants.Defines.VREnabled.Except(allDefines));
                }
                else
                {
                    for (var i = allDefines.Count - 1; i >= 0; i--)
                    {
                        if (Constants.Defines.VREnabled.Contains(allDefines[i]))
                        {
                            allDefines.RemoveAt(i);
                        }
                    }
                }
                PlayerSettings.SetScriptingDefineSymbolsForGroup(group, string.Join(";", allDefines.ToArray()));
            }
            EditorGUILayout.PropertyField(pressureEnabledProperty, new GUIContent("Pressure Enabled"));
            EditorGUILayout.PropertyField(checkCanvasRaycastsProperty, new GUIContent("Check Canvas Raycasts"));
            EditorGUILayout.PropertyField(useJobsForRaycastsProperty, new GUIContent("Use Job System for Raycasts"));
#if !BURST
            if (useJobsForRaycastsProperty.boolValue)
            {
                EditorGUILayout.HelpBox("It is recommended to use the Burst package to increase performance. " +
                                        "Please, install the Burst package from Package Manager.", MessageType.Warning);
                if (GUILayout.Button("Open Package Manager", GUILayout.ExpandWidth(true)))
                {
                    UnityEditor.PackageManager.UI.Window.Open("com.unity.burst");
                }
            }
#endif
            EditorGUILayout.PropertyField(brushDuplicatePartWidthProperty, new GUIContent("Brush Duplicate Part Width"));
            EditorGUILayout.PropertyField(pixelPerUnitProperty, new GUIContent("Pixel per Unit"));
            EditorGUILayout.PropertyField(containerGameObjectNameProperty, new GUIContent("Container GameObject Name"));
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.LabelField("Version: 3.0.8 (cd1726b1)");
            EditorGUI.EndDisabledGroup();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}