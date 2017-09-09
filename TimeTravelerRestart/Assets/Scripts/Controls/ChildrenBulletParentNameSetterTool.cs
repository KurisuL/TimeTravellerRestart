﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenBulletParentNameSetterTool : MonoBehaviour {

	public void SetParentName(string name) {
        BulletMove[] childrenBullets = GetComponentsInChildren<BulletMove>();
        for(int i = 0; i < childrenBullets.Length; i++) {
            childrenBullets[i].SetParentName(name);
        }
    }
}
