using UnityEngine;

public class TextMessageScript : MonoBehaviour
{
	public UILabel Label;
	public GameObject Image;

	public bool Attachment = false;
    public bool Right = false;

    void Start()
	{
		if (!this.Attachment)
		{
			if (this.Image != null)
			{
				this.Image.SetActive(false);
			}
		}

        if (Right && EventGlobals.OsanaConversation)
        {
            gameObject.GetComponent<UISprite>().color = new Color(1, .5f, 0);
            Label.color = new Color(1, 1, 1);
        }
    }

    void Update()
	{
		this.transform.localScale = Vector3.Lerp(
			this.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
	}
}
