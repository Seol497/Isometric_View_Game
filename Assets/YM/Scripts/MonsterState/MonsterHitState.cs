using UnityEngine;

public class MonsterHitState : MonsterBasicState
{
    public override void EnterState(MonsterStateManager monster)
    {
        Debug.Log("������ �ǰ� ���¾�");
        monster.ani.SetTrigger("Hit");
    }

    public override void UpdateState(MonsterStateManager monster)
    {
        //�÷��̾� ���ݸ�ŭ ü�� ����
        //���� ü���� 0���Ϸ� �������� ������·� ����
        if (monster.currentHp <= 0)
        {
            monster.ChangeState(monster.deadState);
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
