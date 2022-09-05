using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using TMPro;
using System;
using UnityEngine.EventSystems;
namespace Monti
{
    public class LetterTracer : MonoBehaviour, IBeginDragHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private bool isCreationMode = false;
        public GameObject pointPrefab = null;
        [SerializeField] private SectionData sectionData;
        [SerializeField] private float distanceThreshold = 1f;
        [SerializeField] private float timeToDetect = 3f;
        private List<Gesture> trainingSet = new List<Gesture>();
        List<Vector3> positionList = new List<Vector3>();
        List<GameObject> pointsGameObjects = new List<GameObject>();
        public TMP_Dropdown dropdown = null;
        public RectTransform pointsParent = null;
        [SerializeField] int strokeId = 0;
        bool startDetectingCounter = false;
        [SerializeField] bool timerHasExpired = true;
        [SerializeField] List<Point> localGesturePoints = new List<Point>();
        [SerializeField] private float detectTimer = 0f;

        public int StrokeId { get => strokeId; }
        public bool IsCreationMode { get => isCreationMode; }

        public static event Action<string> OnGestureRecognized;
        public static event Action<Vector2> OnDragStartEvent;
        public static event Action<Vector2> OnDragAfterThresholdEvent;
        public static event Action OnGestureAdded;

        public void LoadTrainingSet(SectionData section = null)
        {
            sectionData = section;
            trainingSet.Clear();
            foreach (GestureData g in section.tracers)
            {
                Point[] gesturePoints = new Point[g.RecogPoints.Count];
                if (g.RecogPoints.Count == 0) continue;
                for (int i = 0; i < g.RecogPoints.Count; i++)
                {
                    gesturePoints[i] = new Point(g.RecogPoints[i].x, g.RecogPoints[i].y, (int)g.RecogPoints[i].z);
                }
                Gesture trainingGesture = new Gesture(gesturePoints);
                trainingGesture.Name = g.name;
                trainingSet.Add(trainingGesture);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (isCreationMode)
            {
                Vector2 point = new Vector2(eventData.position.x, eventData.position.y);
                OnDragStartEvent?.Invoke(point);
            }
            startDetectingCounter = false;
            detectTimer = timeToDetect;
            Point newPoint = new Point(eventData.position.x, eventData.position.y, strokeId);
            localGesturePoints.Add(newPoint);
            positionList.Add(new Vector3(eventData.position.x, eventData.position.y, strokeId));
            if (pointPrefab)
            {
                pointsGameObjects.Add(GameObject.Instantiate(pointPrefab, eventData.position, Quaternion.identity, pointsParent));
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 v2 = new Vector2(localGesturePoints[localGesturePoints.Count - 1].X, localGesturePoints[localGesturePoints.Count - 1].Y);
            float distance = Vector2.Distance(eventData.position, v2);
            if (distance > distanceThreshold)
            {
                if (isCreationMode)
                {
                    Vector2 point = new Vector2(eventData.position.x, eventData.position.y);
                    OnDragStartEvent?.Invoke(point);
                }
                Point newPoint = new Point(eventData.position.x, eventData.position.y, strokeId);
                localGesturePoints.Add(newPoint);
                positionList.Add(new Vector3(eventData.position.x, eventData.position.y, strokeId));
                if (pointPrefab)
                    pointsGameObjects.Add(GameObject.Instantiate(pointPrefab, eventData.position, Quaternion.identity, pointsParent));
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            startDetectingCounter = true;
            ++strokeId;
        }

        private void Update()
        {
            if (startDetectingCounter)
            {
                if (detectTimer <= 0)
                {
                    startDetectingCounter = false;

                    timerHasExpired = false;
                    detectTimer = timeToDetect;
                    //Detect gesture
                    Gesture newGesture = new Gesture(localGesturePoints.ToArray());
                    if (isCreationMode)
                    {
                        OnGestureAdded?.Invoke();
                        // section.Add(dropdown.options[dropdown.value].text, positionList.ToArray());
                        LoadTrainingSet();
                        Debug.Log($"<color=green>Added:</color>{dropdown.options[dropdown.value].text}");
                    }
                    else
                    {
                        Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
                        Debug.Log($"<color=green>Result:</color>{result.GestureClass}");
                        if (result.Score > .3f)
                        {
                            // Debug.Log($"<color=green>Result:</color>{result.GestureClass}");
                            OnGestureRecognized?.Invoke(result.GestureClass);
                        }
                    }
                    localGesturePoints.Clear();
                    positionList.Clear();
                    strokeId = 0;
                    return;
                }
                detectTimer -= Time.deltaTime;
            }
        }

        public void ToggleCreationMode()
        {
            isCreationMode = !isCreationMode;
        }


    }
}

