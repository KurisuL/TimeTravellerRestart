﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpeedUp : TimeLimitedSkillAbstract {
    [Header("设定倍率")]
    public float shootSpeed = 1.0f;
    public float moveSpeed = 1.0f;
    public float bulletSpeed = 1.0f;
	public float simulationSpeed = 1.0f;

    private float oldShootSpeed = 1.0f;
    private float oldMoveSpeed = 1.0f;
    private float oldBulletSpeed = 1.0f;
	private float oldSimulationSpeed = 1.0f;

	private ParticleSystem ps;

	void Start() {
		ps = GetComponentInChildren<ParticleSystem> ();
		if (ps == null) {
			Debug.LogError ("no ParticleSystem found!");
		}
	}

    public override void BeforeSkill() {
        //base.BeforeSkill();
        oldShootSpeed = (int)PlayerStatusController.playerShootSpeed[this.GetComponent<SimpleMove>().playerID];
        oldMoveSpeed = (int)PlayerStatusController.playerMoveSpeed[this.GetComponent<SimpleMove>().playerID];
        oldBulletSpeed = (int)PlayerStatusController.playerBulletSpeed[this.GetComponent<SimpleMove>().playerID];
        oldPowerConsumeSpeed = (int)PlayerStatusController.playerPowerConsumeSpeed[this.GetComponent<SimpleMove>().playerID];
		oldSimulationSpeed = ps.main.simulationSpeed;
		//oldPlayer
        PlayerStatusController.playerShootSpeed[this.GetComponent<SimpleMove>().playerID] = oldShootSpeed * shootSpeed;
        PlayerStatusController.playerMoveSpeed[this.GetComponent<SimpleMove>().playerID] = oldMoveSpeed * moveSpeed;
        PlayerStatusController.playerBulletSpeed[this.GetComponent<SimpleMove>().playerID] = oldBulletSpeed * bulletSpeed;
        PlayerStatusController.playerPowerConsumeSpeed[this.GetComponent<SimpleMove>().playerID] = powerConsumeSpeed;
		var main = ps.main;
		main.simulationSpeed = oldSimulationSpeed * simulationSpeed;
		main.startLifetimeMultiplier = 3.0f;
    }

    public override void AfterSkill() {
        //base.AfterSkill();
        PlayerStatusController.playerShootSpeed[this.GetComponent<SimpleMove>().playerID] = oldShootSpeed;
        PlayerStatusController.playerMoveSpeed[this.GetComponent<SimpleMove>().playerID] = oldMoveSpeed;
        PlayerStatusController.playerBulletSpeed[this.GetComponent<SimpleMove>().playerID] = oldBulletSpeed;
        PlayerStatusController.playerPowerConsumeSpeed[this.GetComponent<SimpleMove>().playerID] = oldPowerConsumeSpeed;
		var main = ps.main;
		main.simulationSpeed = oldSimulationSpeed;
		main.startLifetimeMultiplier = 1.0f;
    }

    public override void AdditionalUpdate()
    {
        base.AdditionalUpdate();
        PlayerStatusController.playerPower[this.GetComponent<SimpleMove>().playerID] -= Time.deltaTime * PlayerStatusController.playerPowerConsumeSpeed[this.GetComponent<SimpleMove>().playerID];
    }

}
