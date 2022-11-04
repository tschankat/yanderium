using UnityEngine;
using System.Collections;

public class skydomeScript2 : MonoBehaviour {
    
    
    public Light sunLight;
    public GameObject SkyDome;
    public Camera cam;
    Sun sunlightScript;
    public bool debug = false;

    public float JULIANDATE = 150;
    public float LONGITUDE = 0.0f;
    public float LATITUDE = 0.0f;
    public float MERIDIAN = 0.0f;
    public float TIME = 8.0f;
    public float m_fTurbidity = 2.0f;

    public float cloudSpeed1 = 1.0f;
    public float cloudSpeed2 = 1.5f;
    public float cloudHeight1 = 12.0f;
    public float cloudHeight2 = 13.0f;
    public float cloudTint = 1.0f;

	// [af] Commented out unused member.
    //Vector4 vSunColourIntensity = new Vector4(1f, 1f, 1f, 100);

    Vector4 vBetaRayleigh = new Vector4();
    Vector4 vBetaMie = new Vector4();
    Vector3 m_vBetaRayTheta = new Vector3();
    Vector3 m_vBetaMieTheta = new Vector3();
    
    public float m_fRayFactor = 1000.0f;
	public float m_fMieFactor =  0.7f;
    public float m_fDirectionalityFactor = 0.6f;
    public float m_fSunColorIntensity = 1.0f;


