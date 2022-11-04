using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_TransRimShaderInOut : MonoBehaviour
{
	public float str = 1;

	public float fadeIn = 1;
	public float stay = 1;
	public float fadeOut = 1;

	private float timeGoes = 0;
	private float currStr = 0;

	void Start () {
	GetComponent<Renderer>().material.SetFloat( "_AllPower", currStr );
	}

	void Update () {

	timeGoes+=Time.deltaTime;

	if(timeGoes<fadeIn)
	{
	currStr=timeGoes*str*(1/fadeIn);
	}

	if((timeGoes>fadeIn)&&(timeGoes<fadeIn+stay))
	{
	currStr=str;
	}

	if(timeGoes>fadeIn+stay)
	{
	currStr=str-((timeGoes-(fadeIn+stay))*(1/fadeOut));
	}



	//currStr=startStr-timeGoes;

	GetComponent<Renderer>().material.SetFloat( "_AllPower", currStr );


	}


}