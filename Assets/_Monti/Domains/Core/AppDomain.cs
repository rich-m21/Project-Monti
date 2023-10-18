using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Monti
{
    public class AppDomain : MonoBehaviour
    {
        [SerializeField] string endpointUrl = string.Empty;
        [SerializeField] RectTransform domainPanel = null;

        public virtual void PrepareForDestroy(Action success = null, Action fail = null) { }

        public virtual void PrepareForLoad(Action success = null, Action fail = null) { }
        public virtual void Activate() { }

        public void AnimateOut()
        {
            if (domainPanel != null)
                domainPanel.DOAnchorPos(new Vector2(0f, -domainPanel.rect.height), .25f);
        }

        public void AnimateIn()
        {
            if (domainPanel != null)
                domainPanel.DOAnchorPos(new Vector2(0f, 0f), .25f);
        }

        private void Awake()
        {
            if (domainPanel != null)
                domainPanel.anchoredPosition = new Vector2(0f, -domainPanel.rect.height);
        }

        

    }
}
