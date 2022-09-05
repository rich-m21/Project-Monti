using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monti
{
    [CreateAssetMenu(fileName = "New Domain List Data", menuName = "ScriptableObjects/Domain/Create Domain List", order = 1)]
    public class DomainListData : ScriptableObject
    {
        public string DomainName;
        public List<DomainData> DomainList;

        public GameObject GetDomainPrefab(string domainName)
        {
            DomainData data = DomainList.Find(x => x.DomainName.Equals(domainName));
            return data.DomainPrefab;
        }
    }

}
