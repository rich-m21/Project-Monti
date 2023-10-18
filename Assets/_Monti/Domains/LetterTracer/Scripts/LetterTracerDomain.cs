using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Monti
{
    public class LetterTracerDomain : AppDomain
    {
        [SerializeField, BoxGroup("Module Data")] Domain_So _domain = null;

        [SerializeField] Button _goBackButton = null;
        [SerializeField] Button _profileButton = null;

        [SerializeField, BoxGroup("Paint Panel")] GameObject _paintPanel = null;
        [SerializeField, BoxGroup("Paint Panel")] Image _paintPanelImage = null;
        [SerializeField, BoxGroup("Paint Panel")] Material _paintPanelMaterial = null;
        
        [SerializeField, BoxGroup("Saved Session Popup")] GameObject _savedSessionPopUp = null;
        [SerializeField, BoxGroup("Saved Session Popup")] Button _continueButtonSession = null;
        [SerializeField, BoxGroup("Saved Session Popup")] Button _restartButtonSession = null;
        
        [SerializeField, BoxGroup("Big Win Popup")] GameObject _winPopUp = null;
        [SerializeField, BoxGroup("Big Win Popup")] Button _replayButtonWin = null;
        [SerializeField, BoxGroup("Big Win Popup")] Button _nextButtonWin = null;
        
        [SerializeField, BoxGroup("Small Win Popup")] GameObject _winSmallPopUp = null;
        [SerializeField, BoxGroup("Small Win Popup")] Button _replayButtonWinSmall = null;
        [SerializeField, BoxGroup("Small Win Popup")] Button _nextButtonWinSmall = null;
        
        [SerializeField, BoxGroup("Lose Popup")] GameObject _losePopUp = null;
        [SerializeField, BoxGroup("Lose Popup")] Button _replayButtonLose = null;
        [SerializeField, BoxGroup("Lose Popup")] Button _mainMenuButtonLose = null;

        string _painPanelMainMaterial_ID = "_MainTex";
        string _painPanelMaskMaterial_ID = "_MaskTex";

        int _currentSection = 0;
        int _currentLetterIndex = 0;
        float _timer = 0;
        bool _isEnabled = false;

        public override void PrepareForLoad(Action success = null, Action fail = null)
        {
            _paintPanel.SetActive(false);
            _savedSessionPopUp.SetActive(false);
            _winPopUp.SetActive(false);
            _losePopUp.SetActive(false);
            _winSmallPopUp.SetActive(false);
            
            // Add button listeners
            _goBackButton.onClick.AddListener(
                    () =>
                    {
                        DomainLoader.instance.SwitchDomain(_domain.ParentDomain.ReferenceName);
                    });
            
            // Saved Session Popup
            _continueButtonSession.onClick.AddListener(
                    () =>
                    {
                        LoadSection(PlayerPrefs.GetInt(_domain.SectionIndexPlayerPrefKey));
                    });
            
            _restartButtonSession.onClick.AddListener(
                    () =>
                    {
                        LoadSection(0);
                    });
            
            // Big Win Popup
            _replayButtonWin.onClick.AddListener(
                    () =>
                    {
                        
                    });
            
            _nextButtonWin.onClick.AddListener(
                    () =>
                    {
                        
                    });
            
            // Small Win Popup
            _replayButtonWinSmall.onClick.AddListener(
                    () =>
                    {
                        
                    });
            
            _nextButtonWinSmall.onClick.AddListener(
                    () =>
                    {
                        
                    });
            
            // Lose Popup
            _replayButtonLose.onClick.AddListener(
                    () =>
                    {
                        
                    });
            
            _mainMenuButtonLose.onClick.AddListener(
                    () =>
                    {
                        DomainLoader.instance.SwitchDomain(_domain.ParentDomain.ReferenceName);
                    });
            
            /*
             * 1. Find last section completed
             * 2. Ask if you want to continue or start over
             * 3. Load Section from PlayerPrefs and start from first index
             */

            int _savedSection = PlayerPrefs.GetInt(_domain.SectionIndexPlayerPrefKey, -1);

            if(_savedSection != -1)
            {
                // Show saved session popup
                _savedSessionPopUp.SetActive(true);
            }
            else
            {
                LoadSection(0);
            }

            success();
        }

        void LoadSection(int section)
        {
            _paintPanel.SetActive(true);
            _currentLetterIndex = 0;
            _currentSection = section;
            _paintPanelMaterial = _paintPanelImage.material;
            
            Section_LetterTracer_So currentSection = (Section_LetterTracer_So)_domain.Sections[_currentSection];
            
            Letter_So letter = currentSection.Letters[0];
            _paintPanelMaterial.SetTexture(_painPanelMainMaterial_ID, letter.Texture);
            _paintPanelMaterial.SetTexture(_painPanelMainMaterial_ID, letter.TextureMask);
            
            PlayerPrefs.SetInt(_domain.SectionIndexPlayerPrefKey, _currentSection);
        }

        public override void PrepareForDestroy(Action success = null, Action fail = null)
        {
            
            success();
        }

        public override void Activate()
        {

        }

        void HandleOnGestureRecognized(string gesture)
        {
            
        }

        void Update()
        {
            if (_isEnabled)
            {
                _timer += Time.deltaTime;
            }
        }
    }
}
