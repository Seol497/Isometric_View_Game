//using System;
//using System.Collections;
//using UnityEngine;
//using UnityEngine.AI;

//public class MonsterInfo : MonoBehaviour
//{
//    public StateMachine stateMachine;

//    public string monsterName;    // ��ü �̸�
//    public int maxHp;             // �ִ� ü��
//    public int currentHp;         // ���� ü��
//    public float speed;           // �̵��ӵ�
//    public int atk;             // ���ݷ�
//    public BoxCollider attackArea;    //���� ����

//    public NavMeshAgent nav;
//    public Rigidbody rigid;

//    public Collider bodycollider;
//    public Animator ani;
//    public MonsterDB monsterDB;

//    private void Awake()
//    {
//        bodycollider = GetComponent<Collider>();
//        nav = GetComponent<NavMeshAgent>();
//        rigid = GetComponent<Rigidbody>();
//        InitializeFromDB(0);
//    }

//    private void Start()
//    {
//        currentHp = maxHp;
//        nav.speed = speed;
//        ChaseStart();
//    }

//    private void FixedUpdate()
//    {

//        FreezeVelocity();   //�̵��� ���� ����
//    }

//    private void Update()
//    {
        

//        if (currentHp <= 0)
//            Dead();
//    }


//    void OnTriggerStay(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            if (!isAttack)
//            {
//                StartCoroutine(Attack());
//            }
//        }
//    }


//    private void ChaseStart()   //���� ����
//    {
//        isChase = true;
//        ani.SetBool("Walk", true);
//    }

//    private IEnumerator Attack()
//    {
//        if (isAttack) yield break;

//        isChase = false;
//        isAttack = true;
//        ani.SetBool("Attack", true);

//        attackArea.enabled = true; // ���� ���� �� ���� ���� Ȱ��ȭ
//        yield return new WaitForSeconds(1.5f);

//        attackArea.enabled = false; // ���� ���� �� ���� ���� ��Ȱ��ȭ
//        yield return new WaitForSeconds(0.5f);

//        isChase = true;
//        isAttack = false;
//        ani.SetBool("Attack", false);

//    }//���� �ڷ�ƾ

//    private void FreezeVelocity()
//    {
//        if (isChase)
//        {
//            rigid.velocity = Vector3.zero;
//            rigid.angularVelocity = Vector3.zero;
//        }
//    }   //�̵� �� ���� ����

//    void Damage(int index)
//    {
//        currentHp -= index;
//        Hit2();
//    } //�ǰ� ����

//    private IEnumerator Hit2()
//    {
//        isChase = false;
//        ani.SetTrigger("TakeDamage");
//        yield return new WaitForSeconds(1.5f);
//        isChase = true;
//    } //�ǰݽ� �ڷ�ƾ

//    private void Dead()
//    {
//        ani.SetTrigger("Dead");
//        isChase = false;
//        StartCoroutine(DeadDelay(3f));
//    }   //��� ����

//    private IEnumerator DeadDelay(float delay)
//    {
//        yield return new WaitForSeconds(delay);
//        gameObject.SetActive(false);
//    } //��� ������(��Ȱ��ȭ)

//    private void InitializeFromDB(int index)
//    {
//        if (monsterDB != null && index >= 0 && index < monsterDB.Monster.Count)
//        {
//            MonsterEntity monsterData = monsterDB.Monster[index];
//            monsterName = monsterData.name;
//            maxHp = monsterData.maxHp;
//            speed = monsterData.speed;
//            atk = monsterData.atk;
//        }
//    } //���� ������ �ҷ����� ���
//}

