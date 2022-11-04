using UnityEngine;
using System.Collections;
using System;

namespace YandereSimulator.Yancord
{
    [Serializable]
    public class NewTextMessage
    {
        public string Message;
        public bool isQuestion;
        public bool sentByPlayer;
        public bool isSystemMessage;

        [Header("== Question Related ==")]

        public string OptionQ;
        public string OptionR, OptionF;

        [Space(20)]

        public string ReactionQ;
        public string ReactionR, ReactionF;
    }
}
