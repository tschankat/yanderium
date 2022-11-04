using UnityEngine;
using UnityEngine.UI;

public class ScrollingImage : MonoBehaviour
{
    [SerializeField]
    private RawImage image;
    [SerializeField]
    private float scrollSpeed;
    private float scroll;
	void Update ()
    {
        scroll += Time.deltaTime *scrollSpeed;
        if (image != null)
        {
            image.uvRect = new Rect(scroll, scroll, 1, 1);
        }
	}
}
