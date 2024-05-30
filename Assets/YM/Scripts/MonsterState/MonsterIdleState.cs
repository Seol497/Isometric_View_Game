using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class MonsterIdleState : MonsterBasicState
{
    public override void EnterState(MonsterStateManager monster)
    {
        Debug.Log("���� ���´� idle�̾�!");
    }

    public override void UpdateState(MonsterStateManager monster)
    {
        monster.ChangeState(monster.deadState);
    }


    public override void OnCollisionEnter(MonsterStateManager monster)
    {
        
    }

}

