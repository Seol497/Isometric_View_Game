using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class MonsterIdleState : MonsterBasicState
{
    public override void EnterState(MonsterStateManager monster)
    {
        Debug.Log("���� ���´� idle�̾�");
        monster.nav.isStopped = true;
    }

    public override void UpdateState(MonsterStateManager monster)
    {
        if (monster.isChase == true)
        {
            monster.ChangeState(monster.chaseState);
        }
    }

    public override void ExitState(MonsterStateManager monster)
    {

    }

    public override void OnTriggerEnter(MonsterStateManager monster, Collider collider)
    {

    }

    public override void OnTriggerExit(MonsterStateManager monster, Collider collider)
    {

    }

}