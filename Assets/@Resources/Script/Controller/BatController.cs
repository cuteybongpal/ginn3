using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BatController : MonoBehaviour
{
    PlayerController player;
    Rigidbody rb;

    public float Speed;
    public float ChasingDistance;
    int currentHp;
    public int MaxHp;
    UI_MonsterHpBar hpBar;
    public int CurrentHp
    {
        get { return currentHp; }
        set
        {
            currentHp = value;
            hpBar.SetHp(CurrentHp);
            if (currentHp <= 0 )
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        rb = GetComponent<Rigidbody>();
        
        hpBar = GetComponentInChildren<UI_MonsterHpBar>();
        hpBar.Init(MaxHp);
        CurrentHp = MaxHp;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= ChasingDistance && !player.Invisible)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.y = 0;
            rb.velocity = dir.normalized * Speed;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Attack"))
            return;
        CurrentHp--;
    }
}
