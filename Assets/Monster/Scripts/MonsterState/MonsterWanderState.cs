using UnityEngine;
using UnityEngine.AI;

public class MonsterWanderState : MonsterBasicState
{
    bool isWandering = false;
    float timer = 0;
    float wanderTimer = 2f; // ��ȸ Ÿ�̸� (�� ����)
    float wanderRadius = 4f; // ��ȸ �ݰ�

    public override void EnterState(MonsterStateManager monster)
    {
        monster.nav.enabled = true;
        // �ʱ�ȭ �۾�: �ʿ信 ���� �ʱ�ȭ �۾� �߰�
        InitializeNavAgent(monster.nav);
    }

    public override void UpdateState(MonsterStateManager monster)
    {
        // ��ȸ Ÿ�̸� ������Ʈ
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            monster.nav.isStopped = false;
            // ���ο� ��ġ�� �����ϰ� �̵�
            Vector3 newPos = RandomNavSphere(monster.transform.position, wanderRadius, NavMesh.AllAreas);
            monster.nav.SetDestination(newPos);

            timer = 0f;

            monster.ani.SetBool("Wander", true);
            isWandering = true;
        }

        // ��ǥ ������ �����ߴ��� Ȯ��
        if (isWandering && !monster.nav.pathPending)
        {
            if (monster.nav.remainingDistance <= monster.nav.stoppingDistance)
            {
                if (!monster.nav.hasPath || monster.nav.velocity.sqrMagnitude == 0f)
                {
                    // ��ǥ ������ ���������� ��ȸ �ִϸ��̼� ����
                    monster.ani.SetBool("Wander", false);
                    isWandering = false;
                }
            }
        }
    }

    public override void ExitState(MonsterStateManager monster)
    {
        if (monster.isDead == false)
        {
            monster.ani.SetBool("Wander", false);
            monster.nav.isStopped = true;
            isWandering = false;
        }
        // ���� ���� �� �۾�: ���� ��� �ִϸ��̼��� �����ϰ� ��ȸ ���¸� �ʱ�ȭ
    }

    // ������ ��ġ�� �̵��ϴ� �޼���
    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    // NavMeshAgent ���� �ʱ�ȭ �޼���
    private void InitializeNavAgent(NavMeshAgent nav)
    {
        nav.updatePosition = true;
        nav.updateRotation = true;
        nav.isStopped = true;
    }
}


