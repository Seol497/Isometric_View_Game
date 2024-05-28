using System.Collections;
using TreeEditor;
using Unity.Mathematics;
using Unity.VisualScripting;
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

    private NavMeshAgent nav;
    private Rigidbody rigid;
    [SerializeField]private Collider bodycollider;
    public bool detected = false;
    public bool attackRange = false;

    //public bool isInBoundary = false;
    //public bool isInAttackRange = false;

    [SerializeField] private Animator ani;
    [SerializeField] private MonsterDB monsterDB;

    private void Awake()
    {
        bodycollider = GetComponent<Collider>();
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        InitializeFromDB(0);
    }

    private void Start()
    {
        currentHp = maxHp;
        nav.speed = speed;
    }

    private void FixedUpdate()
    {
        if (detected && !attackRange)
        {
            nav.isStopped = false;
            nav.SetDestination(target.position); // ���� �̵� ���
        }
        else if(detected && attackRange)
        {
            nav.isStopped = true;
            ani.SetBool("Walk", false);
        }
            
    }

    private void Update()
    {
        if(detected)
            transform.LookAt(target.position);
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            attackRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            attackRange = false;
        }
    }



    private void InitializeFromDB(int index) // ���� �����ͺ��̽����� ���� �ҷ����� �޼���
    {
        if (monsterDB != null && index >= 0 && index < monsterDB.Monster.Count)
        {
            MonsterEntity monsterData = monsterDB.Monster[index];
            monsterName = monsterData.name;
            maxHp = monsterData.maxHp;
            speed = monsterData.speed;
            atk = monsterData.atk;
        }
    }


}
