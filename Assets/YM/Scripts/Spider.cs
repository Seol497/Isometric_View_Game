using System;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    [SerializeField] private MonsterDB monsterDB;

    [SerializeField] public string name;
    [SerializeField] public int maxHp;
    [SerializeField] private int Hp; // ���� ü��
    [SerializeField] public float speed;
    [SerializeField] public int atk;

    public Transform target; // �׺���̼� Ÿ��
    private NavMeshAgent navigation;
    private Animator anim;
    public Collider detectArea; // ���� ���� �ݶ��̴�
    public Collider attackArea; // ���� ���� �ݶ��̴�
    private bool playerInDetectArea = false;
    private bool playerInAttackArea = false;

    private void Awake()
    {
        navigation = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        InitializeFromDB(0); // 0�� �ε����� ����Ͽ� Spider �����͸� ������
    }

    private void Start()
    {
        navigation.speed = speed; // �̵� �ӵ� ����
        Hp = maxHp;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (playerInDetectArea && !playerInAttackArea)
        {
            Move();
        }
        if (playerInAttackArea)
        {
            Attack();
        }
        else
        {
            anim.SetBool("Move", false);
        }
    }

    private void Move()
    {
        // Ÿ���� ���� �̵�
        navigation.SetDestination(target.position);
        anim.SetBool("Move", true);

        // Ÿ�ٿ� �����ߴ��� �˻�
        if (navigation.remainingDistance <= navigation.stoppingDistance)
        {
            anim.SetBool("Move", false);
        }
    }

    private void Attack()
    {
        // ���� �ִϸ��̼� ����
        anim.SetTrigger("Attack");
        //�÷��̾� ü�¿� ���ݷ¸�ŭ ������ ����
        //player.hp -= atk;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other == detectArea)
            {
                Debug.Log("�÷��̾� ���� ���� �ȿ� ����");
                playerInDetectArea = true;
            }
            else if (other == attackArea)
            {
                Debug.Log("�÷��̾� ���� ���� �ȿ� ����");
                playerInAttackArea = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other == detectArea)
            {
                Debug.Log("�÷��̾� ���� �������� ����");
                playerInDetectArea = false;
            }
            else if (other == attackArea)
            {
                Debug.Log("�÷��̾� ���� �������� ����");
                playerInAttackArea = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        anim.SetTrigger("Hit");
        if (Hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetBool("Die", true);
        gameObject.SetActive(false);
    }

    private void InitializeFromDB(int index)
    {
        if (index >= 0 && index < monsterDB.Monster.Count)
        {
            MonsterEntity spiderData = monsterDB.Monster[index];
            name = spiderData.name;
            maxHp = spiderData.maxHp;
            speed = spiderData.speed;
            atk = spiderData.atk;
        }
    }

}
