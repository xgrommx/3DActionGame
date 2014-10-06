﻿﻿using UnityEngine;
using System.Collections;
using Cradle.FM;

namespace Cradle.FM{
	public class ApproachState : FSMState {
		
		public ApproachState(Transform[] wp)
		{
			waypoints = wp;
			stateID = FSMStateID.Approaching;
			RotSpeed = 360.0f;
			FindNextPoint ();
		}
		
		public override void Reason(Transform player, Transform npc)
		{
			destPos = player.position;
			
			float dist = Vector3.Distance (npc.position, destPos);
			if(dist <= 2.0f)
			{
				Debug.Log("Switch to Attack state");
				npc.GetComponent<EnemyCtrl>().SetTransition(Transition.ReachPlayer);
			}
			else if(dist >= 10.0f)
			{
				Debug.Log("Switch to Search state");
				npc.GetComponent<EnemyCtrl>().SetTransition(Transition.LostPlayer);
			}
		}
		
		public override void Act(Transform player, Transform npc)
		{
			//攻撃フラグを初期化
			npc.GetComponent<EnemyCtrl> ().StateStartCommon ();
			
			destPos = player.position;
			
			Quaternion targetRotation = Quaternion.LookRotation (destPos - npc.position);
			npc.rotation = Quaternion.Slerp (npc.rotation, targetRotation, Time.deltaTime * RotSpeed);
			
			//目的地をプレイヤーに変更
			npc.GetComponent<CharaMove> ().SendMessage ("SetDestination", destPos);
		}
	}
}