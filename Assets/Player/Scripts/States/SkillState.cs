using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkillState : PlayerState
{
    public SkillState(JWPlayerController _player, string _animParameter) : base(_player, _animParameter)
    {
    }

    public int index;

    public override void Enter()
    {
        base.Enter();

        for (int i = 0; i < player.skillKeyCodes.Length; i++)
        {
            if (Input.GetKeyDown(player.skillKeyCodes[i]))
            {
                index = i;
                player.animator.SetBool(player.skillNames[i], true);
            }
        }

        if (player.playerStat.skillList[index].rageUseType == 1)
        {
            player.playerStat.currentRage -= player.playerStat.skillList[index].rageAmount;
        }
    }

    public override void Exit()
    {
        player.animator.SetBool(player.skillNames[index], false);


        base.Exit();
    }
    float elapsedTime = 0;
    public override void Update()
    {
        base.Update();

        if (player.playerStat.currentRage < player.playerStat.skillList[index].rageAmount)
        {
            machine.ChangeState(player.idle);
        }

        if (player.playerStat.skillList[index].rageUseType == 2)
        {
            elapsedTime += Time.deltaTime;

            if(elapsedTime >= 0.5f)
            {
                elapsedTime -= 0.5f;
                player.playerStat.currentRage -= player.playerStat.skillList[index].rageAmount;
            }
        }

        if (player.animTrigger)
        {
            player.stateMachine.ChangeState(player.idle);
        }
    }
    public override void FixedUpdate()
    {
        if (player.damageTrigger)
        {
            if (index == 1)
            {
                EffectCircle();
                player.damageTrigger = false;
            }
            else if (index == 0)
            {
                EffectSquare();
                player.damageTrigger = false;
            }
        }
    }

    void EffectCircle()
    {

        int targets = Physics.OverlapSphereNonAlloc(player.skillBases[index].position, player.skillRangeRadiuses[index], player.targetsInAttackRange, LayerMask.GetMask("Enemy"));

        for (int i = 0; i < targets; i++)
        {

            if (player.targetsInAttackRange[i].TryGetComponent<MonsterStateManager>(out MonsterStateManager monster))
            {
                monster.isHit = true;

                player.DamageToEnemy(monster, player.playerStat.Damage(10));

                    Vector3 numberPosition = monster.transform.position + new Vector3(0, 2, 0);
                    
                    monster.isHit = true;

                DamageNumber damage = player.damageNumber.Spawn(numberPosition, player.playerStat.Damage(10));

                CameraShake.Instance.Shake(0.5f, 0.5f);
            }

        }

    }

    void EffectSquare()
    {

        int targets = Physics.OverlapBoxNonAlloc(
            player.jumpAttackPoint.position,
            player.jumpAttackSize / 2, // OverlapBox의 반사이즈를 전달
            player.targetsInAttackRange,
            player.jumpAttackPoint.rotation, // 점프 공격 포인트의 회전을 사용
            LayerMask.GetMask("Enemy"));

        for (int i = 0; i < targets; i++)
        {
            if (player.targetsInAttackRange[i].TryGetComponent<MonsterStateManager>(out MonsterStateManager monster))
            {
                monster.isHit = true;

                player.DamageToEnemy(monster, player.playerStat.Damage(10));

                Debug.Log(player.playerStat.Damage(10));

                Vector3 numberPosition = monster.transform.position + new Vector3(0, 2, 0);

                DamageNumber damage = player.damageNumber.Spawn(numberPosition, player.playerStat.Damage(10));

                CameraShake.Instance.Shake(0.5f, 0.5f);
            }

        }

    }

}
