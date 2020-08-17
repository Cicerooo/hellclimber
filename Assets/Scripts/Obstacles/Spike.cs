using System;
using UnityEngine;

namespace Player.Obstacles
{
    public class Spike : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag=="Player")
            {
                FindObjectOfType<DeathManager>().Die();
            }
        }
    }
}