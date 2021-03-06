﻿using UnityEngine;
using System.Collections;
using Cradle;
using Cradle.FM;
using Cradle.DesignPattern;

namespace Cradle{
	public class GameRuleSettings : MonoBehaviour, IRuleController {
			public AudioClip clearSeClip;
			AudioSource clearSeAudio;
			public GameRuleSettingsController controller;
			private SceneManager manager;
			
			public void OnEnable() {
				controller.SetRuleController (this);
			}

			
			void Start(){
					//ゲームスピード初期化
					controller.InitializeGameSpeed ();
					//オーディオの初期化
					FindAudioComponent ();
					DisableLoopSE ();
					ClipSE ();
			}


			void Update () {
				//ゲームオーバー、クリア後、タイトルへ
				controller.ReturnTitle ();
			}


			public void GameOver(){
				controller.SetGameOver (true);
				Debug.Log ("GameOver");
			}

			public void GameClear(){
				controller.SetGameClear (true);
				Debug.Log ("GameClear");
				PlaySE ();
			}

			public void FindAudioComponent(){
				this.clearSeAudio = gameObject.AddComponent<AudioSource>();
			}

			public void FindSceneComponent(){
				this.manager = FindObjectOfType <SceneManager> ();
				manager.SwitchState (new TitleState(manager));
			}

			public void DisableLoopSE(){
				this.clearSeAudio.loop = false;
			}

			public void ClipSE(){
				this.clearSeAudio.clip = clearSeClip;
			}

			public void PlaySE(){
				this.clearSeAudio.Play ();
			}

		}
}