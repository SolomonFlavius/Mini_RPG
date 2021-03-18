using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public float speed;
	Animator animator;
	public Image[] hearts;
	public int maxHealth;
	public int currentHealth;
	public GameObject sword;
	public float thrustPower;
	public bool canMove;
	public bool canAttack;
	public bool iniFrames;//invicibility frames
	SpriteRenderer sr;
	float iniTimer = 1f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        GetHealth();
        canMove=true;
        canAttack = true;
        iniFrames = false;
        sr = GetComponent<SpriteRenderer>();
    }

    void GetHealth()//spatiu de optimizare
    {
    	for(int i = 0 ; i < hearts.Length ; i++)
    	{
    		hearts[i].gameObject.SetActive(false);
    	}

    	for(int i = 0 ; i < currentHealth ; i++)
    	{
    		hearts[i].gameObject.SetActive(true);
    	}
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(Input.GetKeyDown(KeyCode.Space))
    	{
    		Attack();
    	}
        if(currentHealth > maxHealth)//functie separata
        	currentHealth = maxHealth;
        if(iniFrames == true)
        {
        	iniTimer-=Time.deltaTime;
        	int rn = Random.Range(0,100);
        	if(rn<50)
        	{
        		sr.enabled = false;
        	}
        	if(rn>50)
        	{
        		sr.enabled = true;
        	}
        	if(iniTimer<=0)
        	{
        		iniTimer = 1f;
        		iniFrames = false;
        		sr.enabled = true;
        	}
        }


        GetHealth();
    }

    void Attack()
    {
    	if(!canAttack)
    		return;
    	canMove=false;
    	canAttack=false;
    	thrustPower=250;
    	GameObject newSword = Instantiate(sword,transform.position,sword.transform.rotation);
    	if(currentHealth == maxHealth)
    	{
    		newSword.GetComponent<Sword>().special = true;
    		canMove = true;
    		thrustPower = 500;
    	}
    	//Sword Rotation
    	int swordDir = animator.GetInteger("dir");
    	animator.SetInteger("attackDir", swordDir);
    	if(swordDir == 0)
    		{
    			newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustPower);
    			newSword.transform.Rotate(0,0,0);
    		}
    	else if(swordDir == 1)
    		{
    			newSword.transform.Rotate(0,0,180);
    			newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * -thrustPower);
    		}
    	else if(swordDir == 2)
    		{
    			newSword.transform.Rotate(0,0,90);
    			newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -thrustPower);
    		}
    	else if(swordDir == 3)
    		{
    			newSword.transform.Rotate(0,0,-90);
    			newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustPower);
    		}


    }

    void Movement()//ca sa poti merge si pe diagonala scoate else-urile
    {
    	if(!canMove)
    		return;
    	if(Input.GetKey(KeyCode.W))
    	{
    		animator.SetInteger("dir",0);
    		transform.Translate(0,speed * Time.deltaTime , 0);
    		animator.speed = 1;
    	}
    	else if(Input.GetKey(KeyCode.S))
    	{
    		animator.SetInteger("dir",1);
    		transform.Translate(0,-speed * Time.deltaTime , 0);
    		animator.speed = 1;
    	}
    	else if(Input.GetKey(KeyCode.A))
    	{
    		animator.SetInteger("dir",2);
    		transform.Translate(-speed * Time.deltaTime, 0, 0);
    		animator.speed = 1;
    	}
    	else if(Input.GetKey(KeyCode.D))
    	{
    		animator.SetInteger("dir",3);
    		transform.Translate(speed * Time.deltaTime ,0 , 0);
    		animator.speed = 1;
    	}
    	else
    		animator.speed = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

    	if(col.gameObject.tag == "EnemyBullet")
    	{
    		if(!iniFrames)
    		{
    		currentHealth--;
    		iniFrames = true;
    		}
    		col.gameObject.GetComponent<Bullet>().CreateParticle();
    		Destroy(col.gameObject);
    	}
    	if(col.gameObject.tag == "Potion")
    	{
    		Destroy(col.gameObject);
    		if(maxHealth >= 5)
    			{	
    				currentHealth=maxHealth;///sau +1 daca e hard
    				return;
    			}
    		maxHealth++; 
    		currentHealth = maxHealth; //sau +1 daca e hard
    		
    				
    	}
    }
}
