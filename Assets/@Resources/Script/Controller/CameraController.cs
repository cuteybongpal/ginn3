using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerController player;
    Vector3 CameraOffset;
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        CameraOffset = transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position = player.transform.position + CameraOffset;
    }
}
