using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    public abstract class AIMover : MonoBehaviour
    {
        protected float moveSpeed = 3f;

        public abstract ControlInput GetInput();

        

        private void FixedUpdate()
        {
            ControlInput inputs = GetInput();

            transform.Translate(new Vector2(inputs.horizontal, 0) * Time.fixedDeltaTime * moveSpeed);
        }
    }

    [System.Serializable]
    public struct ControlInput
    {
        public float horizontal;
    }
}