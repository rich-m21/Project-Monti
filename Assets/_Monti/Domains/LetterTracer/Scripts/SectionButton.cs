using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Monti
{
    public class SectionButton : MonoBehaviour
    {
        [SerializeField] GameObject isCompleted = null;
        [SerializeField] Color completedColor;
        [SerializeField] Color pendingColor;
        [SerializeField] Image image;
        [SerializeField] TMP_Text sectionText;
        [SerializeField] Button button;

        public void SetCompleted(bool value)
        {
            isCompleted.SetActive(value);
            if (value)
            {
                image.color = completedColor;
            }
            else
            {
                image.color = pendingColor;
            }
        }

        public Button GetButton()
        {
            return button;
        }

        public void SetInteractible(bool value)
        {
            button.interactable = value;
        }

        public void SetSectionText(string value)
        {
            sectionText.text = value;
        }

    }
}
