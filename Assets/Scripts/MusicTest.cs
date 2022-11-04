using UnityEngine;

public class MusicTest : MonoBehaviour
{
	public float[] freqData;
	public AudioSource MainSong;
	public float[] band;
	public GameObject[] g;

	void Start()
	{
		int n = this.freqData.Length;
		int k = 0;

		for (int j = 0; j < this.freqData.Length; j++)
		{
			n /= 2;
			if (n == 0)
			{
				break;
			}

			k++;
		}

		this.band = new float[k + 1];
		this.g = new GameObject[k + 1];

		for (int i = 0; i < this.band.Length; i++)
		{
			this.band[i] = 0.0f;
			this.g[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			this.g[i].transform.position = new Vector3(i, 0.0f, 0.0f);
		}

		// Update at 15 fps.
		InvokeRepeating("check", 0.0f, 1.0f / 30.0f);
	}

	void check()
	{
		this.GetComponent<AudioSource>().GetSpectrumData(
			this.freqData, 0, FFTWindow.Rectangular);

		int k = 0;
		int crossover = 2;

		// [af] JS code doesn't initialize "i"; assuming 0.
		for (int i = 0; i < this.freqData.Length; i++)
		{
			float d = this.freqData[i];
			float b = this.band[k];

			// Find the max as the peak value in that frequency band.
			this.band[k] = (d > b) ? d : b;

			if (i > (crossover - 3))
			{
				k++;

				// Frequency crossover point for each band.
				crossover *= 2;

				Transform gTransform = this.g[k].transform;
				gTransform.position = new Vector3(
					gTransform.position.x,
					this.band[k] * 32.0f,
					gTransform.position.z);

				this.band[k] = 0.0f;
			}
		}
	}
}
