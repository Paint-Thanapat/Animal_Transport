using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlowMovementState : MonoBehaviour, IPlayerMovementState
{
    private PlayerController _playerController;
    public void Handle(PlayerController playerController)
    {
        if (!_playerController)
        {
            _playerController = playerController;
        }

        if (_playerController.isFreeze)
            return;

        _playerController.currentSpeed = _playerController.slowSpeed;
        _playerController.canJump = false;
    }
}
