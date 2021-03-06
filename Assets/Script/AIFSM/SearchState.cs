﻿using UnityEngine;
using System.Collections;
using Cradle.FM;

namespace Cradle.FM{
	public class SearchState : FSMState {

		public SearchState(Transform[] wp)
		{
			SetWayPoints (wp);
			SetStateID(FSMStateID.Searching);
		}


		public override void Reason(Transform player, Transform npc)
		{
			if(LessThanCheckReach(Vector3.Distance(npc.position, player.position), 7.0f))
			{
				Debug.Log("Switch to Approach State");
				npc.GetComponent<EnemyCtrl>().SetTransition(Transition.SawPlayer);
			}
		}
		
		public override void Act(Transform player, Transform npc)
		{
			//ターゲットを見失ったらパトロール
			npc.GetComponent<EnemyCtrl>().Walking(destPos);
			
			//ターゲット地点が遠すぎる場合、パトロール地点を再度設定
			if(GreaterThanCheckReach(Vector3.Distance(npc.position, destPos), 50.0f))
			{
				Debug.Log("Reached to the destination point/ncalculating the next point");
				FindNextPoint();
			}
			
			//ターゲット地点に到着した場合に、パトロール地点を再度設定
			if(LessThanCheckReach(Vector3.Distance(npc.position, destPos), 0.6f))
			{
				Debug.Log("Reached to the destination point/ncalculating the next point");
				FindNextPoint();
			}
		}

	}
}