using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monti
{
    [CreateAssetMenu(fileName = "Section_LetterTracer_", menuName = "Monti/Sections/Section_LetterTracer", order = 0)]
    public class Section_LetterTracer_So : Section_So
    {
        [field:SerializeField] public Letter_So[] Letters{ get; private set;} = new Letter_So[0];
        [field:SerializeField] public Gesture_So[] Gestures{ get; private set;} = new Gesture_So[0];
    }
}
