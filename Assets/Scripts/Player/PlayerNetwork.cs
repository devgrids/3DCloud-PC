using System;
using UnityEngine;
using Photon.Pun;

namespace Player
{
    public class PlayerNetwork : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        
        private PlayerComponent playerComponent;
        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            playerComponent = PlayerComponent.sharedInstance;
        }

        void Update()
        {
            if (_photonView.IsMine)
            {
                playerController.Enable();
                playerComponent.Camera.enabled = true;
            }
            else
            {
                playerController.Disable();
                playerComponent.Camera.enabled = false;
            }
        }
        
    }
}