using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monti
{
    [CreateAssetMenu(fileName = "Module_", menuName = "Monti/Domain/Module", order = 0)]
    public class Module_So : ScriptableObject
    {
        [field:SerializeField] public string ModuleName{ get; private set;} = "";
        [field:SerializeField] public Section_So[] sections{ get; private set;} = new Section_So[0];
    }
}
