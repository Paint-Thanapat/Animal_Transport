using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmptyHandState : MonoBehaviour, IPlayerInteractState
{
    private PlayerController _playerController;
    public void Handle(PlayerController playerController)
    {
        if (!_playerController)
        {
            _playerController = playerController;
        }

        _playerController.isHolding = false;

        if (_playerController.animalHolder)
            _playerController.animalHolder.gameObject.transform.SetParent(null);
    
        _playerController.anim.SetTrigger("isIdle");
    }
}
