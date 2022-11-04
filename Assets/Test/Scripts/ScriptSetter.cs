using UnityEngine;
using System.Reflection;
using System;

public class ScriptSetter : MonoBehaviour 
{
	public StudentScript OldStudent;
	public StudentScript NewStudent;

    void Start()
    {
    	Component[] cs = (this.GetComponents (typeof(Component)));

    	foreach (Component c in cs)
    	{
       		Debug.Log("name " + c.name + " type " + c.GetType() + " basetype " + c.GetType().BaseType);

        	foreach (FieldInfo fi in c.GetType().GetFields() )
        	{
            	System.Object obj = (System.Object)c;

            	Debug.Log(fi.Name + " value is: " + fi.GetValue(obj));
        	}
		}
	}
}