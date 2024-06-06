using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterWanderState : MonsterBasicState
{
    public float wanderRadius = 2f;

    private Coroutine wanderCoroutine;

    public override void EnterState(MonsterStateManager monster)
    {
        monster.nav.enabled = true;
    }

    public override void UpdateState(MonsterStateManager monster)
    {
        wanderCoroutine = monster.StartCoroutine(WanderCoroutine(monster));
    }

    public override void ExitState(MonsterStateManager monster)
    {

    }

    private IEnumerator WanderCoroutine(MonsterStateManager monster)
    {
        // ���ο� ������ ����
        Vector3 newPos = RandomNavSphere(monster.transform.position, wanderRadius);
        monster.nav.SetDestination(newPos);

        // �� ���� �̵��� �� �ڷ�ƾ�� ����
        yield return null;
        monster.StopCoroutine(wanderCoroutine);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist)
    {
        //�������� ������ ��ġ ����    dist�� ���������� �������̾� ������
        Vector3 randomDirection = Random.insideUnitSphere * dist;

        //�츮���� �������� �̵����ϴ� y���� �ᱸ��
        randomDirection.y = 0;

        //���� ��ġ�� ���ؼ� �̵� ������ �����
        randomDirection += origin;

        //�̵��� ������ ������ ���� �����
        NavMeshHit navHit;

        // NavMesh.SamplePosition�� �Ἥ radomDirection�� ����� �Ž� ��ġ�� ã�� ������ �����ϸ�
        NavMesh.SamplePosition(randomDirection, out navHit, dist, NavMesh.AllAreas);

        //������ �ϼ�
        return navHit.position;
    }

}