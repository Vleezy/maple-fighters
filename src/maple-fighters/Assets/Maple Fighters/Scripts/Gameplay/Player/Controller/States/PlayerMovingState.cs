using System;
using UnityEngine;

namespace Scripts.Gameplay.Player.States
{
    public class PlayerMovingState : IPlayerStateBehaviour
    {
        private readonly PlayerController playerController;
        private readonly Rigidbody2D rigidbody2D;

        private Direction direction;
        private bool canMove;

        public PlayerMovingState(PlayerController playerController)
        {
            this.playerController = playerController;

            var collider = playerController.GetComponent<Collider2D>();
            rigidbody2D = collider.attachedRigidbody;
        }

        public void OnStateEnter()
        {
            canMove = true;
        }

        public void OnStateUpdate()
        {
            if (IsGrounded())
            {
                if (IsMoveStopped())
                {
                    playerController.SetPlayerState(PlayerStates.Idle);
                }

                if (IsJumpKeyClicked() && CanJump())
                {
                    playerController.SetPlayerState(PlayerStates.Jumping);
                }

                var horizontal = Utils.GetAxis(Axes.Horizontal, isRaw: true);

                if (Math.Abs(horizontal) > 0)
                {
                    direction =
                        horizontal < 0 ? Direction.Left : Direction.Right;
                }

                playerController.ChangeDirection(direction);
            }
            else
            {
                playerController.SetPlayerState(PlayerStates.Falling);
            }
        }

        public void OnStateFixedUpdate()
        {
            if (canMove)
            {
                var speed = playerController.GetProperties().Speed;
                var horizontal = Utils.GetAxis(Axes.Horizontal);
                var y = rigidbody2D.velocity.y;

                rigidbody2D.velocity = new Vector3(horizontal * speed, y);
            }
        }

        public void OnStateExit()
        {
            // Left blank intentionally
        }

        private bool IsGrounded()
        {
            return playerController.IsGrounded();
        }

        private bool IsJumpKeyClicked()
        {
            var jumpKey = playerController.GetKeyboardSettings().JumpKey;

            return Input.GetKeyDown(jumpKey);
        }

        private bool IsMoveStopped()
        {
            return playerController.IsMoving() == false;
        }

        private bool CanJump()
        {
            return playerController.CanJump();
        }
    }
}