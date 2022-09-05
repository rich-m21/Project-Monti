using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monti
{
    public class MainMenu : AppDomain
    {
        [SerializeField] DomainListData domains = null;
        [SerializeField] GameObject openButtonPrefab = null;
        [SerializeField] GameObject closedButtonPrefab = null;
        [SerializeField] RectTransform buttonContent = null;
        [SerializeField] List<Button> domainButtons = new List<Button>();


        public override void PrepareForLoad(Action success = null, Action fail = null)
        {
            int pos = 0;
            foreach (DomainData domain in domains.DomainList)
            {
                if (!domain.showInMenu) continue;
                GameObject DomainButton = Instantiate(openButtonPrefab);
                DomainButton.transform.SetParent(buttonContent);
                Button b = DomainButton.GetComponent<Button>();
                b.onClick.AddListener(() =>
                {
                    DomainLoader.instance.SwitchDomain(domain.DomainName);
                });
                domainButtons.Add(b);
                pos++;
            }
            success();
        }

        public override void PrepareForDestroy(Action success = null, Action fail = null)
        {
            foreach (Button domainButton in domainButtons)
            {
                domainButton.onClick.RemoveAllListeners();
            }
            success();
        }

    }
}