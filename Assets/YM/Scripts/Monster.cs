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
    [SerializeField] private Transform target;      // �̵� ��ǥ
    [SerializeField] private BoxCollider attackArea;    //���� ����

    private NavMeshAgent nav;
    private Rigidbody rigid;
    private LayerMask playerLayerMask;
    private bool atkbool;

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
        playerLayerMask = LayerMask.GetMask("Player"); //ĳ�� ���� ����ȭ
        InitializeFromDB(0);
        Invoke("ChaseStart", 2);
    }

    private void Start()
    {
        currentHp = maxHp;
        nav.speed = speed;
    }

    private void FixedUpdate()
    {
        Targeting();
        FreezeVelocity();
    }

    private void Targeting()
    {
        float targetRadius = 1.5f;
        float targetRange = 2.5f;

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, playerLayerMask);

        if (rayHits.Length > 0 && !isAttack)
        {
            atkbool = true; // �Ź� ������ ������ atkbool�� true�� ����
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
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

    }
    //���� �ڷ�ƾ

    private void Update()
    {
        if (nav.enabled)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }

        Dead(); //������
    }


    private void ChaseStart()   //���� ����
    {
        isChase = true;
        ani.SetBool("Walk", true);
    }

    private void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }   //�̵� �� ���� ����

    private void Dead()
    {
        if (currentHp <= 0)
        {
            ani.SetTrigger("Dead");
            gameObject.SetActive(false);
            isChase = false;
            nav.enabled = false;
        }
    } //��� ���

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
