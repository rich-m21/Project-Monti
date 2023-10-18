using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monti
{
    [CreateAssetMenu(fileName = "Letter_", menuName = "Monti/Letter_So", order = 1)]
    public class Letter_So : ScriptableObject
    {
        [field:SerializeField] public string LetterName{ get; private set;} = "";
        [field:SerializeField] public Texture2D Texture{ get; private set;} = null;
        [field:SerializeField] public Texture2D TextureMask{ get; private set;} = null;

#if UNITY_EDITOR
        public void TransferData(string letterName, Texture2D texture2D)
        {
            LetterName = letterName;
            Texture = texture2D;
        }
#endif
    }
}
