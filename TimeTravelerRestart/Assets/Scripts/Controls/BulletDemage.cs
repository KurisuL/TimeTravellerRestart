﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDemage : MonoBehaviour {

    // 子弹碰撞进入检测后，attack，再调取player的takedemage
    // 就是调用另一脚本的函数
    // 写得不好，要两种子弹
    public int attackDamage;

    //GameObject victim;   
    private string shooter;
    private string hitID;

    void OnTriggerEnter(Collider other)
    {
        // 因为player里的cube也有个collider，就用个if了
        if (other.gameObject.GetComponent<SimpleMove>())
        {
            hitID = other.gameObject.GetComponent<SimpleMove>().playerID;
            shooter = gameObject.GetComponent<BulletMove>().GetParentName();

            if (hitID == shooter)
            {
                Debug.Log("noHurt");
            }
            else
            {
                //victim = other.gameObject;
                Debug.Log("hurt");
                Attack(hitID, attackDamage);
            }
        }
    }

    void Attack(string hitID,int attackDamage)
    {
        // Reset the timer.
        //timer = 0f;

        // If the player has health to lose...
        //if (playerHealth.currentHealth > 0)
        //{
        // ... damage the player.
        Destroy(gameObject);
        PlayerStatusController.playerHealth[hitID] -= attackDamage;
        Debug.Log(PlayerStatusController.playerHealth[hitID]);

        // PlayerStatusController.playerPower[this.GetComponent<SimpleMove>().playerID] -= Time.deltaTime * PlayerStatusController.playerPowerConsumeSpeed[this.GetComponent<SimpleMove>().playerID];
        //}
    }
}
