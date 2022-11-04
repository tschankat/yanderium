using MaidDereMinigame.Malee;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    public class SFXController : MonoBehaviour
    {
        static SFXController instance;
        public static SFXController Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<SFXController>();
                return instance;
            }
        }

        public enum Sounds
        {
            Countdown,
            MenuBack,
            MenuConfirm,
            ClockTick,
            DoorBell,
            GameFail,
            GameSuccess,
            Plate,
            PageTurn,
            MenuSelect,
			MaleCustomerGreet,
			MaleCustomerThank,
			MaleCustomerLeave,
			FemaleCustomerGreet,
			FemaleCustomerThank,
			FemaleCustomerLeave,
			MenuOpen
        }

        [Reorderable]
        public SoundEmitters emitters;

        private void Awake()
        {
            if (Instance != this)
                Destroy(gameObject);
            else
                DontDestroyOnLoad(gameObject);
        }

        public static void PlaySound(Sounds sound)
        {
            SoundEmitter emitter = Instance.GetEmitter(sound);
            AudioSource source = emitter.GetSource();
            if (!source.isPlaying || emitter.interupt)
            {
                source.clip = Instance.GetRandomClip(emitter);
                source.Play();
            }
        }

        SoundEmitter GetEmitter(Sounds sound)
        {
            foreach (SoundEmitter soundEmitter in emitters)
                if (soundEmitter.sound == sound)
                    return soundEmitter;

            Debug.Log(string.Format("There is no sound emitter created for {0}", sound), this);
            return null;
        }

        AudioClip GetRandomClip(SoundEmitter emitter)
        {
            int index = Random.Range(0, emitter.clips.Count);
            return emitter.clips[index];
        }
    }

    [System.Serializable]
    public class SoundEmitters : ReorderableArray<SoundEmitter> { }

    [System.Serializable]
    public class SoundEmitter
    {
        public SFXController.Sounds sound;
        public bool interupt;
        [Reorderable] public AudioSources sources;
        [Reorderable] public AudioClips clips;

        public AudioSource GetSource()
        {
            for (int i = 0; i < sources.Count; i++)
                if (!sources[i].isPlaying)
                    return sources[i];
            return sources[0];
        }
    }

    [System.Serializable]
    public class AudioClips : ReorderableArray<AudioClip> { }

    [System.Serializable]
    public class AudioSources : ReorderableArray<AudioSource> { }
}