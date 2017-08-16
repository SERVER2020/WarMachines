using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    private float fireTimer = 0.0f;
    public float fireSpeed = 0.0f;

    private int projectilePooledAmount = 10;

    private bool needMoreProjectiles = true;

    public GameObject playerProjectile = null;
    public GameObject firePoint = null;

    private List<GameObject> playerProjectiles;

    // Use this for initialization
    void Start ()
    {
        playerProjectiles = new List<GameObject>();
        for(int i = 0; i < projectilePooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(playerProjectile);
            obj.SetActive(false);
            playerProjectiles.Add(obj);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        ProjectileFire();
    }

    private void ProjectileFire()
    {
        if (fireTimer >= 0.0f)
        {
            fireTimer -= Time.deltaTime;
        }

        if (Input.GetButton("Shoot") && fireTimer <= 0.0f)
        {
            for(int i = 0; i < playerProjectiles.Count; i++)
            {
                if (!playerProjectiles[i].activeInHierarchy)
                {
                    playerProjectiles[i].transform.position = firePoint.transform.position;
                    playerProjectiles[i].transform.rotation = firePoint.transform.rotation;
                    playerProjectiles[i].SetActive(true);
                    break;
                }
            }
            fireTimer = fireSpeed;
        }
    }
}
