using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Monti
{
    public class LoggerInterceptor : MonoBehaviour
    {
        public TMP_Text logText = null;
        void Start()
        {
            Application.logMessageReceivedThreaded += HandleLogMessageReceived;
        }

        private void HandleLogMessageReceived(string condition, string stackTrace, LogType type)
        {
            if (type == LogType.Error || type == LogType.Exception)
            {
                logText.text += $"<color=red>{type.ToString()}:</color>\n {stackTrace} \n";
            }
            else
            {
                logText.text += $"{type.ToString()}:\n {condition} \n";
            }
        }

        public void ClearLog()
        {
            logText.text = "";
        }
    }
}

