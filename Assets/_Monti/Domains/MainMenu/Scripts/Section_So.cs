using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monti
{
    [CreateAssetMenu(fileName = "Section_", menuName = "Monti/Domain/Section", order = 0)]
    public class Section_So : ScriptableObject
    {
        [field:SerializeField]public int Section{ get; private set;} = -1;
        [field:SerializeField]public LetterData[] Letters{ get; private set;} = new LetterData[0];
        [field:SerializeField]public GestureData[] Tracers{ get; private set;} = new GestureData[0];
    }
}
