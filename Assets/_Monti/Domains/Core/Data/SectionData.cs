using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monti
{
    [CreateAssetMenu(fileName = "New Section Data", menuName = "ScriptableObjects/Domain/Section", order = 1)]
    public class SectionData : ScriptableObject
    {
        public int section;
        public LetterData[] letters;
        public GestureData[] tracers;
        public float time;
    }
}