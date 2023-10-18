using System.Collections;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Monti
{
    public class Ui_Button_Module : MonoBehaviour
    {
        public Button Button = null;
        
        [SerializeField] GameObject _rootEnabled = null;

        [SerializeField] Image _imageEnabled = null;
        
        [SerializeField] TMP_Text _labelEnabled = null;
        
        [SerializeField] GameObject _rootDisabled = null;
        
        [SerializeField] Image _imageDisabled = null;
        
        [SerializeField] TMP_Text _labelDisabled = null;

        public void SetButtonData(string displayName, bool isEnabled, Sprite icon)
        {
            ApplyEnabledStatus(isEnabled);
            _labelEnabled.text = displayName;
            _labelDisabled.text = displayName;
            _imageDisabled.sprite = icon;
            _imageEnabled.sprite = icon;
        }

        public void ApplyEnabledStatus(bool isEnabled)
        {
            _rootEnabled.SetActive(isEnabled);
            _rootDisabled.SetActive(!isEnabled);
        }
    }
}
