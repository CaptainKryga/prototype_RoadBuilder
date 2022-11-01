using System;
using UnityEngine;

namespace Utils
{
    public class QSettings : MonoBehaviour
    {
        private void Awake()
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
        }
    }
}
