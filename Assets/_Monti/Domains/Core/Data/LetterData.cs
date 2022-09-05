using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monti
{
    [CreateAssetMenu(fileName = "New Letter Data", menuName = "ScriptableObjects/Letter", order = 1)]
    public class LetterData : ScriptableObject
    {
        public string letterName;
        public Texture2D texture;

    }
}