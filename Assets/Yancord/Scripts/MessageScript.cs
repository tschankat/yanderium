using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YandereSimulator.Yancord
{
    public class MessageScript : MonoBehaviour
    {
        [Header("== Partner Informations ==")]
        public Profile MyProfile;
        [Space(20)]

        public UILabel NameLabel;
        public UILabel MessageLabel;
        public UITexture ProfilPictureTexture;

        public void Awake()
        {
            if (MyProfile != null)
            {
                if (NameLabel != null) NameLabel.text = MyProfile.FirstName + " " + MyProfile.LastName;

                if (ProfilPictureTexture != null) ProfilPictureTexture.mainTexture = MyProfile.ProfilePicture;

                gameObject.name = MyProfile.FirstName + "_Message";
            }
        }
    }
}
