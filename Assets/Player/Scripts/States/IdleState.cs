using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class IdleState : PlayerState
{
    public IdleState(JWPlayerController _player, string _animName) : base(_player, _animName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

            
        for(int i = 0; i < player.skillKeyCodes.Length; i++)
        {
            if (Input.GetKey(player.skillKeyCodes[i]) && player.playerStat.currentRage > player.playerStat.skillList[i].rageAmount)
            {
                player.stateMachine.ChangeState(player.skill);
            }

            else if(Input.GetKey(player.skillKeyCodes[i]) && player.playerStat.currentRage < player.playerStat.skillList[i].rageAmount)
            {

            }
        }

        if (player.moveDistance <= 0.1f)
        {
            player.nav.ResetPath();
        }

        if (Input.GetMouseButtonDown(1))
        {

            if(!player.isPointerOnObject)
            {
                player.targetPosition = player.pointerPosition;

                player.nav.SetDestination(player.targetPosition);
            }

            else if(player.clickedTarget.TryGetComponent<Coin>(out Coin coin))
            {
                coin.StartCoroutine(coin.GetCoin());
            }

            else if(player.clickedTarget.TryGetComponent<MonsterStateManager>(out MonsterStateManager monster))
            {
                if (player.targetDistance <= player.attackRange)
                {
                    player.stateMachine.ChangeState(player.basicAttack);
                }

                else
                {
                    player.stateMachine.ChangeState(player.moveToTarget);
                }
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            player.stateMachine.ChangeState(player.evade);
        }
    }
}
