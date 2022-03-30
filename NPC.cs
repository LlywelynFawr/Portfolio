using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string title;
    public Color color;
    public string weapon;
    private Transform target;

    private void Awake()
    { 
       target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();   
    }  

    private void Update() {
        if(target != null)
        {
            this.transform.LookAt(target);
        }
    }
}
