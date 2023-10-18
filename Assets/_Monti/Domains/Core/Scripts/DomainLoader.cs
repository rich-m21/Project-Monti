using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

namespace Monti
{
    public class DomainLoader : MonoBehaviour
    {
        public static DomainLoader instance;
        [SerializeField] DomainList_So domains = null;
        [SerializeField] AppDomain currentDomain = null;
        [SerializeField] RectTransform loadingWheelPanel = null;
        [SerializeField] RectTransform loadingWheel = null;
        [SerializeField] RectTransform leftPanel = null;
        [SerializeField] RectTransform rightPanel = null;
        [SerializeField] float transitionSpeed = .25f;
        [SerializeField] CanvasGroup fade = null;
        [SerializeField] bool rotate = false;
        [SerializeField] private float rotationSpeed = 5f;
        float angle = 0;
        Vector2 leftAnchorPosition;
        Vector2 rightAnchorPosition;

        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            angle = rotationSpeed * Time.deltaTime;
            leftAnchorPosition = leftPanel.anchoredPosition;
            rightAnchorPosition = rightPanel.anchoredPosition;
            rotate = true;
            SwitchDomain("main-menu");
        }

        public DomainList_So GetDomainList()
        {
            return domains;
        }
        public AppDomain GetCurrentDomain()
        {
            return currentDomain;
        }

        public void SwitchDomain(string newDomain)
        {
            StartCoroutine(SwitchDomainHelper(newDomain));
        }

        IEnumerator SwitchDomainHelper(string newDomain)
        {
            LoaderFadeIn();
            yield return new WaitForSeconds(.5f);
            if (!object.ReferenceEquals(currentDomain, null))
            {
                currentDomain.PrepareForDestroy(() =>
                {
                    Debug.Log($"Destroyed Current Domain");
                    Destroy(currentDomain.gameObject);
                    InstanceDomain(newDomain);
                });
            }
            else
            {
                InstanceDomain(newDomain);
            }
        }

        void InstanceDomain(string name)
        {
            if(domains.GetDomainPrefab(name,out GameObject prefab))
            {
                GameObject newDomainObject = Instantiate(prefab);
                currentDomain = newDomainObject.GetComponent<AppDomain>();

                currentDomain.PrepareForLoad(() =>
                {
                    Debug.Log($"Loaded new Domain");
                    Invoke(nameof(LoaderFadeOut), 2f);
                });    
            }
            else
            {
                Debug.LogError("No Domain with that Name found, please fix");
            }
        }

        void Update()
        {
            if (rotate)
            {
                loadingWheel.Rotate(new Vector3(0, 0, angle), Space.World);
            }
        }

        void LoaderFadeOut()
        {
            fade.alpha = 1;
            loadingWheelPanel.anchoredPosition = new Vector2(0f, 0f);
            leftPanel.anchoredPosition = leftAnchorPosition;
            rightPanel.anchoredPosition = rightAnchorPosition;
            float tweenDistance = -loadingWheelPanel.rect.height / 6f;
            Sequence s = DOTween.Sequence()
            .Insert(0, leftPanel.DOAnchorPos(new Vector2(-loadingWheelPanel.rect.width / 2f, 0f), transitionSpeed))
            .Insert(0, rightPanel.DOAnchorPos(new Vector2(loadingWheelPanel.rect.width, 0f), transitionSpeed))
            .Insert(0, fade.DOFade(0, transitionSpeed))
            .Insert(0, loadingWheelPanel.DOAnchorPos(new Vector2(0, tweenDistance), transitionSpeed))
            .OnComplete(() =>
            {
                rotate = false;
                fade.interactable = false;
                fade.blocksRaycasts = false;
                currentDomain.AnimateIn();
                currentDomain.Activate();
            })
            .SetRecyclable()
            .Play();

        }

        void LoaderFadeIn()
        {
            float tweenDistance = -loadingWheelPanel.rect.height / 6f;
            fade.alpha = 0;
            loadingWheelPanel.anchoredPosition = new Vector2(0f, tweenDistance);

            Sequence s = DOTween.Sequence()
            .Insert(0, leftPanel.DOAnchorPos(leftAnchorPosition, transitionSpeed))
            .Insert(0, rightPanel.DOAnchorPos(rightAnchorPosition, transitionSpeed))
            .Insert(0, loadingWheelPanel.DOAnchorPos(new Vector2(0, 0), transitionSpeed))
            .Insert(0, fade.DOFade(1, transitionSpeed))
            .OnComplete(() =>
            {
                rotate = true;
                loadingWheel.rotation = Quaternion.Euler(0, 0, 0);
                fade.interactable = true;
                fade.blocksRaycasts = true;
            })
            .SetRecyclable()
            .Play();

        }

        void SpinnerWheelStart()
        {

        }
#if UNITY_EDITOR
        [MenuItem("Tools/Rich/FadeLoaderIn")]
        static void EditorFadeIn()
        {
            DomainLoader.instance.LoaderFadeIn();
        }

        [MenuItem("Tools/Rich/FadeLoaderOut")]
        static void EditorFadeOut()
        {
            DomainLoader.instance.LoaderFadeOut();
        }

#endif


    }
}