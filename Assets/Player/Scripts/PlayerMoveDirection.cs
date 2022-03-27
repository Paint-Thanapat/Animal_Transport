using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveDirection : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;

    void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    void FixedUpdate()
    {
        transform.rotation = new Quaternion(player.transform.rotation.x,
                                            mainCamera.transform.rotation.y,
                                            player.transform.rotation.z,
                                            mainCamera.transform.rotation.w);
    }
}
