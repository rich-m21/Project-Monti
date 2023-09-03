#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace VR_RTS
{
    public class BatchRename : ScriptableWizard
    {

        /// <summary>
        /// Base name
        /// </summary>
        public string BaseName = "MyObject_";

        /// <summary>
        /// Start count
        /// </summary>
        public int StartNumber = 1;

        /// <summary>
        /// Increment
        /// </summary>
        public int Increment = 1;

        [MenuItem("Custom/Batch Rename %F2")]
        static void CreateWizard()
        {
            DisplayWizard("Batch Rename", typeof(BatchRename), "Rename");
        }

        /// <summary>
        /// Called when the window first appears
        /// </summary>
        void OnEnable()
        {
            UpdateSelectionHelper();
        }

        /// <summary>
        /// Function called when selection changes in scene
        /// </summary>
        void OnSelectionChange()
        {
            UpdateSelectionHelper();
        }

        /// <summary>
        /// Update selection counter
        /// </summary>
        void UpdateSelectionHelper()
        {

            helpString = "";

            if(Selection.objects != null) helpString = "Number of objects selected: " + Selection.objects.Length;
        }


        /// <summary>
        /// Rename
        /// </summary>
        void OnWizardCreate()
        {

            // If selection is empty, then exit
            if(Selection.objects == null) return;

            // Current Increment
            int PostFix = StartNumber;

            List<GameObject> mySelection = new List<GameObject>(Selection.gameObjects);
            mySelection.Sort((go1, go2) => go1.transform.GetSiblingIndex().CompareTo(go2.transform.GetSiblingIndex()));

            foreach(var O in mySelection)
            {
                O.name = BaseName + PostFix;
                PostFix += Increment;
            }
        }
    }
}
#endif