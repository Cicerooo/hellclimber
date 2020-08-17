using System;
using System.Collections;
using System.Security.AccessControl;
using UnityEngine;
using TMPro;
namespace Player.Obstacles
{
    public class IceFromTheWaistDown : MonoBehaviour
    {
        private Movement _movement;
        private bool hasShattered = false;
        private Explodable _explodable;
        public TMP_Text text;
        private void Awake()
        {
            InputMaster inputMaster = new InputMaster();
            inputMaster.Player.Dash.performed += _=>Shatter();
            inputMaster.Player.Dash.Enable();
            _explodable = GetComponent<Explodable>();
            Physics2D.IgnoreLayerCollision(9,10);
        }

        private void Shatter()
        {
            if (FindObjectOfType<CircleVerse>().loadingFinished)
            {
                _movement.EnableMovement();
                Time.timeScale = 1;
                _explodable.explode();
                StartCoroutine("CustomDash");
                Destroy(text.gameObject);
                GameObject obj = Instantiate(new GameObject());
                obj.AddComponent<TimeManager>();
            }
        }

        private IEnumerator CustomDash()
        {
            float dur = _movement.dashDuration;
            float speed = _movement.dashSpeed;
            _movement.dashDuration = 0.4f;
            _movement.dashSpeed = 35;
            _movement.Dash();
            yield return new WaitForSeconds(0.5f);
            _movement.dashDuration = dur;
            _movement.dashSpeed = speed;
            yield return 0;
        }
        private void Start()
        {
            _movement = FindObjectOfType<Movement>();
        }
        
        private void Update()
        {
            if (!hasShattered)
            {
                _movement.DisableMovement();
                Time.timeScale = 0;
            }
        }
    }
}