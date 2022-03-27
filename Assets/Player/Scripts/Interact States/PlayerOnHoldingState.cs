using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnHoldingState : MonoBehaviour, IPlayerInteractState
{
    private PlayerController _playerController;
    public void Handle(PlayerController playerController)
    {
        if (!_playerController)
        {
            _playerController = playerController;
        }

        _playerController.isHolding = true;

        _playerController.animalHolder.gameObject.transform.position = _playerController.holdingTransform.position;
        _playerController.animalHolder.gameObject.transform.SetParent(_playerController.holdingTransform);

        _playerController.anim.SetTrigger("isHolding");
    }
}
