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
        Ui_Button_Module[] _buttonsOptionSet = new Ui_Button_Module[0];

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

            DomainList_So domainList = DomainLoader.instance.GetDomainList();
            
            if(domainList.Domains.Length <= _buttonsOptionSet.Length)
            {
                for(int i = 0; i < domainList.Domains.Length; i++)
                {
                    int index = i;
                    if(domainList.Domains[i].ShowInMenu)
                    {
                        _buttonsOptionSet[i].SetButtonData(domainList.Domains[i].DisplayName, domainList.Domains[i].Enabled, domainList.Domains[i].Icon);
                        _buttonsOptionSet[i].Button.onClick.AddListener(
                                () =>
                                {
                                    if(domainList.Domains[index].Enabled)
                                    {
                                        DomainLoader.instance.SwitchDomain(domainList.Domains[index].ReferenceName);
                                    }
                                });   
                    }
                }
            }
            success();
        }

        public override void PrepareForDestroy(Action success = null, Action fail = null)
        {
            
            success();
        }

    }
}