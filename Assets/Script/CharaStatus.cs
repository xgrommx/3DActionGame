﻿using UnityEngine;
using System.Collections;

public class CharaStatus : MonoBehaviour {
	public int HP = 100;
	public int MaxHP = 100;
	public int Power = 10;
	public GameObject lastAttackTarget = null;
	public string charactername = "Player";
	public bool attacking = false;
	public bool died = false;
	public bool powerBoost = false;
	float powerBoostTime = 0.0f;

	//アイテム取得
	public void GetItem(DropItem.ItemKind itemKind){
		switch(itemKind){
		case DropItem.ItemKind.Attack:
			powerBoostTime = 10.0f;
			break;
		case DropItem.ItemKind.Heal:
			HP = Mathf.Min (HP + MaxHP / 2, MaxHP);
			break;
		}
	}

	void Update(){
		powerBoost = false;
		if(powerBoostTime > 0.0f){
			powerBoost = true;
			powerBoostTime = Mathf.Max(powerBoostTime - Time.deltaTime, 0.0f);
		}
	}
}
