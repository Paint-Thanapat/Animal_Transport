using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RakeController : MonoBehaviour
{
    public GameObject stunExplosion;
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void GetTrigger()
    {
        Collider[] collidersToPlayer = Physics.OverlapSphere(transform.position + transform.forward * 0.2f, 0.4f);
        foreach (Collider nearbyObject in collidersToPlayer)
        {
            PlayerController playerController = nearbyObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController._playerStateContext.Transition(playerController._stunMovementState);

                //create particle
                GameObject particleClone = Instantiate(stunExplosion, transform.position, stunExplosion.transform.rotation);
                Destroy(particleClone, 3f);
                return;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            anim.SetTrigger("isActivate");
        }
    }
}