	void Start () {
        sunlightScript = sunLight.GetComponent(typeof(Sun)) as Sun;
	}
    void Update()
    {
        calcAtmosphere();
        if (Input.GetKeyDown("tab"))
        {
            //debug = !debug;
        }
        Vector3 sunLightD = sunLight.transform.TransformDirection(-Vector3.forward);
        Vector3 pos = cam.transform.position;

        SkyDome.GetComponent<Renderer>().material.SetVector("vBetaRayleigh", vBetaRayleigh);
        SkyDome.GetComponent<Renderer>().material.SetVector("BetaRayTheta", m_vBetaRayTheta);
        SkyDome.GetComponent<Renderer>().material.SetVector("vBetaMie", vBetaMie);                     
        SkyDome.GetComponent<Renderer>().material.SetVector("BetaMieTheta", m_vBetaMieTheta);
        SkyDome.GetComponent<Renderer>().material.SetVector("g_vEyePt",  pos);
        SkyDome.GetComponent<Renderer>().material.SetVector("LightDir", sunLightD);
        SkyDome.GetComponent<Renderer>().material.SetVector("g_vSunColor", sunlightScript.m_vColor);
        SkyDome.GetComponent<Renderer>().material.SetFloat("DirectionalityFactor", m_fDirectionalityFactor);
        SkyDome.GetComponent<Renderer>().material.SetFloat("SunColorIntensity", m_fSunColorIntensity);
        SkyDome.GetComponent<Renderer>().material.SetFloat("tint", cloudTint);
        SkyDome.GetComponent<Renderer>().material.SetFloat("cloudSpeed1", cloudSpeed1);
        SkyDome.GetComponent<Renderer>().material.SetFloat("cloudSpeed2", cloudSpeed2);
        SkyDome.GetComponent<Renderer>().material.SetFloat("plane_height1", cloudHeight1);
        SkyDome.GetComponent<Renderer>().material.SetFloat("plane_height2", cloudHeight2);

	}
    void calcAtmosphere()
    {
        calcRay();
        CalculateMieCoeff();
    }
    void calcRay()
    {
	    const float n  = 1.00029f;		//Refraction index for air
	    const float N  = 2.545e25f;		//Molecules per unit volume
	    const float pn = 0.035f;		//Depolarization factor

        float fRayleighFactor = m_fRayFactor * (Mathf.Pow(Mathf.PI, 2.0f) * Mathf.Pow(n * n - 1.0f, 2.0f) * (6 + 3 * pn)) / ( N * ( 6 - 7 * pn ) );
        
	    m_vBetaRayTheta.x = fRayleighFactor / ( 2.0f * Mathf.Pow( 650.0e-9f, 4.0f ) );
	    m_vBetaRayTheta.y = fRayleighFactor / ( 2.0f * Mathf.Pow( 570.0e-9f, 4.0f ) );
	    m_vBetaRayTheta.z = fRayleighFactor / ( 2.0f * Mathf.Pow( 475.0e-9f, 4.0f ) );

        vBetaRayleigh.x = 8.0f * fRayleighFactor / (3.0f * Mathf.Pow(650.0e-9f, 4.0f));
        vBetaRayleigh.y = 8.0f * fRayleighFactor / (3.0f * Mathf.Pow(570.0e-9f, 4.0f));
        vBetaRayleigh.z = 8.0f * fRayleighFactor / (3.0f * Mathf.Pow(475.0e-9f, 4.0f));
    }
    void CalculateMieCoeff()
    {
        float[] K =new float[3];
        K[0]=0.685f;  
        K[1]=0.679f;
        K[2]=0.670f;

	    float c = ( 0.6544f * m_fTurbidity - 0.6510f ) * 1e-16f;	//Concentration factor

	    float fMieFactor = m_fMieFactor * 0.434f * c * 4.0f * Mathf.PI * Mathf.PI;

	    m_vBetaMieTheta.x = fMieFactor / ( 2.0f * Mathf.Pow( 650e-9f, 2.0f ) );
	    m_vBetaMieTheta.y = fMieFactor / ( 2.0f * Mathf.Pow( 570e-9f, 2.0f ) );
	    m_vBetaMieTheta.z = fMieFactor / ( 2.0f * Mathf.Pow( 475e-9f, 2.0f ) );

        vBetaMie.x = K[0] * fMieFactor / Mathf.Pow(650e-9f, 2.0f);
        vBetaMie.y = K[1] * fMieFactor / Mathf.Pow(570e-9f, 2.0f);
        vBetaMie.z = K[2] * fMieFactor / Mathf.Pow(475e-9f, 2.0f);

        float fTemp3 = 0.434f * c * (float)Mathf.PI * (2.0f * (float)Mathf.PI) * (2.0f * (float)Mathf.PI);
        // not sure if above is correct, but it look good.

        vBetaMie *= fTemp3;
    }
    void OnGUI()
    {
        if (debug)
        {
            GUILayout.BeginArea(new Rect(0, 10, 600, 400));
            GUILayout.BeginHorizontal();
            GUILayout.Label("Time : " + TIME.ToString(), GUILayout.Width(200));
            TIME = GUILayout.HorizontalSlider(TIME, 0f, 23f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("SunColorIntensity : " + m_fSunColorIntensity.ToString(), GUILayout.Width(200));
            m_fSunColorIntensity = GUILayout.HorizontalSlider(m_fSunColorIntensity, 0, 2f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("DirectionalityFactor : " + m_fDirectionalityFactor.ToString(), GUILayout.Width(200));
            m_fDirectionalityFactor = GUILayout.HorizontalSlider(m_fDirectionalityFactor, 0f, 1f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Rayleigh multiplier : " + m_fRayFactor.ToString(), GUILayout.Width(200));
            m_fRayFactor = GUILayout.HorizontalSlider(m_fRayFactor, 0f, 10000f);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Mie multiplier : " + m_fMieFactor.ToString(), GUILayout.Width(200));
            m_fMieFactor = GUILayout.HorizontalSlider(m_fMieFactor, 0f, 5f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("cloudTint : " + cloudTint.ToString(), GUILayout.Width(200));
            cloudTint = GUILayout.HorizontalSlider(cloudTint, 0f, 2f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("cloudSpeed1 : " + cloudSpeed1.ToString(), GUILayout.Width(200));
            cloudSpeed1 = GUILayout.HorizontalSlider(cloudSpeed1, 0f, 6f);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("cloudSpeed2 : " + cloudSpeed2.ToString(), GUILayout.Width(200));
            cloudSpeed2 = GUILayout.HorizontalSlider(cloudSpeed2, 0f, 6f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("cloudHeight1 : " + cloudHeight1.ToString(), GUILayout.Width(200));
            cloudHeight1 = GUILayout.HorizontalSlider(cloudHeight1, 10f, 20f);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("cloudHeight2 : " + cloudHeight2.ToString(), GUILayout.Width(200));
            cloudHeight2 = GUILayout.HorizontalSlider(cloudHeight2, 10f, 20f);
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }
        
    }
}
