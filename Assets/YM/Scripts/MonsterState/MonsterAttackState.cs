using UnityEngine;
using UnityEngine.UI;

public class MonsterAttackState : MonsterBasicState
{
    float attacktime;  //�����ϴ� �ð�
    
    public override void EnterState(MonsterStateManager monster)
    {
        monster.ani.SetBool("Attack", true);
        monster.attackArea.enabled = true;
        attacktime = 2f;
    }

    public override void UpdateState(MonsterStateManager monster)
    {

    }

    public override void ExitState(MonsterStateManager monster)
    {
        monster.ani.SetBool("Attack", false);
        monster.attackArea.enabled = false;
    }

    public override void OnTriggerEnter(MonsterStateManager monster, Collider collider)
    {
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
        if (collider.CompareTag("Player"))
        {
            monster.ChangeState(monster.chaseState);
        }
    }

    
}
