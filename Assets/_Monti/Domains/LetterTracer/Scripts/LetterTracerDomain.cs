using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using XDPaint;
using PDollarGestureRecognizer;
        
namespace Monti
{
    public class LetterTracerDomain : AppDomain
    {
        [SerializeField, Required, BoxGroup("Module Data")] Domain_So _domain = null;

        [SerializeField, Required] Button _goBackButton = null;
        [SerializeField, Required] Button _profileButton = null;

        [SerializeField, Required, BoxGroup("Paint Panel")] GameObject _paintPanel = null;
        [SerializeField, Required, BoxGroup("Paint Panel")] RawImage _paintPanelImage = null;

        [SerializeField, Required] GameObject _background = null;
        
        [SerializeField, Required, BoxGroup("Saved Session Popup")] GameObject _savedSessionPopUp = null;
        [SerializeField, Required, BoxGroup("Saved Session Popup")] Button _continueButtonSession = null;
        [SerializeField, Required, BoxGroup("Saved Session Popup")] Button _restartButtonSession = null;
        
        [SerializeField, Required, BoxGroup("Big Win Popup")] GameObject _winPopUp = null;
        [SerializeField, Required, BoxGroup("Big Win Popup")] Button _replayButtonWin = null;
        [SerializeField, Required, BoxGroup("Big Win Popup")] Button _nextButtonWin = null;
        
        [SerializeField, Required, BoxGroup("Small Win Popup")] GameObject _winSmallPopUp = null;
        [SerializeField, Required, BoxGroup("Small Win Popup")] Button _replayButtonWinSmall = null;
        [SerializeField, Required, BoxGroup("Small Win Popup")] Button _nextButtonWinSmall = null;
        
        [SerializeField, Required, BoxGroup("Lose Popup")] GameObject _losePopUp = null;
        [SerializeField, Required, BoxGroup("Lose Popup")] Button _replayButtonLose = null;
        [SerializeField, Required, BoxGroup("Lose Popup")] Button _mainMenuButtonLose = null;

        [SerializeField, Required, BoxGroup("Red Curtain")] RectTransform _curtainObject = null;
        [SerializeField, Required, BoxGroup("Red Curtain")] RectTransform _outPoint = null;
        [SerializeField, Required, BoxGroup("Red Curtain")] RectTransform _inPoint = null;
        [SerializeField, Required, BoxGroup("Red Curtain")] float _transitionSpeed = 0.0f;

        [SerializeField, Required, BoxGroup("Paint Manager")] PaintManager _paintManager = null;

        [SerializeField, BoxGroup("PDollar")] List<Gesture> _trainingSet = null;
        [SerializeField, BoxGroup("PDollar")] List<Point> _points = null;

        Material _paintPanelMaterial = null;
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
                        _background.SetActive(false);
                        _savedSessionPopUp.SetActive(false);
                    });
            
            _restartButtonSession.onClick.AddListener(
                    () =>
                    {
                        LoadSection(0);
                        _background.SetActive(false);
                        _savedSessionPopUp.SetActive(false);
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
                _background.SetActive(true);
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
            
            Sequence s = DOTween.Sequence()
                    .Insert(0, _curtainObject.DOAnchorPos(_outPoint.anchoredPosition, _transitionSpeed))
                    .OnComplete(() =>
                    {
                        // Draw Trace mode
                    })
                    .SetRecyclable()
                    .Play();
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
