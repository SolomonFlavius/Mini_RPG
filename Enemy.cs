using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
public int health;
[SerializeField]
protected GameObject deathParticle;
[SerializeField]
protected float directionTimer;
[SerializeField]
protected float speed;
[SerializeField]
protected int direction;


protected void PlayerCollision(Collision2D col)
{
	if(col.gameObject.tag == "Player")
    	{
    		health--;//scadem si viata crabului????  DA DOAR PE EZ
    		if(!col.gameObject.GetComponent<Player>().iniFrames)
    		{
    			col.gameObject.GetComponent<Player>().currentHealth--;
    			col.gameObject.GetComponent<Player>().iniFrames = true;
    		}
    		HealthTest();
    	}
}

protected void HealthTest()
{
	if(health<=0)
    		{
    			Instantiate(deathParticle, transform.position,transform.rotation);
    			Destroy(gameObject);
    		}
}

protected void WallCollision(Collision2D col)
{
	if(col.gameObject.tag == "Wall")
    	{
           // Debug.Log("Ceva");
    		switch(direction){
                case 1: direction = 2;
                    break;
                case 2: direction = 1;
                    break;
                case 3: direction = 0;
                    break;
                case 0: direction = 3;
                    break;
                default: direction=1;
                    break;
            }
    	}
}
protected void SwordCollider(Collider2D col)
{
    if(col.gameObject.tag == "Sword")
        {
            health--;
            col.gameObject.GetComponent<Sword>().CreateParticle();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = true;
            Destroy(col.gameObject);
            HealthTest();
        }
}
}
