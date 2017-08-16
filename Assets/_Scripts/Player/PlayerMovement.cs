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

    public Boundry boundry;

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
    }

    private void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        playerRigidBody.velocity = new Vector2(moveHorizontal, moveVertical) * playerMovementSpeed;
        playerRigidBody.velocity.Normalize();

        playerRigidBody.position = new Vector3
        (
            Mathf.Clamp(playerRigidBody.position.x, boundry.xMin, boundry.xMax),
            Mathf.Clamp(playerRigidBody.position.y, boundry.yMin, boundry.yMax),
            0.0f
        );
    }
}