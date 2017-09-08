﻿using UnityEngine;

//TODO：目前玩家信息和Move脚本放在一起，以后修改
public class SimpleMove : MonoBehaviour {

	[Tooltip("玩家ID")]
	public string playerID = "1";

	public float moveSpeed = 45.0f;
	public float rotateSpeed = 6.0f;

    //玩家开始时自动向GameController注册自身信息，若名称出现重复，则GameController会随机分配给玩家一个ID
    private void Start() {
        PlayerStatusController.RegisterPlayer(this);
    }

    void Update () {
		Move ();
	}
    //玩家销毁时自动向GameController销毁自身信息
    private void OnDestroy() {
        PlayerStatusController.RemovePlayer(playerID);
    }

    void Move(){

		float inputX = Input.GetAxisRaw ("Horizontal" + playerID);	// 临时的x方向值
		float inputZ = Input.GetAxisRaw ("Vertical" + playerID);	// 临时的z方向值
		
		// 如果输入的位置接近于0，那么不进行转向（否则LookRotation会抛警告）
		if (inputX * inputX + inputZ * inputZ > 0.0025f) {
            Quaternion fromQuaternion = transform.rotation;
            Quaternion toQuaternion = Quaternion.LookRotation (new Vector3 (inputX, 0, inputZ));
			transform.rotation = Quaternion.Lerp (fromQuaternion, toQuaternion, Time.deltaTime * rotateSpeed);

			// 当当前方向和目标方向夹角过大时，只转向，不进行移动
            // 180是不是每次都需要跳进来？
			if (Quaternion.Angle (fromQuaternion, toQuaternion) < 180) {
                // 为什么要用InverseTransformVector，因为转完之后Translate移动的方向也变了
                // 所以需要从局部的坐标映射回全局的坐标
                transform.Translate(transform.InverseTransformVector(Time.deltaTime * moveSpeed * new Vector3(inputX, 0, inputZ) * (int)(PlayerStatusController.playerShootSpeed.ContainsKey(playerID) ? PlayerStatusController.playerMoveSpeed[playerID] : 1)));
			}
		}	
	}
}
