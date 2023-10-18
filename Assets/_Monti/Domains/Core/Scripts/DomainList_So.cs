using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Monti
{
    [CreateAssetMenu(fileName = "DomainList_", menuName = "Monti/Domain/Domain List", order = 0)]
    public class DomainList_So : ScriptableObject
    {
        [field: SerializeField] public string SetList{ get; private set; } = "";
        
        [field: SerializeField] public Domain_So MainDomain{ get; private set; } = null;
        
        [field: SerializeField] public Domain_So[] Domains{ get; private set; } = new Domain_So[0];
        
        public bool GetDomainPrefab(string domainName, out GameObject prefab)
        {
            if(domainName.Equals(MainDomain.ReferenceName))
            {
                prefab = MainDomain.Prefab;
                return true;
            }
            
            for(int i = 0; i < Domains.Length; i++)
            {
                if(domainName.Equals(Domains[i].ReferenceName))
                {
                    prefab = Domains[i].Prefab;
                    return true;
                }
            }
            prefab = null;
            return false;
        }
    }
}
