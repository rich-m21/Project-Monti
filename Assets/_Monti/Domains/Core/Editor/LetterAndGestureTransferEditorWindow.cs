#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Monti
{
    public class LetterAndGestureTransferEditorWindow : OdinEditorWindow
    {
        [MenuItem("Monti/Letter And Gesture Transfer Tools &L")]
        static void OpenWindow()
        {
            LetterAndGestureTransferEditorWindow window = GetWindow<LetterAndGestureTransferEditorWindow>();

            // Nifty little trick to quickly position the window in the middle of the editor.
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);
        }
        [Header("Letters Data Original"), SerializeField, BoxGroup("Letters")] LetterData[] _letterDataToTransfer = new LetterData[0];
        [SerializeField, BoxGroup("Letters")] Letter_So[] _letterDestination = new Letter_So[0];
        [Button("Transfer Letters"), BoxGroup("Letters")]
        void TransferLetterData()
        {
            if(_letterDataToTransfer.Length > 0 && _letterDataToTransfer.Length == _letterDestination.Length)
            {
                for(int i = 0; i < _letterDataToTransfer.Length; i++)
                {
                    _letterDestination[i].TransferData(_letterDataToTransfer[i].letterName, _letterDataToTransfer[i].texture);
                    EditorUtility.SetDirty(_letterDestination[i]);
                }
            }
        }
        
        
        [Header("Gestures Data Original"), SerializeField, BoxGroup("Gestures")] GestureData[] _gestureDataToTransfer = new GestureData[0];
        [SerializeField, BoxGroup("Gestures")] Gesture_So[] _gestureDestination = new Gesture_So[0];

        [Button("Transfer Gestures"), BoxGroup("Gestures")]
        void TransferGestureData()
        {
            if(_gestureDataToTransfer.Length > 0 && _gestureDataToTransfer.Length == _gestureDestination.Length)
            {
                for(int i = 0; i < _gestureDataToTransfer.Length; i++)
                {
                    _gestureDestination[i].TransferData(_gestureDataToTransfer[i].RecogPoints, _gestureDataToTransfer[i].StrokePoints);
                    EditorUtility.SetDirty(_gestureDestination[i]);
                }
            }
        }
    }
}
#endif
