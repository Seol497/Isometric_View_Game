using UnityEngine;

public class MonsterDeadState : MonsterBasicState
{
    public override void EnterState(MonsterStateManager monster)
    {
        // monster.ani.SetBool("Dead", true);
        monster.ani.enabled = false; // �ִϸ����͸� ��Ȱ��ȭ�մϴ�.

        // ��� ���� ǥ��
        monster.isDead = true;

        monster.ragdoll.SetRagdollActive(true);
        monster.bodyCollider.enabled = false;
        monster.rigid.isKinematic = true;
        monster.nav.enabled = false;
        GameManager.instance.CoinSpawn(monster.transform.position);
        ScoreManager.instance.killedEnemyCount++;
        GameManager.instance.player.currentExp += monster.exp;
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
