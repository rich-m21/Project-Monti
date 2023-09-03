using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monti
{
    [CreateAssetMenu(fileName = "New Module Data", menuName = "ScriptableObjects/Domain/Module", order = 0)]
    public class ModuleData : ScriptableObject
    {
        [field:SerializeField] public string ModuleName{ get; private set;} = "";
        [field:SerializeField] public SectionData[] sections{ get; private set;} = new SectionData[0];
        [field:SerializeField] public int currentSection{ get; private set;} = -1;
    }
}