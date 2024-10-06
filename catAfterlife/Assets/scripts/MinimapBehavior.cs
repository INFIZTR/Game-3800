using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MinimapBehavior : MonoBehaviour
{
    private Transform _player;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void LateUpdate()
    {
        Vector3 playerPosition = _player.position;
        playerPosition.z = transform.position.z;
        transform.position = playerPosition;
    }
}
