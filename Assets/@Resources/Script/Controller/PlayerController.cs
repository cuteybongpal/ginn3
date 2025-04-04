using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.ParticleSystem;

public class PlayerController : MonoBehaviour
{
    enum PlayerState
    {
        Idle,
        Run,
        Attack,
        Jump
    }
    PlayerState State 
    { 
        set
        {
            switch (value)
            {
                case PlayerState.Idle:
                    S_Idle();
                    break;
                case PlayerState.Run:
                    S_Run();
                    break;
                case PlayerState.Attack:
                    S_Attack();
                    break;
                case PlayerState.Jump:
                    S_Jump();
                    break;
            }
        }
    }
    Rigidbody rb;
    Animator anim;
    bool canJump;
    bool canAttack = true;
    public Collider AttackCollider;
    public bool Invisible = false;
    public bool Invincible = false;
    public AudioSource[] audioSources;
    ParticleSystem particle;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        particle = GetComponentInChildren<ParticleSystem>();
        AttackCollider.enabled = false;
    }
    void Update()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            dir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir += Vector3.right;
        }

        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
            State = PlayerState.Jump;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
                State = PlayerState.Attack;
        }
        rb.velocity = new Vector3(0,rb.velocity.y,0) + dir.normalized * GameManager.Instance.PlayerSpeed;
        if (dir == Vector3.zero)
            State = PlayerState.Idle;
        else
            State = PlayerState.Run;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, ray.direction, 100);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red);
        Vector3 collPos = Vector3.zero;
        foreach (RaycastHit hit in hits)
        {
            if (!hit.collider.CompareTag("Ground"))
                continue;
            collPos = hit.point;
            break;
        }
        if (collPos != Vector3.zero)
        {
            collPos -= transform.position;
            collPos.y = 0;
            transform.rotation = Quaternion.LookRotation(collPos.normalized);
        }
    }
    void S_Idle()
    {
        anim.SetFloat("Speed", 0);
    }
    void S_Run()
    {
        anim.SetFloat("Speed", 1);
    }
    void S_Attack()
    {
        if (canAttack)
        {
            anim.Play("Attack");
            audioSources[3].Play();
        }
    }
    void S_Jump()
    {
        anim.Play("Jump");
        audioSources[2].Play();
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Ground"))
            return;
        canJump = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster") || other.CompareTag("Trap"))
        {
            if (Invincible)
                return;
            particle.Play();
            GameManager.Instance.PlayerCurrentHp--;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ground"))
            return;
        canJump = false;
    }
    void AttackOn()
    {
        StartCoroutine(AttackColliderOnOff());
        StartCoroutine(CoolDown());
    }
    void FootStep()
    {
        audioSources[0].Play();
    }
    IEnumerator AttackColliderOnOff()
    {
        AttackCollider.enabled = true;
        yield return new WaitForSeconds(.3f);
        AttackCollider.enabled = false;
    }
    IEnumerator CoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
}
