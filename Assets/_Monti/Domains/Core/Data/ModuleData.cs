using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monti
{
    [CreateAssetMenu(fileName = "New Module Data", menuName = "ScriptableObjects/Domain/Module", order = 0)]
    public class ModuleData : ScriptableObject
    {
        public string moduleName;
        public SectionData[] sections;
        public int currentSection;
    }
}