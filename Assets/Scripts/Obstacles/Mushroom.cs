using System;
using System.Collections;
using UnityEngine;

namespace Player.Obstacles
{
    public class Mushroom : MonoBehaviour
    {
        public float forceX;
        public float forceY;
        public float duration = 1;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine("MushroomJump");
            }
        }

        public IEnumerator MushroomJump()
        {
            Movement movement = FindObjectOfType<Movement>();
            movement.DisableMovement();
            movement.GetComponent<Rigidbody2D>().velocity = new Vector2(forceX,forceY);
            movement.GetComponent<Rigidbody2D>().gravityScale = 0;
            yield return new WaitForSeconds(duration);
            movement.EnableMovement();
            movement.GetComponent<Rigidbody2D>().gravityScale = 1;
            movement.canDash = true;
            yield return 0;
        }
    }
}