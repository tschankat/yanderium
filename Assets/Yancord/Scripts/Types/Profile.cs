using UnityEngine;
using System.Collections;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace YandereSimulator.Yancord
{
    [CreateAssetMenu(fileName = "ChatProfile", menuName = "Yancord/Profile", order = 1)]
    public class Profile : ScriptableObject
    {
        [Header("Personal Information")]
        public string FirstName;
        public string LastName;
        [Space(20)]

        [Header("Profile Information")]
        public Texture2D ProfilePicture;
        public string Tag = "XXXX";
        [Space(20)]

        [Header("Profile Settings")]
        public Status CurrentStatus;

        public string GetTag(bool WithHashtag)
        {
            string discordTag = Tag;
            if (discordTag.Length > 4)
            {
                discordTag = discordTag.Substring(0, 4);
            }

            discordTag = WithHashtag ? "#" + discordTag : discordTag;
            return discordTag;
        }
    }

	#if UNITY_EDITOR
    [CustomEditor(typeof(Profile))]
    public class ProfileEditor : Editor
    {
        public Profile profile;

        bool MenuPersonalInformation;
        bool MenuProfileInformation;
        bool MenuProfileSettings;

        public override void OnInspectorGUI()
        {
            profile = (Profile)target;

            profile.FirstName = EditorGUILayout.TextField("First Name", profile.FirstName);
            profile.LastName = EditorGUILayout.TextField("Last Name", profile.LastName);
            EditorGUILayout.Space();

            profile.ProfilePicture = (Texture2D)EditorGUILayout.ObjectField("Profile Picture", profile.ProfilePicture, typeof(Texture2D), false);
            profile.Tag = EditorGUILayout.TextField("Tag", profile.Tag);
            EditorGUILayout.Space();

            profile.CurrentStatus = (Status)EditorGUILayout.EnumPopup("Current Status", profile.CurrentStatus);

            EditorUtility.SetDirty(profile);
        }

    }
	#endif
}