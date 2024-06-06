using UnityEngine;

public class MonsterDeadState : MonsterBasicState
{
    public override void EnterState(MonsterStateManager monster)
    {
        monster.ani.enabled = false; // �ִϸ����͸� ��Ȱ��ȭ�մϴ�.
        monster.ragdoll.SetRagdollActive(true);
        monster.bodyCollider.enabled = false;
        monster.rigid.isKinematic = true;
        monster.nav.enabled = false;
        monster.isDead = true;
    }

    public override void UpdateState(MonsterStateManager monster)
    {
        if (monster.deadCount > 0)
        {
            monster.deadCount -= Time.deltaTime;
        }
        else
        {
            monster.gameObject.SetActive(false);
        }
    }

    public override void ExitState(MonsterStateManager monster)
    {
        
    }

}
