﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    public GameObject playerPrefab;
    public Entrance entrance;
    [SerializeField] private float smoothSpeed;
    // Start is called before the first frame update

    private void Awake() {
       
        
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        // target = PlayerController.instance.transform;
      
        
    }

    // Update is called once per frame
   // private void Update()
    //{
    //    transform.position = new Vector3(target.position.x,target.position.y,transform.position.z);
    //}
    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        // transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z),smoothSpeed * Time.deltaTime);
    }
}
