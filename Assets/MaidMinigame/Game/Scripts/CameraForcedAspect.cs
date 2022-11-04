using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    [RequireComponent(typeof(Camera))]
    public class CameraForcedAspect : MonoBehaviour
    {
        public Vector2 targetAspect = new Vector2(16f, 9f);

        Camera cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
        }

        void Start()
        {
            float targetAspectValue = targetAspect.x / targetAspect.y;
            float windowAspect = (float)Screen.width / (float)Screen.height;
            float scaleHeight = windowAspect / targetAspectValue;

            if (scaleHeight < 1f)
            {
                Rect rect = cam.rect;

                rect.width = 1f;
                rect.height = scaleHeight;
                rect.x = 0;
                rect.y = (1f - scaleHeight) / 2f;

                cam.rect = rect;
            }
            else
            {
                Rect rect = cam.rect;
                float scaleWidth = 1f / scaleHeight;

                rect.width = scaleWidth;
                rect.height = 1f;
                rect.x = (1f - scaleWidth) / 2f;
                rect.y = 0;

                cam.rect = rect;
            }
        }
    }
}