using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace MaidDereMinigame
{
    public class CharacterHairPlacer : MonoBehaviour
    {

        public Sprite[] hairSprites;

        [HideInInspector] public SpriteRenderer hairInstance;

        private void Awake()
        {
            int randomIndex = Random.Range(0, hairSprites.Length);
            hairInstance = new GameObject("Hair", typeof(SpriteRenderer)).GetComponent<SpriteRenderer>();
            Transform hairTransform = hairInstance.transform;
            hairTransform.parent = transform;
            hairTransform.localPosition = new Vector3(0, 0, -0.1f);
            hairInstance.sprite = hairSprites[randomIndex];
        }

        public void WalkPose(float height)
        {
            hairInstance.transform.localPosition = new Vector3(0, height, hairInstance.transform.localPosition.z);
        }

        public void HairPose(string point)
        {
            string[] array = point.Split(new char[]
            {
                ','
            });
            float num;
            bool numParsed = float.TryParse(array[0], NumberStyles.Float, NumberFormatInfo.InvariantInfo, out num);
            float y;
            bool yParsed = float.TryParse(array[1], NumberStyles.Float, NumberFormatInfo.InvariantInfo, out y);
            if (numParsed && yParsed)
            {
                this.hairInstance.transform.localPosition = new Vector3(this.hairInstance.flipX ? (-num) : num, y, this.hairInstance.transform.localPosition.z);
                return;
            }
            Debug.Log("There was an error while parsing the hair position in CharacterHairPlacer");
        }
    }
}