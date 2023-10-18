using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monti
{
    public class Section_So : ScriptableObject
    {
        [field:SerializeField] public string DisplayName{ get; private set;} = "";
        [field:SerializeField] public int SectionIndex{ get; private set;} = -1;
    }
}
