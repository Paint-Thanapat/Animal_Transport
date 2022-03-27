using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopMovementState : MonoBehaviour, IPlayerMovementState
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

        _playerController.currentSpeed = 0;
        _playerController.canJump = false;
    }
}
