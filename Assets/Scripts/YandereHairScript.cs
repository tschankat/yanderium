using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YandereHairScript : MonoBehaviour
{
    public YandereScript Yandere;

    public int Frame;
    public int Limit;

    private void Start()
    {
        ScreenCapture.CaptureScreenshot(Application.streamingAssetsPath +
            "/YandereHair/" + "Hair_" + this.Yandere.Hairstyle + ".png");

        this.Limit = this.Yandere.Hairstyles.Length - 1;
    }

    void Update()
    {
        if (this.Yandere.Hairstyle < this.Limit)
        {
            this.Frame++;

            if (this.Frame == 1)
            {
                this.Yandere.Hairstyle++;
                this.Yandere.UpdateHair();
            }

            if (this.Frame == 2)
            {
                ScreenCapture.CaptureScreenshot(Application.streamingAssetsPath +
                    "/YandereHair/" + "Hair_" + this.Yandere.Hairstyle + ".png");

                this.Frame = 0;
            }
        }
    }
}