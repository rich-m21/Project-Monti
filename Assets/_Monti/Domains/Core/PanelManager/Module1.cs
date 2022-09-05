using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monti
{
    public class Module1 : MonoBehaviour
    {
        [SerializeField] private LetterTracer letterTracer = null;
        [SerializeField] private DrawLinesOnTexture drawLinesOnTexture = null;
        [SerializeField] private int turnsForModule = 10;
        [SerializeField] private GameObject winPopUp = null;
        [SerializeField] private AudioSource audioSource = null;
        [SerializeField] private AudioClip winSound = null;
        [SerializeField] private AudioClip hitSound = null;
        [SerializeField] private AudioClip missSound = null;
        private int currentTrivia = 0;
        private float timer = 0;
        private bool isEnabled = false;

        private void OnEnable()
        {
            // letterTracer.LoadTrainingSet();
            isEnabled = true;
        }

        private void OnDisable()
        {
            isEnabled = false;
        }

        private void Start()
        {
            LetterTracer.OnGestureRecognized += HandleOnGestureRecognized;
            RestartModuleButtonEvent();
        }

        private void OnDestroy()
        {
            LetterTracer.OnGestureRecognized -= HandleOnGestureRecognized;
        }

        public void RestartModuleButtonEvent()
        {
            timer = 0;
            currentTrivia = 0;
            drawLinesOnTexture.GetRandomLetter();
            drawLinesOnTexture.PhaseOneDrawTexture();
        }

        private void HandleOnGestureRecognized(string gesture)
        {
            if (drawLinesOnTexture.GetSelectedWordName().Equals(gesture))
            {
                audioSource.PlayOneShot(hitSound);
                drawLinesOnTexture.GetRandomLetter();
                drawLinesOnTexture.PhaseOneDrawTexture();
                if (currentTrivia == turnsForModule)
                {
                    audioSource.PlayOneShot(winSound);
                    winPopUp.SetActive(true);
                    return;
                }

                currentTrivia++;
            }
            else
            {
                audioSource.PlayOneShot(missSound);
                drawLinesOnTexture.SepatarateLetterData();
            }
        }

        private void Update()
        {
            if (isEnabled)
            {
                timer += Time.deltaTime;
            }
        }
    }

}

