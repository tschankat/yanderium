using System.Collections.Generic;
using UnityEngine;

public class PopulationManagerScript : MonoBehaviour
{
    [Tooltip("All defined areas should go in here. If your area is not in here, it will not count as an actual area.")]
    [SerializeField] private List<AreaScript> _definedAreas;

    public Vector3 GetCrowdedLocation()
    {
        var crowdedArea = GetCrowdedArea();
        var areaPosition = crowdedArea.transform.position;

        var center = new Vector3(0, 0, 0);
        var count = 0f;
        var floorY = 0;

        foreach(StudentScript student in crowdedArea.Students)
        {
            center += new Vector3(student.transform.position.x, 0f, student.transform.position.z);
            count += 1f;
        }

        center /= count;

        if (areaPosition.y >= 0 && areaPosition.y < 4) floorY = 0;
        else if (areaPosition.y >= 4 && areaPosition.y < 8) floorY = 4;
        else if (areaPosition.y >= 8 && areaPosition.y < 12) floorY = 8;
        else floorY = 12;

        return new Vector3(center.x, floorY, center.z);
    }

    public AreaScript GetCrowdedArea()
    {
        AreaScript mostPopulated = null;
        float closestDistance = 0;

        foreach(AreaScript area in _definedAreas)
        {
            int population = area.Population;

            //If this area's population is bigger than the previous one, make it our biggest area.
            if(population > closestDistance)
            {
                closestDistance = population;
                mostPopulated = area;
            }
        }

        return mostPopulated;
    }

    public Transform Cube;

	#if UNITY_EDITOR
    //public void Update()
    //{
		//if (Input.GetKeyDown(KeyCode.E))
		//{
			//Cube.position = GetCrowdedLocation();
		//}
	//}
	#endif
}