using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_effectCaster : MonoBehaviour
{
	public GameObject moveThis;
	public RaycastHit hit;
	public GameObject[] createThis;
	public float cooldown;
	public float changeCooldown;
	public int selected = 0;
	public UnityEngine.UI.Text writeThis;
	private float rndNr;
	private GameObject effect;
	
	void Start ()
	{
		selected=createThis.Length-1;
		writeThis.text=selected.ToString()+" "+createThis[selected].name;

	}

	void Update ()
	{
		if(cooldown>0){cooldown-=Time.deltaTime;}
		if(changeCooldown>0){changeCooldown-=Time.deltaTime;}

		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit)) {
		// Create a prefab if hit something

		moveThis.transform.position=hit.point;


		if(Input.GetMouseButton(0)&&cooldown<=0){
		effect=Instantiate(createThis[selected], moveThis.transform.position, moveThis.transform.rotation);

		//For showcasing reasons: some effects don't look good if they appear half underground. I move these effects a bit upward.
		if(effect.tag=="explosion"||effect.tag=="missile"||effect.tag=="breath"){
                    effect.transform.position += new Vector3(0, 1.5f, 0);
                    //effect.transform.position.y+=1.5f;
		}

		if(effect.tag=="shield"){
                    effect.transform.position += new Vector3(0, 0.5f, 0);
		//effect.transform.position.y+=0.5f;
		}

		cooldown=0.15f;
		}



		}


		if (Input.GetKeyDown(KeyCode.UpArrow) && changeCooldown<=0)
		{
			selected+=1;
				if(selected>(createThis.Length-1)) {selected=0;}
			
			writeThis.text=selected.ToString()+" "+createThis[selected].name;
			changeCooldown=0.1f;
		}

		if (Input.GetKeyDown(KeyCode.DownArrow) && changeCooldown<=0)
		{
			selected-=1;
				if(selected<0) {selected=createThis.Length-1;}
			
				writeThis.text=selected.ToString()+" "+createThis[selected].name;
			changeCooldown=0.1f;
		}




	}
}