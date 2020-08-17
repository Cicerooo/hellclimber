using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

namespace Player.Obstacles
{
    public class FinishLine : MonoBehaviour
    {
        private Movement _movement;
        public string nextLevel;
        private void Start()
        {
            _movement = FindObjectOfType<Movement>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            StartCoroutine("FinishLevel");
        }
        
        public IEnumerator FinishLevel()
        {
            _movement.canDash = true;
            FindObjectOfType<CinemachineVirtualCamera>().Follow = null;
            _movement.dashDuration = 10;
            _movement.Dash();
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(nextLevel);
            yield return 0;
        }
        
    }
}