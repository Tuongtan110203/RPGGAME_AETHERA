﻿using System.Collections;
using UnityEngine;

namespace Assets.Script.Enemy.NightBorne
{
    public class nightBorneStunState : EnemyState
    {
        Enemy_NightBorne enemy;
        public nightBorneStunState(global::Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_NightBorne _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
        {
            enemy = _enemy; 
        }

        public override void Enter()
        {
            base.Enter();

            //enemy.fx.InvokeRepeating("RedColorBlink", 0, .1f);

            stateTimer = enemy.stunDuration;

            rb.linearVelocity = new Vector2(-enemy.facingDir * enemy.stunDirection.x, enemy.stunDirection.y);
        }

        public override void Exit()
        {
            base.Exit();

            //enemy.fx.Invoke("CancelColorChange", 0);
        }

        public override void Update()
        {
            base.Update();

            if (stateTimer < 0)
                stateMachine.ChangeState(enemy.idleState);
        }
    }
}