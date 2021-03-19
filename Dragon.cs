using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
	Animator animator;
	protected bool canAttack;//rangeEnemy
	protected float attackTimer = 2f;//rangeEnemy
    [SerializeField]
	protected GameObject projectile;
    [SerializeField]
	protected float thrustPower;
    // Start is called before the first frame update
    protected void Start()
    {
        animator = GetComponent<Animator>();
        direction = Random.Range(0,3);
        canAttack=false;

    }

    // Update is called once per frame
    protected void Update()
    {
    	
        directionTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        if(directionTimer<=0)
        {
        	directionTimer = .7f;
        	direction = Random.Range(0,3);
        }
        Movement();
        if(attackTimer<=0)
        {
        	attackTimer = 2f;
        	canAttack = true;
        }
        Attack();
    }

    protected void Attack()
    {
    	if(!canAttack)
    		return;
    	canAttack = false;
    	GameObject newProjectile = Instantiate(projectile,transform.position,transform.rotation);
    	if(direction == 0)
    	{  		
    		newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustPower);
    	}
    	else if(direction == 1)
    	{
    		newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -thrustPower);
    	}
    	else if(direction == 2)
    	{
    		newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * -thrustPower);
    	}
    	else if(direction == 3)
    	{
    		newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustPower);
    	}
    }

    protected void Movement()
    {
    	if(direction == 0)
    	{
    		transform.Translate(0,speed*Time.deltaTime,0);
    	}
    	else if(direction == 1)
    	{
    		transform.Translate(-speed*Time.deltaTime,0,0);
    	}
    	else if(direction == 2)
    	{
    		transform.Translate(0,-speed*Time.deltaTime,0);
    	}
    	else if(direction == 3)
    	{
    		transform.Translate(speed*Time.deltaTime,0,0);    		
    	}
    	animator.SetInteger("dir" , direction);
    }
    protected void OnTriggerEnter2D(Collider2D col)
    {
    	SwordCollider(col);
    }
    protected void OnCollisionEnter2D(Collision2D col)
    {
        PlayerCollision(col);
        WallCollision(col);    
    }
}
