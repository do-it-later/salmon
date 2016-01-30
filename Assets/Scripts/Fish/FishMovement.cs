﻿using UnityEngine;
using System.Collections;

public class FishMovement : MonoBehaviour
{
    //
    public float swimSpeed = 20.0f;
    public float strafeSpeed = 10.0f;

	public Vector3 direction;

	public int playerNumber;
	public bool inControl;

    //
    private bool hasJumped;
	private bool canJump;

    void Start()
    {
        hasJumped = false;
		canJump = false;

		direction.Normalize();
    }

    void Update()
    {
        if (!hasJumped)
        { 
            if (Input.GetKey("a"))
				direction.x = -strafeSpeed;

            if (Input.GetKey("d"))
				direction.x = strafeSpeed;

			if(Input.GetKeyDown(InputHelper.instance.GetInputButtonString(playerNumber, InputHelper.Button.B)))
			{
				if(!hasJumped && canJump)
				{
					hasJumped = true;

					Vector3 camera = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

					direction = camera - transform.position;
					direction.Normalize();
				}
            }
        }

		this.transform.position += direction * Time.deltaTime * swimSpeed;

		if(transform.position.z <= -96)
		{
			ObjectPool.instance.PoolObject(gameObject);
		}
    }

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Jump Box")
		{
			canJump = true;
		}
		else if(other.tag == "Bear")
		{
			gameObject.SetActive(false);
			ObjectPool.instance.PoolObject(gameObject);
			ObjectPool.instance.GetObject("Fish");
			Debug.Log("YAY");
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Jump Box")
		{
			canJump = false;
		}
	}
}
