using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monti
{
    [CreateAssetMenu(fileName = "New Domain Data", menuName = "ScriptableObjects/Domain/Create Domain", order = 1)]
    public class DomainData : ScriptableObject
    {
        public bool showInMenu;
        public string DomainName;
        public GameObject DomainPrefab;
        public ModuleData module;
    }
}
