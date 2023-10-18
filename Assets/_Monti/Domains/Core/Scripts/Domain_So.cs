using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monti
{
    [CreateAssetMenu(fileName = "Domain_", menuName = "Monti/Domain/Domain", order = 1)]
    public class Domain_So : ScriptableObject
    {
        [field: SerializeField] public Domain_So ParentDomain{ get; private set; } = null;
        [field: SerializeField] public bool Enabled{ get; private set; } = false;
        
        [field: SerializeField] public bool ShowInMenu{ get; private set; } = false;
        [field: SerializeField] public string ReferenceName{ get; private set; } = "";
        [field: SerializeField] public string DisplayName{ get; private set; } = "";
        [field: SerializeField] public Sprite Icon{ get; private set; } = null;
        [field: SerializeField] public GameObject Prefab{ get; private set; } = null;
        [field: SerializeField] public Section_So[] Sections{ get; private set; } = new Section_So[0];
        [field:SerializeField] public string SectionIndexPlayerPrefKey{ get; private set; } = "";
    }
    
    
}
