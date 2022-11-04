using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AStarHotReloadHelper : MonoBehaviour {
#if UNITY_EDITOR
    [SerializeField]
    private byte[] graphData = new byte[0];

    [SerializeField]
    private AstarPath graphItself;

    private void OnDisable()
    {
        if (AstarPath.active != null && AstarPath.active.data != null)
        {
            graphItself = AstarPath.active;
            graphData = graphItself.data.SerializeGraphs();
        }
    }

    //private void OnEnable()
    //{
    //    if (graphData.Length > 0)
    //    {
    //        AstarPath.active.data.DeserializeGraphs(graphData);
    //        AstarPath.active.Scan();
    //    }
    //}

    // Use this for initialization
    void Start () {
		
	}

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            AstarPath.active = graphItself;
            AstarPath.active.data.DeserializeGraphs(graphData);
            AstarPath.active.Scan();

            AIPath[] allAI = GameObject.FindObjectsOfType<AIPath>();
            
            for (int i = 0; i < allAI.Length; i++)
            {
                AIPath currentAi = allAI[i];

                if (currentAi.tr == null)
                {
                    continue;
                }
                
                currentAi.interpolator = new Pathfinding.Util.PathInterpolator();
                currentAi.FindComponents();
                AIDestinationSetter currentSetter = currentAi.GetComponent<AIDestinationSetter>();
                currentSetter.ai = currentAi;

                if (allAI[i].seeker != null)
                {
                    currentAi.seeker.StartPath(currentAi.tr.position, currentAi.hotReloadDestination);
                }
            }
        }
    }
#endif
}
