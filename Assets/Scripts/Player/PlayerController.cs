using System;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;

namespace Player
{
    // [DefaultExecutionOrder(-100)]
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement playerMovement;
        private PlayerCamera playerCamera;
        private PlayerAnimation playerAnimation;
        private PlayerCrosshair playerCrosshair;
        private PlayerJump playerJump;
        
        [SerializeField] private PlayerMovementConfiguration playerMovementConfiguration;
        [SerializeField] private PlayerCameraConfiguration playerCameraConfiguration;
        [SerializeField] private PlayerCrosshairConfiguration playerCrosshairConfiguration;
        [SerializeField] private PlayerJumpConfiguration playerJumpConfiguration;

        private void Awake()
        {
            playerMovement = gameObject.AddComponent<PlayerMovement>();
            playerCamera = gameObject.AddComponent<PlayerCamera>();
            playerAnimation = gameObject.AddComponent<PlayerAnimation>();
            playerCrosshair = gameObject.AddComponent<PlayerCrosshair>();
            playerJump = gameObject.AddComponent<PlayerJump>();
            
            playerMovement.Configuration = playerMovementConfiguration;
            playerCamera.Configuration = playerCameraConfiguration;
            playerCrosshair.Configuration = playerCrosshairConfiguration;
            playerJump.Configuration = playerJumpConfiguration;
            playerAnimation.Animator = gameObject.GetComponentInChildren<Animator>();
            
        }

        public void Enable()
        {
            playerMovement.enabled = true;
            playerCamera.enabled = true;
            playerAnimation.enabled = true;
            playerCrosshair.enabled = true;
            playerJump.enabled = true;
        }

        public void Disable()
        {
            playerMovement.enabled = false;
            playerCamera.enabled = false;
            playerAnimation.enabled = false;
            playerCrosshair.enabled = false;
            playerJump.enabled = false;
        }

    }
}