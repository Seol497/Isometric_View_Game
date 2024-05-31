using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class MonsterIdleState : MonsterBasicState
{
    public override void EnterState(MonsterStateManager monster)
    {
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
        //�÷��̾��� ���ݿ� �������
        //�ǰݻ��·� ��ȯ
        //if (collider.CompareTag("PlayerAttack"))
        //{
        //    Debug.Log("�÷��̾��� ���ݿ� ����. �ǰ� ���·� ��ȯ");
        //    monster.ChangeState(monster.hitState);
        //}
    }

    public override void OnTriggerStay(MonsterStateManager monster, Collider collider)
    {

    }

    public override void OnTriggerExit(MonsterStateManager monster, Collider collider)
    {

    }

    
}