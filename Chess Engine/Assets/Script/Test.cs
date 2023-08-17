using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameManager gameManager;
    GameObject[] allGameObjects;

    // Update is called once per frame
    void Update()
    {
        allGameObjects = GameObject.FindObjectsOfType<GameObject>();


    }
}
