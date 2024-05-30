using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class MonsterBasicState
{
    public string monsterName;    // ��ü �̸�
    public int maxHp;             // �ִ� ü��
    public int currentHp;         // ���� ü��
    public float speed;           // �̵��ӵ�
    public float atk;             // ���ݷ�
    public BoxCollider attackArea;    //���� ����

    public NavMeshAgent nav;
    public Rigidbody rigid;
    //private LayerMask playerLayerMask;

    public Collider bodycollider;
    public Animator ani;
    public MonsterDB monsterDB;
    public bool isChase;
    public bool isAttack;

    public abstract void EnterState(MonsterStateManager monster);

    public abstract void UpdateState(MonsterStateManager monster);

    public abstract void OnCollisionEnter(MonsterStateManager monster);


}
