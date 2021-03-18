using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour//poate fi derivata din dragon sau sa se schimbe ptr inamici mergatori
{
	Animator animator;
	public float speed;//serialize field
	public int dir;
	float dirTimer = 1f;
	public int health;
	public GameObject deathParticle;
	bool canAttack;
	float attackTimer = 2f;
	public GameObject projectile;
	public float thrustPower;//serialize field
	float specialTimer = .5f;
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
        	dirTimer = 1.2f;
        	switch(dir){
        		case 1: dir = 0;
        			break;
        		case 2: dir =1;
        			break;
        		case 3: dir = 2;
        			break;
        		case 0: dir = 3;
        			break;
        		default: dir=1;
        			break;
        	}

        }
        specialTimer -=Time.deltaTime;
        	if(specialTimer <= 0)
        	{
        		SpecialAttack();
        		SpecialAttack();
        		specialTimer =.5f;
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

    void SpecialAttack()
    {
    	GameObject newProjectile =Instantiate(projectile, transform.position, transform.rotation);
    	int randomDir = Random.Range(0,3);
    	switch(randomDir)
    	{
    		case 0: newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustPower);
    			break;
    		case 1: newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustPower);
    			break;
    		case 2: newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -thrustPower);
    			break;
    		case 3: newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * -thrustPower);
    			break;
    	}
    }
}
