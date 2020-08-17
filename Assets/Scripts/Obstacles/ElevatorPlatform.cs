using System;
using System.Collections;
using UnityEngine;

namespace Player.Obstacles
{
    public class ElevatorPlatform : MonoBehaviour
    {
        private Vector3 startingPosition;
        public Transform targetPosition;
        public float timeInterval;
        public float transitionDuration;
        private void Start()
        {
            startingPosition = gameObject.transform.position;
            StartCoroutine(MovePlatform());
        }

        public IEnumerator MovePlatform()
        {
            LeanTween.move(gameObject, targetPosition.position, transitionDuration);
            yield return new WaitForSeconds(transitionDuration);
            LeanTween.move(gameObject, startingPosition, transitionDuration);
            yield return new WaitForSeconds(transitionDuration);
            StartCoroutine(MovePlatform());
            yield return null;
        }
    }
}