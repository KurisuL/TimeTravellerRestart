﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SkillShoot))]

public class SkillShootTeleport : SkillAbstract  {

    private GameObject exchangeBullet;

    public override void ReleaseSkill(){
        exchangeBullet = this.GetComponent<SkillShoot>().lastBullet;
        //当无子弹时，重置冷却时间为0
        if (exchangeBullet == null) {
            Debug.Log("no bullet can exchange position, skill [" + skillName + "] can't trigger!");
            coldDownTimeLeft = 0.0f;
            return;
        }
        //使触发此技能的玩家的position与rotation与子弹的相同
        transform.position = exchangeBullet.transform.position;
        Vector3 eulerAngles = exchangeBullet.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0);
        //转移比较突兀，需要加入动画效果
        Destroy (exchangeBullet);
	}
}
