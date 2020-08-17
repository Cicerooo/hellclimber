using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        public float speed;
        public float jumpForce;
        [Header("Ground check")]
        public Transform groundCheckTransform;
        public LayerMask groundCheckLayerMask;
        public float groundCheckRadius;
        [Header("Dashing")]
        public float dashDuration = 0.8f;
        public float dashSpeed;
        [Header("Better Jumping")]
        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2f;
        private float dashTime;
        private Rigidbody2D rb2d;
        private Animator _animator;
        private float moveDirection = 0;
        [HideInInspector]
        public bool isGrounded;
        public bool canMove=true;
        public bool canDash;
        public bool canJump;
        private bool isDashing;
        public float facing=1;
        private InputMaster inputController;
        private void Awake()
        {
            inputController = new InputMaster();
            inputController.Player.Move.performed += Move;
            inputController.Player.Move.Enable();
            inputController.Player.Jump.performed += _ => Jump();
            inputController.Player.Jump.Enable();
            inputController.Player.Dash.performed += _ => Dash();
            inputController.Player.Dash.Enable();
        }

        private void Start()
        {
            moveDirection = 0;
            rb2d = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            //var movementInput = inputController.Player.Move.ReadValue<float>();
            if(canMove)
                MoveTowards(moveDirection);
            GroundCheck();
            _animator.SetFloat("vertical",rb2d.velocity.y);
        }

        private void FixedUpdate()
        {
            if (rb2d.velocity.y<0 && !isDashing)
            {
                rb2d.gravityScale = fallMultiplier;
            } else if (rb2d.velocity.y>0 && !isDashing)
            {
                rb2d.gravityScale = lowJumpMultiplier;
            }
            else if(!isDashing)
            {
                rb2d.gravityScale = 1;
            }
        }
        private void MoveTowards(float direction)
        {
            if (canMove)
            {
                rb2d.velocity = new Vector2(direction*speed,rb2d.velocity.y);
            }
        }
        public void Move(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();
            if(value>0.2f)
                moveDirection = 1;
            else if (value < -0.2f)
                moveDirection = -1;
            else if (value<0.2f && value>-0.2f)
                moveDirection = 0;
            if (canMove)
            {
                Flip(moveDirection);
            }
                if (moveDirection != 0)
                {
                    _animator.SetFloat("velocity",1);
                    facing = moveDirection;
                }
                else
                    _animator.SetFloat("velocity",0);
        }
        public void BetterJumping()
        {
            
            if (rb2d.velocity.y < 0) {
                rb2d.velocity = Physics.gravity * (1 - 1) * Time.deltaTime;
            }
        }
        public void Flip(float direction)
        {
            if (direction != 0)
            {
                transform.localScale = new Vector3(direction,1,1);
                facing = direction;
            }
        }
        public void Dash()
        {
            if (canDash&&canMove)
            {
                isDashing = true;
                canDash = false;
                DisableMovement();
                rb2d.velocity = new Vector2(0,0);
                _animator.SetTrigger("Dash");
                rb2d.gravityScale = 0;
            }
        }

        public void DoDash()
        {
            StartCoroutine("DashCoroutine");
        }
        public IEnumerator DashCoroutine()
        {
            isDashing = true;
            //canMove = false;
            EnableMovement();
            canJump = false;
            rb2d.gravityScale = 0;
            _animator.SetBool("isDashing",true);
            rb2d.velocity = new Vector2(rb2d.velocity.x,dashSpeed);
            
            yield return new WaitForSeconds(dashDuration);
            EnableMovement();
            rb2d.gravityScale = 2;
            rb2d.velocity = new Vector2(0,0);
            canJump = true;
            canMove = true;
            canDash = false;
            isDashing = false;
            _animator.SetBool("isDashing",false);
            Flip(facing);

            yield return 0;
        }
        private void Jump()
        {
            if (isGrounded&&canMove&&!isDashing)
            {
                rb2d.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
            }
            _animator.SetTrigger("Jump");
        }
        private void GroundCheck()
        {
            isGrounded =
                Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundCheckLayerMask);
            if (isGrounded&&!isDashing)
            {
                canDash = true;
            }
            _animator.SetBool("isGrounded", isGrounded);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(groundCheckTransform.position,groundCheckRadius);
        }
        // Utilities
        public void DisableMovement()
        {
            rb2d.velocity=Vector2.zero;
            canMove = false;
            canDash = false;
            //Debug.Break();
        }
        public void EnableMovement()
        {
            canMove = true;
            //canDash = true;
            Flip(facing);
        }
        public void ToggleMovement(bool toggle)
        {
            if (toggle)
            {
                EnableMovement();
            }
            if (!toggle)
            {
                DisableMovement();
            }
        }
        public void MoveForward(float amount)
        {
            rb2d.AddForce(new Vector2(amount,0)*facing, ForceMode2D.Impulse);
        }
    }
}