using UnityEngine;

public class PlayerInteractStateContext : MonoBehaviour
{
    public IPlayerInteractState currentState { get; set; }
    private readonly PlayerController _playerController;

    public PlayerInteractStateContext(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void Transition()
    {
        currentState.Handle(_playerController);
    }

    public void Transition(IPlayerInteractState state)
    {
        currentState = state;
        currentState.Handle(_playerController);
    }
}
