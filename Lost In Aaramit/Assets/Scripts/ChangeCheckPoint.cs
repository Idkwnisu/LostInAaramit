﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCheckPoint : MonoBehaviour {

    public GameObject Allen;

    public GameObject checkPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(checkPoint);
            Allen.GetComponent<PlayerPosition>().position_save();
        }
    }
}
