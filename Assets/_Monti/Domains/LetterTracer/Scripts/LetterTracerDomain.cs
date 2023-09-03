using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monti
{
    public class LetterTracerDomain : AppDomain
    {
        [SerializeField] DomainData domainData = null;
        [SerializeField] Button sectionButtonOpenPrefab = null;
        [SerializeField] LetterTracer letterTracer = null;
        [SerializeField] DrawLinesOnTexture drawLinesOnTexture = null;
        [SerializeField] Button playButton = null;
        [SerializeField] AudioSource audioSource = null;
        [SerializeField] AudioClip winSound = null;
        [SerializeField] AudioClip hitSound = null;
        [SerializeField] AudioClip missSound = null;
        [SerializeField] Button[] GoBackButtons = null;
        [SerializeField] Transform sectionContainer = null;
        [SerializeField] List<Button> sectionButtons = new List<Button>();
        [SerializeField] RectTransform interactionPanel = null;
        [SerializeField] RectTransform stagePanel = null;
        [SerializeField] Button resetButton = null;

        [Header("Win Pop-up")]
        [SerializeField] GameObject winPopUp = null;
        [SerializeField] Button replayButton = null;
        [SerializeField] Button nextButton = null;

        int currentSection = 0;
        int currentTrivia = 0;
        float timer = 0;
        bool isEnabled = false;

        public override void PrepareForLoad(Action success = null, Action fail = null)
        {
            // currentSection = domainData.module.currentSection;
            // LetterTracer.OnGestureRecognized += HandleOnGestureRecognized;
            // foreach (Button GoBackButton in GoBackButtons)
            // {
            //     GoBackButton.onClick.AddListener(() =>
            // {
            //     DomainLoader.instance.SwitchDomain("main-menu-domain");
            // });
            //
            // }
            // playButton.onClick.AddListener(() =>
            // {
            //     if (currentSection == domainData.module.currentSection && currentSection != 0)
            //         currentSection--;
            //     interactionPanel.gameObject.SetActive(true);
            //     stagePanel.gameObject.SetActive(false);
            //     drawLinesOnTexture.StartSection(domainData.module.sections[currentSection]);
            //     letterTracer.LoadTrainingSet(domainData.module.sections[currentSection]);
            //     drawLinesOnTexture.PhaseOneDrawTexture();
            // });
            //
            // resetButton.onClick.AddListener(() =>
            // {
            //     currentSection = 0;
            //     domainData.module.currentSection = 0;
            //     foreach (Button button in sectionButtons)
            //     {
            //         Destroy(button.gameObject);
            //     }
            //     SetupModuleWithSectionButtons();
            // });
            //
            // replayButton.onClick.AddListener(() =>
            // {
            //     currentSection--;
            //     LoadInteraction();
            // });
            //
            // nextButton.onClick.AddListener(() =>
            // {
            //     LoadInteraction();
            // });
            // SetupModuleWithSectionButtons();

            success();
        }

        void LoadInteraction()
        {
            winPopUp.SetActive(false);
            drawLinesOnTexture.StartSection(domainData.module.sections[currentSection]);
            letterTracer.LoadTrainingSet(domainData.module.sections[currentSection]);
            drawLinesOnTexture.PhaseOneDrawTexture();
        }

        public void SetupModuleWithSectionButtons()
        {
            for (int i = 0; i < domainData.module.sections.Length; i++)
            {
                GameObject b = Instantiate(sectionButtonOpenPrefab.gameObject, sectionContainer);
                SectionButton bScript = b.GetComponent<SectionButton>();
                bScript.SetSectionText($"{i + 1}");
                if ((i + 1) > domainData.module.currentSection)
                {
                    bScript.SetInteractible(false);
                    bScript.SetCompleted(false);
                }
                else
                {
                    bScript.SetInteractible(false);
                    bScript.SetCompleted(true);
                }
                // bScript.GetButton().onClick.AddListener(() =>
                // {

                // });
                sectionButtons.Add(bScript.GetButton());
            }
        }

        public override void PrepareForDestroy(Action success = null, Action fail = null)
        {
            LetterTracer.OnGestureRecognized -= HandleOnGestureRecognized;
            foreach (Button GoBackButton in GoBackButtons)
            {
                GoBackButton.onClick.RemoveAllListeners();
            }
            playButton.onClick.RemoveAllListeners();

            replayButton.onClick.RemoveAllListeners();

            nextButton.onClick.RemoveAllListeners();
            success();
        }

        public override void Activate()
        {

        }

        void HandleOnGestureRecognized(string gesture)
        {
            // if (drawLinesOnTexture.GetSelectedWordName().Equals(gesture))
            // {
            //
            //     if (currentTrivia == domainData.module.sections[currentSection].letters.Length - 1)
            //     {
            //         if (currentSection == domainData.module.currentSection)
            //         {
            //             sectionButtons[domainData.module.currentSection].GetComponent<SectionButton>().SetCompleted(true);
            //             domainData.module.currentSection++;
            //         }
            //         currentSection++;
            //
            //         if (currentSection == domainData.module.sections.Length)
            //         {
            //             nextButton.onClick.RemoveAllListeners();
            //             nextButton.gameObject.SetActive(false);
            //         }
            //         audioSource.PlayOneShot(winSound);
            //         winPopUp.SetActive(true);
            //         currentTrivia = 0;
            //         return;
            //     }
            //     audioSource.PlayOneShot(hitSound);
            //     drawLinesOnTexture.NextLetter();
            //     drawLinesOnTexture.PhaseOneDrawTexture();
            //
            //     currentTrivia++;
            // }
            // else
            // {
            //     audioSource.PlayOneShot(missSound);
            //     drawLinesOnTexture.SepatarateLetterData();
            // }
        }

        void Update()
        {
            if (isEnabled)
            {
                timer += Time.deltaTime;
            }
        }
    }
}
