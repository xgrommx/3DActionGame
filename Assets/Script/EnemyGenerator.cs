﻿using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {
	//生成される敵
	public GameObject enemyPrefab;
	GameObject[] existEnemys;
	//アクティブの最大数
	public int maxActive = 2;

	void Start () {
		existEnemys = new GameObject[maxActive];
		StartCoroutine (Exec());
	}

	IEnumerator Exec(){
		while(true){
			//ホストが管理するように
			if(Network.isServer)
			Generate();
			yield return new WaitForSeconds( 8.0f );
		}
	}

	void Generate(){
		for(int enemyCount = 0; enemyCount < existEnemys.Length; ++ enemyCount){
			if(existEnemys[enemyCount] == null){
				//敵作成
				existEnemys[enemyCount] = Network.Instantiate(enemyPrefab, transform.position, transform.rotation, 0) as 
					GameObject;
				return;
			}
		}
	}
}
