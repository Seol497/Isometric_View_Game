using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterStateManager : MonoBehaviour
{
    MonsterBasicState currentState;
    public MonsterIdleState idleState = new MonsterIdleState();
    public MonsterChaseState chaseState = new MonsterChaseState();
    public MonsterAttackState attackState = new MonsterAttackState();
    public MonsterHitState hitState = new MonsterHitState();
    public MonsterDeadState deadState = new MonsterDeadState();
    public RagDoll ragdoll;

    [SerializeField] private int excelDBNumber;     //�������� �ҷ��� ������ ��ȣ
    [SerializeField] private string monsterName;    // ��ü �̸�
    [SerializeField] private int maxHp;             // �ִ� ü��
    public int currentHp;         // ���� ü��
    [SerializeField] private float speed;           // �̵��ӵ�
    [SerializeField] private int atk;               // ���ݷ�
    public Transform target;                        // �÷��̾��� ��ġ(�ӽ�)

    public float deadCount; //����� ������� �ð�

    public bool isDead;                 //��� ���� Ȯ��
    public bool hit;
    public float targetDistance;

    public BoxCollider attackArea;      //���� ����
    [SerializeField] private MonsterDB monsterDB;
    public NavMeshAgent nav;
    public Rigidbody rigid;
    public Collider bodyCollider;
    public Animator ani;
    public BoxCollider detectArea;      //�ĺ� ����



    void Awake()
    {
        bodyCollider = GetComponent<Collider>();
        nav = GetComponent<NavMeshAgent>();
        InitializeFromDB(excelDBNumber);

    }
    void OnEnable()
    {
        deadCount = 10;
        currentHp = maxHp;
        nav.speed = speed;
        isDead = false;
        gameObject.SetActive(true);
        ani.enabled = true;
        ragdoll.SetRagdollActive(false);
        rigid.isKinematic = false;
        bodyCollider.enabled = true;
        currentState = idleState;
        currentState.EnterState(this);
    }


    void Start()
    {

    }


    void Update()
    {
        currentState.UpdateState(this);

        //���Ϳ� Ÿ��(�÷��̾� �Ÿ� üũ)
        if (target != null)
        {
            nav.enabled = true;
            targetDistance = Vector3.Distance(transform.position, target.position);
            ani.SetFloat("targetDistance", targetDistance);
        }


        if (targetDistance <= 2 && isDead == false)
        {
            ChangeState(attackState);
        }
        else if (targetDistance > 2 && targetDistance < 15 && isDead == false)
        {
            ChangeState(chaseState);
        }
        else if (isDead == false)
        {
            ChangeState(idleState);
        }
        else if (isDead == true)
        {
            ChangeState(deadState);
        }

    }


    public void ChangeState(MonsterBasicState state)
    {
        currentState.ExitState(this);
        currentState = state;
        state.EnterState(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            JWPlayerController player = other.GetComponent<JWPlayerController>();
            target = player.transform;
        }
    }


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
    }//���������� �ҷ����� �޼���

    public void MonsterDead()
    {
        ChangeState(deadState);
    }

    public void MonsterHit()
    {
        ChangeState(hitState);
    }

}

