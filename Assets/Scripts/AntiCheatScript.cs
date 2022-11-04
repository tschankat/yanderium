using UnityEngine;
using UnityEngine.SceneManagement;

public class AntiCheatScript : MonoBehaviour
{
    public AudioSource MyAudio;
	public GameObject Jukebox;
	public bool Check = false;

    void Start()
    {
        this.MyAudio = this.GetComponent<AudioSource>();
    }

    void Update()
	{
		if (this.Check)
		{
			if (!this.MyAudio.isPlaying)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "YandereChan")
		{
			this.Jukebox.SetActive(false);
			this.Check = true;
			this.MyAudio.Play();
		}
	}
}
