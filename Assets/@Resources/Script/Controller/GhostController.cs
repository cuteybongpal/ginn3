using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float Speed;
    public Transform[] Targets;
    int targetIndex = 0;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = Targets[0].position;
        targetIndex++;
        StartCoroutine(CommandMove());
    }
    IEnumerator CommandMove()
    {
        while (true)
        {
            yield return Move();
            targetIndex = (targetIndex + 1) % Targets.Length;
        }
    }

    IEnumerator Move()
    {
        Vector3 dir = (Targets[targetIndex].position -  transform.position).normalized;
        rb.velocity = dir * Speed;
        transform.rotation = Quaternion.LookRotation(dir);
        while(true)
        {
            if (Vector3.Distance(transform.position, Targets[targetIndex].position) < 0.1f)
                break;
            yield return null;
        }
    }
}
