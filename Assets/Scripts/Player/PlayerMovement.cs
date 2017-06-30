using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundry
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerMovement : MonoBehaviour
{
    public float playerMovementSpeed = 0.0f;

    private GameObject player = null;
    private Rigidbody2D playerRigidBody;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidBody = player.GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        Movement();
        ProjectileFire();
    }

    private void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        playerRigidBody.velocity = new Vector2(moveHorizontal, moveVertical) * playerMovementSpeed;
        playerRigidBody.velocity.Normalize();

    }

    private void ProjectileFire()
    {

    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
