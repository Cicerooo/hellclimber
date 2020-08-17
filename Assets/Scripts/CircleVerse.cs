using System;
using UnityEngine;

namespace Player
{
    public class CircleVerse : MonoBehaviour
    {
        private Movement _movement;
        public bool loadingFinished = false; 
        private TimeManager tim;        
        private void Start()
        {
            _movement = FindObjectOfType<Movement>();
            _movement.DisableMovement();
            tim = FindObjectOfType<TimeManager>();
            if (tim)
            {
                tim.StopTimer();
            }
        }

        public void FinishVerse()
        {
            _movement.EnableMovement();
            loadingFinished = true;
            tim.ResumeTimer();
        }
    }
}