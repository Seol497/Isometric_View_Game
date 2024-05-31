using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField] private string monsterName;    // ��ü �̸�
    [SerializeField] private int maxHp;             // �ִ� ü��
    [SerializeField] private int currentHp;         // ���� ü��
    [SerializeField] private float speed;           // �̵��ӵ�
    [SerializeField] private float atk;             // ���ݷ�
    [SerializeField] private BoxCollider attackArea;    //���� ����

    private NavMeshAgent nav;
    private Rigidbody rigid;
    //private LayerMask playerLayerMask;

    private Collider bodycollider;
    [SerializeField] private Animator ani;
    [SerializeField] private MonsterDB monsterDB;
    [SerializeField] private bool isChase;
    [SerializeField] private bool isAttack;

    private void Awake()
    {
        bodycollider = GetComponent<Collider>();
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        //playerLayerMask = LayerMask.GetMask("Player"); //ĳ�� ���� ����ȭ
        InitializeFromDB(0);
    }

    private void Start()
    {
        currentHp = maxHp;
        nav.speed = speed;
        ChaseStart();
    }

    private void FixedUpdate()
    {
       
        FreezeVelocity();   //�̵��� ���� ����
    }

    private void Update()
    {
        if (nav.enabled)
        {
            nav.SetDestination(GameManager.instance.player.transform.position);
            nav.isStopped = !isChase;
        }   //�÷��̾�� �̵� ���

        if (currentHp <= 0)
            Dead();
    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }


    private void ChaseStart()   //���� ����
    {
        isChase = true;
        ani.SetBool("Walk", true);
    }

    private IEnumerator Attack()
    {
        if (isAttack) yield break;

        isChase = false;
        isAttack = true;
        ani.SetBool("Attack", true);

        attackArea.enabled = true; // ���� ���� �� ���� ���� Ȱ��ȭ
        yield return new WaitForSeconds(1.5f);

        attackArea.enabled = false; // ���� ���� �� ���� ���� ��Ȱ��ȭ
        yield return new WaitForSeconds(0.5f);

        isChase = true;
        isAttack = false;
        ani.SetBool("Attack", false);

    }//���� �ڷ�ƾ

    private void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }   //�̵� �� ���� ����

    void Damage(int index)
    {
        currentHp -= index;
        Hit2();
    } //�ǰ� ����

    private IEnumerator Hit2()
    {
        isChase = false;
        ani.SetTrigger("TakeDamage");
        yield return new WaitForSeconds(1.5f);
        isChase = true;
    } //�ǰݽ� �ڷ�ƾ

    private void Dead()
    {
        ani.SetTrigger("Dead");
        isChase = false;
        StartCoroutine(DeadDelay(3f));
    }   //��� ����

    private IEnumerator DeadDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    } //��� ������(��Ȱ��ȭ)

    private void InitializeFromDB(int index)
    {
        if (monsterDB != null && index >= 0 && index < monsterDB.Monster.Count)
        {
            MonsterEntity monsterData = monsterDB.Monster[index];
            monsterName = monsterData.name;
            maxHp = monsterData.maxHp;
            speed = monsterData.speed;
            atk = monsterData.atk;
        }
    } //���� ������ �ҷ����� ���
}
