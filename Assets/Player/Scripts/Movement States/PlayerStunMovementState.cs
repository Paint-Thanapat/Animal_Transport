using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunMovementState : MonoBehaviour, IPlayerMovementState
{
    private PlayerController _playerController;
    public void Handle(PlayerController playerController)
    {
        if (!_playerController)
        {
            _playerController = playerController;
        }


        GameObject stunClone = Instantiate(GameManager.Instance.stunParticle,
                                            _playerController.stunParticlePoint.position,
                                            GameManager.Instance.stunParticle.transform.rotation);

        stunClone.transform.SetParent(_playerController.stunParticlePoint);
        Destroy(stunClone, _playerController.stunDuration);

        StartCoroutine(StunPlayer());

        _playerController.DropAnimal();
    }

    IEnumerator StunPlayer()
    {
        _playerController.currentSpeed = 0;
        _playerController.canJump = false;
        _playerController.isFreeze = true;



        yield return new WaitForSeconds(_playerController.stunDuration);

        _playerController.isFreeze = false;
        _playerController._playerStateContext.Transition(_playerController._normalMovementState);
    }
}
