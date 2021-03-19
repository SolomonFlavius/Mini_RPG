using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Dragon//poate fi derivata din dragon sau sa se schimbe ptr inamici mergatori
{
	float specialTimer = .5f;
    // Start is called before the first frame update
    void Update()
    {
    	
        directionTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        if(directionTimer<=0)
        {
        	directionTimer = 1.2f;
        	switch(direction){
        		case 1: direction = 0;
        			break;
        		case 2: direction =1;
        			break;
        		case 3: direction = 2;
        			break;
        		case 0: direction = 3;
        			break;
        		default: direction=1;
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
