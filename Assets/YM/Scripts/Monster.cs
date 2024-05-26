using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField] private int MaxHp;   // �ִ� ü��
    [SerializeField] private int Hp;      // ���� ü��
    [SerializeField] private int Atk;     // ���ݷ�
    [SerializeField] private float Speed; // �̵��ӵ�

    [SerializeField] private Animator ani;              // �Ź��� �ִϸ��̼�
    [SerializeField] private NavMeshAgent nav;          // �Ź��� �׺���̼�
    [SerializeField] private Transform target;          // �̵� ��ǥ�� ��ǥ

    [SerializeField] private Collider MonsterCollider;  // �Ź� ��ü�� �ݶ��̴�
    [SerializeField] private Collider detectRange;      // ���� ���� �ݶ��̴�
    [SerializeField] private Collider AtkRange;         // ���� ���� �ݶ��̴�

    Rigidbody rigid;

    [SerializeField] private MonsterDB MonsterDB;       // ���� �����ͺ��̽�

    [SerializeField] private float Cooltime = 1f;       // ���� ��Ÿ��
    [SerializeField] private bool IsAttacking;          // ���� ���� Ȯ��
    [SerializeField] private bool IsChasing = false;    // �̵� ���� Ȯ��

    private void Awake()
    {
        ani = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        MonsterCollider = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
        InitializeFromDB(0);
    }

    private void Start()
    {
        Hp = MaxHp;
        nav.speed = Speed;
    }

    private void FixedUpdate()
    {
        if (IsChasing)
        {
            Move();
        }
        FreezeVelocity();
    }

    private void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other == detectRange)
            {
                IsChasing = true;
                IsAttacking = false;
            }
            else if (other == AtkRange)
            {
                IsChasing = false;
                IsAttacking = true;
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other == detectRange)
            {
                IsChasing = false;
                ani.SetBool("Walk", false);
            }
            else if (other == AtkRange)
            {
                IsAttacking = false;
            }
        }
    }

    private IEnumerator AttackCoroutine() // ���� ��Ÿ���� ������ �ڷ�ƾ
    {
        while (IsAttacking)
        {
            ani.SetBool("Attack", true);
            yield return new WaitForSeconds(Cooltime);
        }
    }

    private void Move()
    {
        nav.SetDestination(target.position);
        ani.SetBool("Walk", true);

        // ��ǥ�� �����ߴ��� Ȯ���ϰ� �ִϸ��̼� ����
        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            ani.SetBool("Walk", false);
        }
    }

    private void InitializeFromDB(int index) // ���� �����ͺ��̽����� ���� �ҷ����� �޼���
    {
        if (index >= 0 && index < MonsterDB.Monster.Count)
        {
            MonsterEntity spiderData = MonsterDB.Monster[index];
            name = spiderData.name;
            MaxHp = spiderData.maxHp;
            Speed = spiderData.speed;
            Atk = spiderData.atk;
        }
    }
}
