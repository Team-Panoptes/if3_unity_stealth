using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ClosestEnemyUpdater : MonoBehaviour
{

    TMP_Text enemyText;
    Transform player;
    GameObject[] guards;


    // Start is called before the first frame update
    void Start()
    {
        enemyText = GetComponent<TMP_Text>();
        player = GameObject.FindWithTag("Player").transform;
        guards = GameObject.FindGameObjectsWithTag("Guard");
    }

    // Update is called once per frame
    void Update()
    {
        float minDistance = Mathf.Infinity;

        foreach(GameObject guard in guards)
        {
            float distance = Vector3.Distance(player.position, guard.transform.position);
            if (distance < minDistance) {
                minDistance = distance;
            }
        }
        enemyText.SetText($"Closest enemy : {minDistance:f}");   
    }
}
