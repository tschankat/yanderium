using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_TransRimShaderFader : MonoBehaviour
{
	public float startStr = 2;
	public float speed = 3;
	private float timeGoes = 0;
	private float currStr = 0;



	void Update () 
	{

		timeGoes+=Time.deltaTime*speed*startStr;

		currStr=startStr-timeGoes;

		GetComponent<Renderer>().material.SetFloat( "_AllPower", currStr );
	}


}

