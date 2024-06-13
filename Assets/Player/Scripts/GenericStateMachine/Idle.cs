using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : AvatorState
{
    public Idle(StateMachineAvatar _avatar, string _animParameter) : base(_avatar, _animParameter)
    {
    }

    public void SetDestination(Vector3 _targetPosition) // ��ǥ ��ġ �������ּ���
    {
        targetPosition = _targetPosition;
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
        
        if(targetPosition != Vector3.zero)
        {
            avatar.navMeshAgent.SetDestination(targetPosition);
        }
    }
}
