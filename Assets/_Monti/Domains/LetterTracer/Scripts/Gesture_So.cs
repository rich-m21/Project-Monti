using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monti
{
    [CreateAssetMenu(fileName = "Gesture_", menuName = "Monti/Gesture_So", order = 1)]
    public class Gesture_So : ScriptableObject
    {
        [field:SerializeField] public Vector3[] RecogPoints{ get; private set;} = new Vector3[0];
        [field:SerializeField] public Vector2[] StrokePoints{ get; private set;} = new Vector2[0];
        
#if UNITY_EDITOR
        public void TransferData(List<Vector3> recogPoints, List<Vector2> strokePoints)
        {
            RecogPoints = new Vector3[recogPoints.Count];
            for(int i = 0; i < recogPoints.Count; i++)
            {
                RecogPoints[i] = recogPoints[i];
            }
            
            StrokePoints = new Vector2[strokePoints.Count];
            for(int i = 0; i < strokePoints.Count; i++)
            {
                StrokePoints[i] = strokePoints[i];
            }
        }
#endif
    }
}
