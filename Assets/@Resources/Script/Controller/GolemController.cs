using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemController : MonoBehaviour
{

    PlayerController player;
    Rigidbody rb;

    public float Speed;
    public float ChasingDistance;
    public float AttackDist;
    int currentHp;
    public int MaxHp;
    public Collider AttackCollider;
    UI_MonsterHpBar hpBar;
    Animator anim;
    bool canAttack = true;
    public ParticleSystem[] Particles;
    public AudioSource[] Audios;
    public int CurrentHp

    {
        get { return currentHp; }
        set
        {
            currentHp = value;
            hpBar.SetHp(CurrentHp);
            if (currentHp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    enum GolemState
    {
        Idle,
        Walk,
        Attack
    }
    GolemState State
    {
        set
        {
            switch (value)
            {
                case GolemState.Idle:
                    S_Idle();
                    break;
                case GolemState.Walk:
                    S_Walk();
                    break;
                case GolemState.Attack:
                    S_Attack();
                    break;
            }
        }
    }
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        hpBar = GetComponentInChildren<UI_MonsterHpBar>();
        hpBar.Init(MaxHp);
        CurrentHp = MaxHp;
        AttackCollider.enabled = false;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= ChasingDistance && !player.Invisible)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.y = 0;
            rb.velocity = dir.normalized * Speed;
            transform.rotation = Quaternion.LookRotation(dir);
            State = GolemState.Walk;
            if (Vector3.Distance(transform.position, player.transform.position) <= AttackDist)
            {
                State = GolemState.Attack;
            }
        }
        else
        {
            State = GolemState.Idle;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Attack"))
            return;
        CurrentHp -= GameManager.Instance.PlayerAttack;
        Audios[0].Play();
        Particles[0].Play();
    }
    void S_Idle()
    {
        anim.SetFloat("Speed", 0);
    }
    void S_Walk()
    {
        anim.SetFloat("Speed", 1);
    }
    void S_Attack()
    {
        if (canAttack)
            anim.Play("Attack");
        
    }
    void Attack()
    {
        StartCoroutine(ColliderOnOff());
        StartCoroutine(AttackCoolDown());
    }
    IEnumerator ColliderOnOff()
    {
        AttackCollider.enabled = true;
        Particles[1].Play();
        Audios[1].Play();
        yield return new WaitForSeconds(.2f);
        AttackCollider.enabled = false;
    }
    IEnumerator AttackCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(2f);
        canAttack = true;
    }
}
