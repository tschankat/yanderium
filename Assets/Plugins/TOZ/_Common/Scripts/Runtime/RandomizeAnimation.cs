using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOZ {

	public sealed class RandomizeAnimation : MonoBehaviour {
		[SerializeField] private bool randomize;
		[Range(0f, 1f)]
		[SerializeField] private float time;
		private Animator animator;

		private void Awake() {
			animator = GetComponent<Animator>();
			animator.Update(0f);
			animator.Play("Idle", 0, randomize ? UnityEngine.Random.value : time);
		}
	}

}