using UnityEngine;

public class PlayerClientMovementState : MonoBehaviour
{
    private PlayerController _playerController;
    void Start()
    {
        //need to keep data "PlayerController" then we use "(PlayerController)" for this
        _playerController = (PlayerController)FindObjectOfType(typeof(PlayerController));
    }
}
