//using UnityEngine;
//using UnityEngine.AI;
//using UnityEngine.UI;
//using UnityEngine.XR;

//public class MonsterIdleState : MonsterBasicState
//{
//    float changeWander;
//    bool isWandering = false;
//    int wanderTimer = 3;
//    float timer = 0;
//    float wanderRadius = 1f;

//    public override void EnterState(MonsterStateManager monster)
//    {
//        //changeWander = Random.Range(1, 4);

//    }

//    public override void UpdateState(MonsterStateManager monster)
//    {
//        //if (!isWandering)
//        //{
//        //    changeWander -= Time.deltaTime;
//        //    if (changeWander <= 0f)
//        //    {
//        //        Wander(monster);
//        //        isWandering = true;
//        //    }
//        //}

//        timer += Time.deltaTime;

//        if (timer >= wanderTimer)
//        {
//            Vector3 newPos = RandomNavSphere(monster.transform.position, wanderRadius, -1);
//            monster.nav.SetDestination(newPos);
//            timer = 0f;
//        }
//    }


//    public override void ExitState(MonsterStateManager monster)
//    {
//        //monster.ani.SetBool("Wander", false);
//        //isWandering = false;
//    }

//    private void Wander(MonsterStateManager monster)
//    {
//        monster.ani.SetBool("Wander", true);
//        monster.nav.SetDestination(WanderPosition(monster.transform.position, 5f));
//    }

//    private Vector3 WanderPosition(Vector3 monsterPosition, float radius)
//    {
//        float randomAngle = Random.Range(0f, 360f);

//        // ������ �����κ��� ���� ���� ���
//        Vector3 direction = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), 0, Mathf.Sin(randomAngle * Mathf.Deg2Rad));

//        // ������ ���� ��ġ���� ������ �������� radius ��ŭ ������ ��ġ ���
//        Vector3 wanderPosition = monsterPosition + direction * radius;

//        NavMeshHit hit;
//        // NavMesh���� ��ȿ�� ��ġ�� ã�� ��ȯ
//        if (NavMesh.SamplePosition(wanderPosition, out hit, radius, NavMesh.AllAreas))
//        {
//            Debug.Log(hit.position);
//            return hit.position;
//        }

//        // ��ȿ�� ��ġ�� ã�� ���� ��� ���� ��ġ ��ȯ
//        return monsterPosition;
//    }


//    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
//    {
//        Vector3 randomDirection = Random.insideUnitSphere * dist;
//        randomDirection += origin;
//        NavMeshHit navHit;
//        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);
//        return navHit.position;
//    }


//}

using UnityEngine;
using UnityEngine.AI;

public class MonsterIdleState : MonsterBasicState
{
    bool isWandering = false;
    float timer = 0;
    float wanderTimer = 3f; // ��ȸ Ÿ�̸� (�� ����)
    float wanderRadius = 5f; // ��ȸ �ݰ�

    public override void EnterState(MonsterStateManager monster)
    {
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
        if(monster.isDead == false)
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

