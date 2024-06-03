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

}
