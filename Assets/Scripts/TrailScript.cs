using UnityEngine;

public class TrailScript : MonoBehaviour
{
	void Start()
	{
		GameObject Yandere = GameObject.Find("YandereChan");

		Physics.IgnoreCollision(Yandere.GetComponent<Collider>(),
			this.GetComponent<Collider>());

        //Do NOT ignore collisions between Trail's layer (20) and Ground (8)
        Physics.IgnoreLayerCollision(20, 8, false);
        Physics.IgnoreLayerCollision(8, 20, false);

        //Ignore collisions between Trail (20) and Pickup (15)
        Physics.IgnoreLayerCollision(20, 15, true);
        Physics.IgnoreLayerCollision(15, 20, true);

        Destroy(this);
	}
}