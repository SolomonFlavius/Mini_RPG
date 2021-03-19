using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : Enemy
{
	SpriteRenderer spriteRenderer;
    [SerializeField]
	Sprite facingUp;
    [SerializeField]
	Sprite facingDown;
    [SerializeField]
	Sprite facingRight;
    [SerializeField]
	Sprite facingLeft;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //direction = Random.Range(0,3);
    }

    // Update is called once per frame
    void Update()
    {
        directionTimer -= Time.deltaTime;
        if(directionTimer<=0)
        {
        	directionTimer = 1.5f;
        	direction = Random.Range(0,3);
        }
        Movement();
    }

    void Movement()
    {
    	if(direction == 0)
    	{
    		transform.Translate(0,-speed * Time.deltaTime,0);
    		spriteRenderer.sprite = facingDown;
    	}
    	else if(direction == 1)
    	{
    		transform.Translate(-speed * Time.deltaTime,0,0);
    		spriteRenderer.sprite = facingLeft;
    	}
    	else if(direction == 2)
    	{
    		transform.Translate(speed * Time.deltaTime,0,0);
    		spriteRenderer.sprite = facingRight;
    	}
    	else if(direction == 3)
    	{
    		transform.Translate(0,speed * Time.deltaTime,0);
    		spriteRenderer.sprite = facingUp;
    	}

    }

    void OnTriggerEnter2D(Collider2D col)
    {
    	SwordCollider(col);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
    	PlayerCollision(col);
    	WallCollision(col);    
    }
}
