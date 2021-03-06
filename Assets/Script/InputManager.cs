﻿using UnityEngine;
using System;
using System.Collections;
using Cradle;
using Cradle.Resource;

namespace Cradle{
	public class InputManager : MonoBehaviour, IInputController {
		
		public InputManagerController inputController;
		
		public void OnEnable() {
			inputController.SetInputController (this);
		}


		void Update () {
			//スライド開始地点
			inputController.SlideStart ();				

			try{
			//画面の一割以上移動させたらスライド開始
			inputController.Sliding (true);
			
			//スライド操作が終了したか
			inputController.StopSlide (false);

			}catch(ArgumentOutOfRangeException e){
				Debug.Log("SaveErrorLog : " + e);
				TextReadWriteManager write = new TextReadWriteManager();
				write.WriteTextFile(Application.dataPath + "/" + "ErrorLog_Cradle.txt", e.ToString());
			}

			//移動量を求める
			inputController.Moved ();
			
			//カーソル位置を更新
			inputController.PrevPosition ();
		}
		
		//クリックされたか
		public bool Clicked(){
			return inputController.IsClicking ();
		}
		
		//スライド時のカーソルの移動量
		public Vector2 GetDeltaPosition(){
			return inputController.GetDeltaPosition();
		}
		
		//スライド中か
		public bool Moved(){
			return inputController.IsMoved();
		}
		
		public Vector2 GetCursorPosition()
		{
			return inputController.GetCursorPosition();
		}

		public bool InputGetButtonFire1(){
			return Input.GetButton("Fire1");		
		}
		
		public bool InputGetButtonUpFire1(){
			return Input.GetButtonUp("Fire1");		
		}
		
		public bool InputGetButton(){
			return Input.GetButton("Fire2");		
		}
		
		public bool InputGetButtonDown(){
			return Input.GetButtonDown("Fire2");		
		}
		
		public bool InputGetButtonUp(){
			return Input.GetButtonUp("Fire2");		
		}

	}
}
