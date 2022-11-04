using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_TransRimShaderIrisator : MonoBehaviour
{
	public float topStr = 2;
	public float botStr = 1;
	public float minSpeed = 1;
	public float maxSpeed = 1;
	private float speed = 0;
	private float timeGoes = 0;
	private bool timeGoesUp = true;

	//********CUSTOM
	void RandomizeSpeed ()
	{
	speed=Random.Range(minSpeed, maxSpeed);
	}

	//********START
	void Start () {
	timeGoes=botStr;
	speed=Random.Range(minSpeed, maxSpeed);
	}


	//********UPDATE
	void Update () {

	if (timeGoes>topStr)
	{
	timeGoesUp=false;
	RandomizeSpeed ();
	}


	if (timeGoes<botStr)
	{
	timeGoesUp=true;
	RandomizeSpeed ();
	}


	if (timeGoesUp){timeGoes+=Time.deltaTime*speed;}
	if (!timeGoesUp){timeGoes-=Time.deltaTime*speed;}

	var currStr=timeGoes;

	GetComponent<Renderer>().material.SetFloat( "_AllPower", currStr );
	}

}