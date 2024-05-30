using UnityEngine;

public class MonsterChaseState : MonsterBasicState
{

    public override void EnterState(MonsterStateManager monster)
    {
        Debug.Log("���� ���´� �߰� ���¾�!");
    }

    public override void UpdateState(MonsterStateManager monster)
    {
        nav.SetDestination(GameManager.instance.player.transform.position);
    }

    

    public override void OnCollisionEnter(MonsterStateManager monster)
    {

    }
}
