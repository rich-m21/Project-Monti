using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monti
{
    [CreateAssetMenu(fileName = "New Gesture Data", menuName = "ScriptableObjects/GestureData", order = 2)]
    public class GestureData : ScriptableObject
    {
        public List<Vector3> RecogPoints;
        public List<Vector2> StrokePoints;
    }
}