using System;
using UnityEngine;

namespace Platformer2DSystem.Example
{
    [RequireComponent(typeof(Actor))]
    [RequireComponent(typeof(Runner))]
    [RequireComponent(typeof(Jumper))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private int maxJumps;
        [SerializeField] private int jumpBufferFrames;
        [SerializeField] private float jumpMovementMultiplier;
        [SerializeField] private float doubleJumpMultiplier;

        private Actor actor;
        private Runner runner;
        private Jumper jumper;

        private int remainingJumps;
        private Timer jumpBufferTimer;

        private void Awake()
        {
            actor = GetComponent<Actor>();
            runner = GetComponent<Runner>();
            jumper = GetComponent<Jumper>();
            jumpBufferTimer = Timer.Frames(jumpBufferFrames);
            remainingJumps = maxJumps;
        }

        private void OnEnable()
        {
            actor.GroundEntered += OnGroundEntered;
            actor.CeilingHit += OnCeilingHit;
        }

        private void Update()
        {
            Vector2 direction = GetInputDirection();
            UpdateMovement(direction);
            UpdateJumping(direction);
        }

        private Vector2 GetInputDirection()
        {
            Vector2 direction = new();
            direction.x = Convert.ToInt32(Input.GetKey(KeyCode.RightArrow)) - Convert.ToInt32(Input.GetKey(KeyCode.LeftArrow));
            direction.y = Convert.ToInt32(Input.GetKey(KeyCode.UpArrow)) - Convert.ToInt32(Input.GetKey(KeyCode.DownArrow));
            return direction;
        }

        private void UpdateMovement(Vector2 direction)
        {
            if (direction.x == 0f)
            {
                runner.Stop();
            }
            else
            {
                float multiplier = jumper.IsJumping ? jumpMovementMultiplier : 1f;
                runner.Move(direction.x, multiplier);
            }
        }

        private void UpdateJumping(Vector2 direction)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpBufferTimer.Start();
            }

            if (jumpBufferTimer.IsRunning && remainingJumps > 0)
            {
                if (direction == Vector2.down && actor.IsOnGroundOneWay)
                {
                    jumper.JumpDown();
                }
                else
                {
                    bool isDoubleJumping = remainingJumps < maxJumps;
                    float multiplier = isDoubleJumping ? doubleJumpMultiplier : 1f;
                    jumper.Jump(multiplier);
                    remainingJumps--;
                }

                jumpBufferTimer.Stop();
            }

            if (!Input.GetKey(KeyCode.Space) && jumper.IsJumping)
            {
                jumper.CancelJump();
            }
        }

        public void OnGroundEntered(Collider2D ground)
        {
            remainingJumps = maxJumps;
        }

        public void OnCeilingHit(Collider2D ceiling)
        {
            jumper.CancelJump();
        }

        private void OnDisable()
        {
            actor.GroundEntered -= OnGroundEntered;
            actor.CeilingHit -= OnCeilingHit;
        }
    }
}
