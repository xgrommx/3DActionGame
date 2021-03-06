﻿using UnityEngine;
using System.Collections;
using Cradle.FM;

namespace Cradle.FM{
	public class AttackState : FSMState {

		public AttackState(Transform[] wp)
		{
			SetWayPoints (wp);
			SetStateID (FSMStateID.Attacking);
			SetRotSpeed (360.0f);
			FindNextPoint ();
		}


		public override void Reason(Transform player, Transform npc){
			//プレイヤーとの距離を確認
			SetDist (Vector3.Distance (npc.position, player.position));
			if(CheckDist(dist, 0.0f, 5.0f ))
			{
				//ターゲット地点に回転
				if(IsTestScene())
				SetRot (npc, npc.position);
				
				Debug.Log("Switch to Approach State");
				npc.GetComponent<EnemyCtrl>().SetTransition(Transition.SawPlayer);
			}
			
			//距離が遠すぎる場合
			else if(GreaterThanCheckReach(dist, 10.0f))
			{
				Debug.Log("Switch to Search State");
				npc.GetComponent<EnemyCtrl>().SetTransition(Transition.LostPlayer);
			}
		}
		
		public override void Act(Transform player, Transform npc){
			//ターゲット地点をプレーヤーポジションに設定
			SetDest (player);
			
			//攻撃
			npc.GetComponent<EnemyCtrl> ().AttackStart ();
		}

	}
}