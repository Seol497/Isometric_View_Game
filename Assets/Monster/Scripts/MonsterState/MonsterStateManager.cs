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

    public string monsterName;    // ��ü �̸�
    public int maxHp;             // �ִ� ü��
    public int currentHp;         // ���� ü��
    public float speed;           // �̵��ӵ�
    public int atk;               // ���ݷ�
    public bool hit;
    public float deadCount = 3; //����� ������� �ð�

    public Transform target;
    public bool isChase;
    public bool isDead;

    public float targetDistance;

    public BoxCollider attackArea;    //���� ����
    public NavMeshAgent nav;
    public Rigidbody rigid;
    public Collider bodyCollider;
    public Animator ani;
    public MonsterDB monsterDB;

    void Awake()
    {
        bodyCollider = GetComponent<Collider>();
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        InitializeFromDB(0);
    }
    void OnEnable()
    {
        rigid.velocity = Vector3.zero;
        deadCount = 3;
        currentHp = maxHp;
        nav.speed = speed;
        currentState = idleState;
        currentState.EnterState(this);
        isDead = false;
    }

    void Start()
    {
       
    }


    void Update()
    {
        currentState.UpdateState(this);
        Debug.Log(currentState);

        //���Ϳ� Ÿ��(�÷��̾� �Ÿ� üũ)
        targetDistance = Vector3.Distance(transform.position, target.position);
        ani.SetFloat("targetDistance", targetDistance);

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


    }


    public void ChangeState(MonsterBasicState state)
    {
        currentState.ExitState(this);
        currentState = state;
        state.EnterState(this);
    }

    void OnTriggerEnter(Collider other)
    {
        
    }

    void OnTriggerStay(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {

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

