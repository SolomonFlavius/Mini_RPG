using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
	Animator animator;
	public float speed;//serialize field
	int dir;
	float dirTimer = .7f;
	public int health;
	public GameObject deathParticle;
	bool canAttack;
	float attackTimer = 2f;
	public GameObject projectile;
	public float thrustPower;//serialize field
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dir = Random.Range(0,3);
        canAttack=false;

    }

    // Update is called once per frame
    void Update()
    {
    	
        dirTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        if(dirTimer<=0)
        {
        	dirTimer = .7f;
        	dir = Random.Range(0,3);
        }
        Movement();
        if(attackTimer<=0)
        {
        	attackTimer = 2f;
        	canAttack = true;
        }
        Attack();
    }

    void Attack()
    {
    	if(!canAttack)
    		return;
    	canAttack = false;
    	GameObject newProjectile = Instantiate(projectile,transform.position,transform.rotation);
    	if(dir == 0)
    	{  		
    		newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustPower);
    	}
    	else if(dir == 1)
    	{
    		newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -thrustPower);
    	}
    	else if(dir == 2)
    	{
    		newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * -thrustPower);
    	}
    	else if(dir == 3)
    	{
    		newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustPower);
    	}
    }

    void Movement()
    {
    	if(dir == 0)
    	{
    		transform.Translate(0,speed*Time.deltaTime,0);
    	}
    	else if(dir == 1)
    	{
    		transform.Translate(-speed*Time.deltaTime,0,0);
    	}
    	else if(dir == 2)
    	{
    		transform.Translate(0,-speed*Time.deltaTime,0);
    	}
    	else if(dir == 3)
    	{
    		transform.Translate(speed*Time.deltaTime,0,0);    		
    	}
    	animator.SetInteger("dir" , dir);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
    	if(col.gameObject.tag == "Sword")
    	{
    		health--;
    		col.gameObject.GetComponent<Sword>().CreateParticle();
    		GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
    		GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = true;
    		Destroy(col.gameObject);
    		if(health<=0)//functie
    		{
    			Instantiate(deathParticle, transform.position, transform.rotation);
    			Destroy(gameObject);
    		}
    	}
    }
    void OnCollisionEnter2D(Collision2D col)///sa ia din alt script asta---merge la toti enemies
    {
    	if(col.gameObject.tag == "Player")
    	{
    		health--;//scadem si viata crabului????
    		if(!col.gameObject.GetComponent<Player>().iniFrames)
    		{
    			col.gameObject.GetComponent<Player>().currentHealth--;
    			col.gameObject.GetComponent<Player>().iniFrames = true;
    		}
    		if(health<=0)//functie ptr ASTA
    		{
    			Instantiate(deathParticle, transform.position,transform.rotation);
    			Destroy(gameObject);
    		}
    	}
    	if(col.gameObject.tag == "Wall")//alta functie, merge la toti inamicii
    	{
    		dir--;
    		if(dir<0)
    		{
    			dir = 3;
    		}
    	}
    }
}
