using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEditor;

namespace Monti
{
    public class DrawLinesOnTexture : MonoBehaviour, IPointerClickHandler, IDragHandler
    {
        [SerializeField] private SectionData sectionData = null;
        [SerializeField] private RectTransform areaBox = null;
        [SerializeField] private Image drawImage = null;
        [SerializeField] private Image letterMaskImage = null;
        [SerializeField] private Image arrowsImage = null;
        [SerializeField] private int brushSize = 10;
        [SerializeField] private Texture2D brushTexture = null;
        [SerializeField] private Color32 green;
        [SerializeField] private Color32 red;
        [SerializeField] private Color32 alpha;
        [SerializeField] private Color32 brushColor;
        [SerializeField] private Color32 resultSuccess;
        [SerializeField] private Color32 resultError;
        [SerializeField] private LetterTracer letterTracer = null;
        [SerializeField] private TMP_Dropdown dropdown = null;
        [SerializeField] private Texture2D drawTexture = null;
        [SerializeField] private Texture2D letterTexture = null;
        [SerializeField] private Texture2D arrowTexture = null;
        private Color32[] brushArray;
        [SerializeField] private int alphabetPosition = 0;
        private List<Vector2> creationPoints = new List<Vector2>();

        public int AlphabetPosition { get => alphabetPosition; set => alphabetPosition = value; }

        public string GetSelectedWordName()
        {
            return sectionData.letters[alphabetPosition].name;
        }

        private void Awake()
        {
            letterTracer = GetComponent<LetterTracer>();
            drawTexture = drawImage.sprite.texture;
            letterMaskImage.alphaHitTestMinimumThreshold = 1;
            arrowsImage.alphaHitTestMinimumThreshold = 0;

            drawTexture = drawImage.sprite.texture;
            letterTexture = letterMaskImage.sprite.texture;
            arrowTexture = arrowsImage.sprite.texture;
            SetBrushColor();
        }

        private void Start()
        {
            LetterTracer.OnDragStartEvent += HandleOnDragStartEvent;
            LetterTracer.OnDragAfterThresholdEvent += HandleOnDragThresholdEvent;
            LetterTracer.OnGestureAdded += HandleOnGestureAdded;
        }

        private void SetBrushColor()
        {
            brushArray = new Color32[(brushSize * 2) * (brushSize * 2)];
            for (int i = 0; i < brushArray.Length; i++)
            {
                brushArray[i] = brushColor;
            }
        }

        public void SelectLetterDropdown()
        {
            alphabetPosition = dropdown.value;
            SepatarateLetterData();
        }

        public void GetRandomLetter()
        {
            // alphabetPosition = Random.Range(0, alphabet.alphabet.Length);
            SepatarateLetterData();
        }

        public void StartSection(SectionData section)
        {
            sectionData = section;
            alphabetPosition = 0;
            SepatarateLetterData();
        }

        public void NextLetter()
        {
            alphabetPosition++;
            SepatarateLetterData();
        }

        public void SepatarateLetterData()
        {
            Color32[] originalData = sectionData.letters[alphabetPosition].texture.GetPixels32();
            Color32[] drawData = new Color32[originalData.Length];
            Color32[] letterData = new Color32[originalData.Length];
            Color32[] arrowData = new Color32[originalData.Length];
            Color32 resetColor = new Color32(255, 255, 255, 255);
            for (int i = 0; i < originalData.Length; i++)
            {
                drawData[i] = resetColor;
                if (CompareColor(originalData[i], green) || CompareColor(originalData[i], red))
                {
                    arrowData[i] = originalData[i];
                    letterData[i] = alpha;
                }
                else
                {
                    arrowData[i] = alpha;
                    letterData[i] = originalData[i];
                }
            }
            arrowTexture.SetPixels32(arrowData);
            letterTexture.SetPixels32(letterData);
            drawTexture.SetPixels32(drawData);
            arrowTexture.Apply();
            letterTexture.Apply();
            drawTexture.Apply();
        }

        private bool CompareColor(Color32 first, Color32 second)
        {
            if (first.r == second.r && first.g == second.g && first.b == second.b) return true;
            return false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Vector2 point = new Vector2(eventData.position.x, eventData.position.y);
            DetectLocalPointInRectangle(point);
        }

        private void DrawOnTexture(Vector2 point)
        {
            int texturePositionX = (int)((point.x / areaBox.rect.width) * drawTexture.width);
            int texturePositionY = (int)((point.y / areaBox.rect.height) * drawTexture.height);

            if (texturePositionX > drawTexture.width || texturePositionY > drawTexture.height) return;
            if ((texturePositionX - brushSize + (brushSize * 2)) > drawTexture.width) return;
            if ((texturePositionY - brushSize + (brushSize * 2)) > drawTexture.height) return;
            if ((texturePositionX - brushSize) < 0) return;
            if ((texturePositionY - brushSize) < 0) return;
            drawTexture.SetPixels32(texturePositionX - brushSize, texturePositionY - brushSize, brushSize * 2, brushSize * 2, brushArray);
            drawTexture.Apply();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 point = new Vector2(eventData.position.x, eventData.position.y);
            DetectLocalPointInRectangle(point);
        }

        private void HandleOnDragStartEvent(Vector2 point)
        {
            DetectLocalPointInRectangle(point);
        }

        private void HandleOnDragThresholdEvent(Vector2 point)
        {
            DetectLocalPointInRectangle(point);
        }

        private void HandleOnGestureAdded()
        {
            // alphabet.Add(dropdown.options[alphabetPosition].text, creationPoints.ToArray());
            // creationPoints.Clear();
        }

        private void DetectLocalPointInRectangle(Vector2 point)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(areaBox, point, null, out Vector2 localClick))
            {
                localClick.x = (areaBox.rect.xMin * -1) - (localClick.x * -1);
                localClick.y = (areaBox.rect.yMin * -1) - (localClick.y * -1);
                if (letterTracer.IsCreationMode)
                {
                    creationPoints.Add(new Vector2(localClick.x / areaBox.rect.width, localClick.y / areaBox.rect.height));
                }
                DrawOnTexture(localClick);
            }
        }

        public void PhaseOneDrawTexture()
        {
            StartCoroutine(C_DrawStroke());
        }


        WaitForSecondsRealtime wfs = new WaitForSecondsRealtime(.0001f);
        IEnumerator C_DrawStroke()
        {
            foreach (Vector2 point in sectionData.tracers[alphabetPosition].StrokePoints)
            {
                int texturePositionX = (int)((point.x) * drawTexture.width);
                int texturePositionY = (int)((point.y) * drawTexture.height);
                drawTexture.SetPixels32(texturePositionX - brushSize, texturePositionY - brushSize, brushSize * 2, brushSize * 2, brushArray);
                drawTexture.Apply();
                yield return wfs;

            }
            yield return new WaitForSeconds(1);
            SepatarateLetterData();
        }
    }
}

