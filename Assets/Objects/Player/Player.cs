﻿using System.Collections;using System.Collections.Generic;using UnityEngine;public class Player : SpawnableObject, IMoveable {    private float _movementSpeed;    public float movementSpeed {        get { return _movementSpeed; }        set { _movementSpeed = value; }    }    public void Move(Vector2 direction) {        rigidBody.AddForce(direction * Time.deltaTime * movementSpeed, ForceMode2D.Impulse);    }    // Use this for initialization    void Start () {        movementSpeed = 50.0f; // dummy speed	}		// Update is called once per frame	void Update () {			}}