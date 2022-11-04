using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_stohasticArrowMovement : MonoBehaviour
{
	public float rotSpeed = 3;
	public float rotRandomPlus = 0.5f;
	public float rotTreshold = 50;

	public float changeRotMin = 1;
	public float changeRotMax = 2;

	public float minSpeed = 0.5f;
	public float maxSpeed = 2;

	public float changeSpeedMin = 0.5f;
	public float changeSpeedMax = 2;


	private float speed = 0;
	private float timeGoesX = 0;
	private float timeGoesY = 0;
	private float timeGoesSpeed = 0;

	private float timeToChangeX = 0.1f;
	private float timeToChangeY = 0.1f;
	private float timeToChangeSpeed = 0.1f;

	private bool xDir = true;
	private bool yDir = true;

	private float curRotSpeedX = 0;
	private float curRotSpeedY = 0;

	private float lendX = 0;
	private float lendY = 0;


	//********CUSTOM
	void RandomizeSpeed ()
	{
	speed=Random.Range(minSpeed, maxSpeed);
	}

	void RandomizeXRot () {
	var rnd=Random.value*rotRandomPlus;
	curRotSpeedX=rotSpeed*rnd;
	}

	void RandomizeYRot () {
	var rnd=Random.value*rotRandomPlus;
	curRotSpeedY=rotSpeed*rnd;
	}



	//********START
	void Start () 
	{
		RandomizeSpeed ();
		if (Random.value>0.5) xDir=!xDir;
		if (Random.value>0.5) yDir=!yDir;

		timeToChangeY=Random.Range(changeRotMin, changeRotMax);
		timeToChangeX=Random.Range(changeRotMin, changeRotMax);
		timeToChangeSpeed=Random.Range(changeSpeedMin, changeSpeedMax);

		curRotSpeedX=Random.Range((rotSpeed),(rotSpeed+rotRandomPlus));
		curRotSpeedY=Random.Range((rotSpeed),(rotSpeed+rotRandomPlus));


	}


	//********UPDATE
	void Update () 
	{
		if (xDir==true) lendX+=Time.deltaTime*curRotSpeedX;
		if (xDir==false) lendX-=Time.deltaTime*curRotSpeedX;
		if (yDir==true) lendY+=Time.deltaTime*curRotSpeedY;
		if (yDir==false) lendY-=Time.deltaTime*curRotSpeedY;

		if (lendX>rotTreshold)
		{
		lendX=rotTreshold;
		xDir=!xDir;
		}

		if (lendX>rotTreshold)
		{
		lendX=-rotTreshold;
		xDir=!xDir;
		}
		if (lendY>rotTreshold)
		{
		lendY=rotTreshold;
		yDir=!yDir;
		}


		if (lendY>rotTreshold)
		{
		lendY=-rotTreshold;
		yDir=!yDir;
		}


		transform.Rotate(lendX*Time.deltaTime, lendY*Time.deltaTime, 0);
		transform.Translate(0, speed*Time.deltaTime, 0);


		//**************
		timeGoesX+=Time.deltaTime;
		timeGoesY+=Time.deltaTime;
		timeGoesSpeed+=Time.deltaTime;

		if (timeGoesX>timeToChangeX)
		{
		xDir=!xDir;
		timeGoesX-=timeGoesX;
		timeToChangeX=Random.Range(changeRotMin, changeRotMax);
		curRotSpeedX=Random.Range((rotSpeed),(rotSpeed+rotRandomPlus));
		}

		if (timeGoesY>timeToChangeY)
		{
		yDir=!yDir;
		timeGoesY-=timeGoesY;
		timeToChangeY=Random.Range(changeRotMin, changeRotMax);
		curRotSpeedY=Random.Range((rotSpeed),(rotSpeed+rotRandomPlus));
		}


		if (timeGoesSpeed>timeToChangeSpeed)
		{
		RandomizeSpeed();
		timeGoesSpeed-=timeGoesSpeed;
		timeToChangeSpeed=Random.Range(changeSpeedMin, changeSpeedMax);
		Debug.Log("hejj");
		}



	}

}

