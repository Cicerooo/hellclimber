using System;
using UnityEngine;

namespace Player
{
    public class TimeManager : MonoBehaviour
    {
        private float elTime = 0;
        private bool doTimer = true;
        private void Start()
        {
            
            elTime = 0;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (doTimer)
            {
                elTime += Time.deltaTime;
                PlayerPrefs.SetFloat("time",elTime);
            }
        }

        public void StopTimer()
        {
            doTimer = false;
        }

        public void StartTimer()
        {
            doTimer = true;
            elTime = 0;
        }

        public void ResumeTimer()
        {
            doTimer = true;
        }
    }
}