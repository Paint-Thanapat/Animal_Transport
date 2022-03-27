using UnityEngine;

public class PlayerStateContext : MonoBehaviour
{
    public IPlayerMovementState currentState { get; set; }
    private readonly PlayerController _playerController;

    public PlayerStateContext(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void Transition()
    {
        currentState.Handle(_playerController);
    }

    public void Transition(IPlayerMovementState state)
    {
        currentState = state;
        currentState.Handle(_playerController);
    }
}
