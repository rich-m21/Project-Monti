using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Monti
{
    public class MainMenu : AppDomain
    {
        [SerializeField]
        DomainListData _domains = null;

        #region Home Panel
        [SerializeField, Header("Home Screen"), Required]
        Button _startButton = null;

        [SerializeField, Required]
        GameObject _startPanel = null;
        
        #endregion
        

        #region Registration Panel
        [SerializeField, Header("Registration Screen"), Required]
        GameObject _registrationPanel = null;

        [SerializeField, Required]
        TMP_InputField _emailInput = null;

        [SerializeField, Required]
        TMP_InputField _passwordInput = null;

        #endregion

        #region Modules
        [SerializeField, Header("Modules Screen"), Required]
        GameObject _modulesPanel = null;

        [SerializeField]
        Button[] _buttonsOptionSet = new Button[0];
        
        [SerializeField]
        ModuleData[] _modules = new ModuleData[0];

        #endregion


        public override void PrepareForLoad(Action success = null, Action fail = null)
        {
            _startButton.onClick.AddListener(
                    () =>
                    {
                        _startPanel.SetActive(false);
                        if(string.IsNullOrEmpty(PlayerPrefs.GetString("token", "")))
                        {
                            _registrationPanel.SetActive(true);
                        }
                        else
                        {
                            _modulesPanel.SetActive(true);
                        }
                    });

            if(_modules.Length <= _buttonsOptionSet.Length)
            {
                for(int i = 0; i < _modules.Length; i++)
                {
                    
                }
            }
            
            // int pos = 0;
            // foreach (DomainData domain in domains.DomainList)
            // {
            //     if (!domain.showInMenu) continue;
            //     GameObject DomainButton = Instantiate(openButtonPrefab);
            //     DomainButton.transform.SetParent(buttonContent);
            //     Button b = DomainButton.GetComponent<Button>();
            //     b.onClick.AddListener(() =>
            //     {
            //         DomainLoader.instance.SwitchDomain(domain.DomainName);
            //     });
            //     domainButtons.Add(b);
            //     pos++;
            // }
            success();
        }

        public override void PrepareForDestroy(Action success = null, Action fail = null)
        {
            
            success();
        }

    }
}