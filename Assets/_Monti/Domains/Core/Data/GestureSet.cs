using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Monti
{
    [CreateAssetMenu(fileName = "New Gesture Set", menuName = "ScriptableObjects/GestureSet", order = 2)]
    public class GestureSet : ScriptableObject
    {
        public List<GestureData> gesturesData = new List<GestureData>();

        public void Add(string gestureName, Vector3[] points)
        {
            foreach (GestureData gesture in gesturesData)
            {
                if (gesture.name.Equals(gestureName))
                {
                    gesture.RecogPoints.Clear();
                    foreach (Vector3 v in points)
                    {
                        gesture.RecogPoints.Add(v);
                    }
#if UNITY_EDITOR
                    EditorUtility.SetDirty(gesture);
#endif
                }
            }
        }
        public void Add(string gestureName, Vector2[] points)
        {
            foreach (GestureData gesture in gesturesData)
            {
                if (gesture.name.Equals(gestureName))
                {
                    gesture.StrokePoints.Clear();
                    foreach (Vector2 v in points)
                    {
                        gesture.StrokePoints.Add(v);
                    }
#if UNITY_EDITOR
                    EditorUtility.SetDirty(gesture);
#endif
                }
            }
        }

        public void Remove(string gestureName)
        {
            foreach (GestureData gesture in gesturesData)
            {
                if (gesture.name.Equals(gestureName))
                {
                    gesture.RecogPoints.Clear();
                    gesture.StrokePoints.Clear();
                }
#if UNITY_EDITOR
                EditorUtility.SetDirty(gesture);
#endif
            }

        }


        public void RemoveAll()
        {
            foreach (GestureData gesture in gesturesData)
            {
                gesture.RecogPoints.Clear();
                gesture.StrokePoints.Clear();
#if UNITY_EDITOR
                EditorUtility.SetDirty(gesture);
#endif
            }
        }
    }
}
