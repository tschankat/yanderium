using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YandereSimulator.Yancord
{
    public class YancordManager : MonoBehaviour
    {
        [Header("== Conversation related ==")]
        [Range(1, 50)] public int ConversationID = 1;

        [Header("== Chatpartner related ==")]
        public Profile CurrentPartner;
        public Profile MyProfile;
        public Profile SystemProfile;

        [Space(20)]
        [Header("== Chat related ==")]
        public MessageScript MessagePrefab;
        public List<MessageScript> Messages = new List<MessageScript>();
        public List<NewTextMessage> Dialogue = new List<NewTextMessage>();

        public Transform ConversationParent;
        int[] Choice;
        public int currentPhase = 1;
        public float Distance;

        [Space(20)]
        public UILabel ChatLabel;
        [Header("== Dialogue Menu related ==")]
        public UILabel[] DialogueChooseLabel;
        public GameObject DialogueChooseMenu;

        public MessageScript DialogueQuestion;

        [Header("== Server related ==")]
        public GameObject NewServer;
        public Transform SelectedServer;
        public Transform CreateNewServer;

        public GameObject ServerRelated;
        public GameObject PartnerOffline;
        public GameObject PartnerOnline;

        [Space(20)]

        //This Icon is fading out when YC has joined a Server.
        public UITexture BlueDiscordIcon;

        public GameObject DirectMessages;
        public GameObject FindLabel;

        public Transform FirstTimeUI;

        [SerializeField] bool IsDebug;

        [Header("== Delay related ==")]
        public float SystemMessageDelay = 3.0f;
        public float LetterPerSecond = 0.05f;

        public float messageDelay;

        bool Chatting;
        bool ShowingDialogueOption;

        bool FadeOut;
		bool FadeIn;

        public void Start()
        {
            //if (IsDebug) YancordGlobals.JoinedYancord = false;

            if (!YancordGlobals.JoinedYancord)
            {
            	Debug.Log("This is the player's first time launching Yancord.");

                YancordGlobals.CurrentConversation = 1;

				Debug.Log("YancordGlobals.CurrentConversation is: " + YancordGlobals.CurrentConversation);

                if (ConversationID != YancordGlobals.CurrentConversation) enabled = false;

                ChatLabel.text = string.Empty;

                Dialogue[1].isSystemMessage = true;
                Dialogue[1].Message = "Ayano Aishi has joined the Moonlit Warrior Selene Fanserver.";

                FirstTimeUI.gameObject.SetActive(true);
            }
            else
            {
				Debug.Log("The player has launched Yancord before.");

                if (ConversationID != YancordGlobals.CurrentConversation) enabled = false;

                JoinServer();
				
				Dialogue[1].isSystemMessage = true;
                Dialogue[1].Message = "Ayano Aishi has logged in.";

                PartnerOnline.SetActive(true);

                BlueDiscordIcon.alpha = 0f;
                ChatLabel.text = "Press E to start chatting on the Moonlit Warrior Selene Fanserver!";
            }

            CurrentPartner.CurrentStatus = Status.Online;

            SpawnAll();
            Choice = new int[Dialogue.Count];

			Darkness.color = new Color(0, 0, 0, 1);
			FadeIn = true;
        }

		public UITexture Darkness;
        public float timer;
        bool shouldScroll;

        public void Update()
        {
			if (FadeIn)
        	{
				float Alpha = Darkness.color.a;
        		Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime);

        		Darkness.color = new Color(0, 0, 0, Alpha);

        		if (Darkness.color.a == 0)
        		{
        			FadeIn = false;
				}
			}
        	else if (FadeOut)
        	{
        		float Alpha = Darkness.color.a;
				Alpha = Mathf.MoveTowards(Alpha, 1, Time.deltaTime);

        		Darkness.color = new Color(0, 0, 0, Alpha);

				if (Darkness.color.a == 1)
        		{
					SceneManager.LoadScene(SceneNames.HomeScene);
					DateGlobals.DayPassed = false;
				}
			}
			else
			{
	            if (Chatting) // If YC is chatting.
	            {
	                if (currentPhase < Dialogue.Count) //If the Chat hasn't ended.
	                {
	                    CalculateMessageDelay();
	                    if (Dialogue[currentPhase].isQuestion) // If the current dialogue is tagged as a question.
	                    {
	                        if (!ShowingDialogueOption)
	                        {
	                            timer += Time.deltaTime;
	                            if (string.IsNullOrEmpty(ChatLabel.text)) // Preventing the string to change on every frame.
	                            {
	                                ChatLabel.text = CurrentPartner.FirstName + " is typing...";
	                            }

	                            if (timer > messageDelay) // We make some seconds pass in order for the chat partner to send their message in time.
	                            {
	                                ChatLabel.text = string.Empty;
	                                Messages[currentPhase].MyProfile = CurrentPartner;

	                                SpawnChatMessage();

	                                timer = 0f;
	                                ShowingDialogueOption = true;
	                            }
	                        }
	                    }
	                    else
	                    {
	                        if (Dialogue[currentPhase].isSystemMessage)
	                        {
	                            timer += Time.deltaTime;
	                            if (timer > SystemMessageDelay) // We make some seconds pass in order for the chat partner to send their message in time. Or we press E.
	                            {
	                                ChatLabel.text = string.Empty;
	                                SpawnChatMessage();

	                                Messages[currentPhase].MyProfile = SystemProfile;

	                                timer = 0f;
	                                currentPhase++;
	                            }
	                        }
	                        else // If the message is normal.
	                        {
	                            if (currentPhase < Dialogue.Count)
	                            {
	                                if (Dialogue[currentPhase].sentByPlayer) // If message is by player.
	                                {
	                                    Messages[currentPhase].MyProfile = MyProfile;

	                                    SpawnChatMessage();
	                                    currentPhase++;
	                                }
	                                else
	                                {
	                                    timer += Time.deltaTime;
	                                    if (string.IsNullOrEmpty(ChatLabel.text)) // Preventing the string to change on every frame.
	                                    {
	                                        ChatLabel.text = CurrentPartner.FirstName + " is typing...";
	                                    }

	                                    if (timer > messageDelay) // We make some seconds pass in order for the chat partner to send their message in time. Or we press E.
	                                    {
	                                        ChatLabel.text = string.Empty;
	                                        SpawnChatMessage();

	                                        Messages[currentPhase].MyProfile = CurrentPartner;

	                                        timer = 0f;
	                                        currentPhase++;
	                                    }
	                                }
	                            }
	                            else
	                            {
	                                currentPhase++;
	                            }
	                        }
	                    }
						
						if (Input.GetKeyDown(KeyCode.E)) timer = messageDelay;
	                }
	                else // If the chat has ended.
	                {
	                    if (string.IsNullOrEmpty(ChatLabel.text))
	                    {
	                        ChatLabel.text = "Press E to log out of Yancord.";

	                        CurrentPartner.CurrentStatus = Status.Invisible;
	                        PartnerOnline.SetActive(false);
	                        PartnerOffline.SetActive(true);
	                    }

	                    if (Input.GetKeyDown(KeyCode.E))
	                    {
	                        Debug.Log("Quitting!");
	                        YancordGlobals.CurrentConversation++;
	                        FadeOut = true;
	                    }
	                }

	                if (ShowingDialogueOption) // If the chat partner has asked a Question.
	                {
	                    if (Input.GetKeyDown(KeyCode.E) && !DialogueChooseMenu.activeInHierarchy) // If we press E to answer the question.
	                    {
	                        ChatLabel.text = "Choose one of the following answers to respond.";

	                        DialogueChooseMenu.SetActive(true);
	                        DialogueChooseLabel[1].text = Dialogue[currentPhase].OptionQ;
	                        DialogueChooseLabel[2].text = Dialogue[currentPhase].OptionR;
	                        DialogueChooseLabel[3].text = Dialogue[currentPhase].OptionF;

	                        DialogueQuestion.MyProfile = CurrentPartner;
	                        DialogueQuestion.MessageLabel.text = Dialogue[currentPhase].Message;
	                        DialogueQuestion.Awake();
	                    }

	                    if (DialogueChooseMenu.activeInHierarchy) // If the dialogue menu has opened.
	                    {
	                        if (Input.GetKeyDown(KeyCode.Q)) Choice[currentPhase] = 1;
	                        else if (Input.GetKeyDown(KeyCode.R)) Choice[currentPhase] = 2;
	                        else if (Input.GetKeyDown(KeyCode.F)) Choice[currentPhase] = 3;

	                        if (Choice[currentPhase] != 0)
	                        {
	                            // We have decided on our Choice, so the reaction and answer can be predicted now.
	                            Dialogue[currentPhase + 1].Message = GetAnswer(currentPhase);
	                            Dialogue[currentPhase + 2].Message = GetReaction(currentPhase);

	                            Dialogue[currentPhase + 1].sentByPlayer = true;

	                            DialogueChooseMenu.SetActive(false);
	                            ChatLabel.text = "";

	                            ShowingDialogueOption = false;
	                            timer = 0f;

	                            currentPhase++;
	                        }
	                    }
	                    else
	                    {
	                        if (string.IsNullOrEmpty(ChatLabel.text)) ChatLabel.text = "Press E to respond.";
	                    }
	                }

	                if (BlueDiscordIcon.alpha >= 0f)
	                {
	                    BlueDiscordIcon.alpha -= Time.deltaTime * 10f;
	                }
	            }
	            else // If we aren't chatting yet.
	            {
	                if (!YancordGlobals.JoinedYancord)
	                {
	                    if (Input.GetKeyDown(KeyCode.E))
	                    {
	                        YancordGlobals.JoinedYancord = true;
	                        JoinServer();
	                        SpawnChatMessage();

	                        PartnerOnline.SetActive(true);

	                        Chatting = true;
	                    }
	                    else if (Input.GetKeyDown(KeyCode.Q))
	                    {
	                        Debug.Log("Quitting!");
                            FadeOut = true;
	                    }
	                }
	                else
	                {
	                    if (Input.GetKeyDown(KeyCode.E))
	                    {
	                        ChatLabel.text = string.Empty;
	                        SpawnChatMessage();

	                        Chatting = true;
	                    }
	                    else if (Input.GetKeyDown(KeyCode.Q))
	                    {
                            Debug.Log("Quitting!");
                            FadeOut = true;
                        }
	                }
	            }
			}

			if (Input.GetKeyDown(KeyCode.Space)){YancordGlobals.JoinedYancord = false;}

			if (Input.GetKeyDown(KeyCode.Alpha1)){YancordGlobals.CurrentConversation = 1;}
            if (Input.GetKeyDown(KeyCode.Alpha2)){YancordGlobals.CurrentConversation = 2;}
			if (Input.GetKeyDown(KeyCode.Alpha3)){YancordGlobals.CurrentConversation = 3;}
			if (Input.GetKeyDown(KeyCode.Alpha4)){YancordGlobals.CurrentConversation = 4;}
			if (Input.GetKeyDown(KeyCode.Alpha5)){YancordGlobals.CurrentConversation = 5;}
        }

        private string GetReaction(int phase) // We're getting the reaction string of the previous question.
        {
            switch (Choice[phase])
            {
                case 1:
                    return Dialogue[phase].ReactionQ;

                case 2:
                    return Dialogue[phase].ReactionR;

                case 3:
                    return Dialogue[phase].ReactionF;

                default:
                    return null;
            }
        }

        private string GetAnswer(int phase) // We're getting the answer string of the previous question.
        {
            switch (Choice[phase])
            {
                case 1:
                    return Dialogue[phase].OptionQ;

                case 2:
                    return Dialogue[phase].OptionR;

                case 3:
                    return Dialogue[phase].OptionF;

                default:
                    return null;
            }
        }

        void SpawnAll()
        {
            for (int i = 1; i < Dialogue.Count; i++)
            {
                MessageScript currentMessage = Instantiate(MessagePrefab, new Vector3(0f, Messages[i - 1].transform.position.y - ((Messages[i - 1].MessageLabel.height * 0.00167239447142323f) + (Distance * 0.00167239447142323f)), 0f), Quaternion.identity, ConversationParent);
                Messages.Add(currentMessage);

                Messages[i].MessageLabel.text = Dialogue[i].Message;

                if (Dialogue[i].isQuestion) Dialogue[i + 1].sentByPlayer = true;

                if (Dialogue[i].isSystemMessage) Messages[i].MyProfile = SystemProfile;
                else if (Dialogue[i].sentByPlayer) Messages[i].MyProfile = MyProfile;
                else Messages[i].MyProfile = CurrentPartner;

                Messages[i].Awake();

                Messages[i].gameObject.SetActive(false);
            }
        }

        void SpawnChatMessage()
        {
            if (Messages[currentPhase].transform.position.y < -400f|| Messages[currentPhase].transform.localPosition.y - Messages[currentPhase].MessageLabel.height < -400f)
            {
                if (!Messages[currentPhase].gameObject.activeInHierarchy)
                {
                    Messages[currentPhase].gameObject.SetActive(true);
                    Messages[currentPhase].MessageLabel.text = Dialogue[currentPhase].Message;

                    //We move the starting point higher by using the height from the current phase and distance.

                    float targetPosition = -400f + Messages[currentPhase].MessageLabel.height - 10f;
                    float distanceToTargetPosition = -Messages[currentPhase].transform.position.y - targetPosition;
                    //Debug.Log("target pos: " + targetPosition);
                    //Debug.Log("target distance: " + distanceToTargetPosition);

                    Messages[currentPhase].transform.position = new Vector3(0f, targetPosition * 0.00167239447142323f, 0f);

                    // We position everything including 0 into the correct place so that the current phase has enough space.
                    for (int i = currentPhase - 1; i >= 0; i--)
                    {
                        Messages[i].transform.position = new Vector3(0f, Messages[i + 1].transform.position.y + ((Messages[i].MessageLabel.height * 0.00167239447142323f) + (Distance * 0.00167239447142323f)), 0f);
                    }

                    // We make the chat actually scroll up. Starting from the 0 Start point.
                    for (int i = 1; i < Messages.Count; i++)
                    {
                        Messages[i].transform.position = new Vector3(0f, Messages[i - 1].transform.position.y - ((Messages[i - 1].MessageLabel.height * 0.00167239447142323f) + (Distance * 0.00167239447142323f)), 0f);
                    }
                }
            }
            else
            {
                if (!Messages[currentPhase].gameObject.activeInHierarchy)
                {
                    Messages[currentPhase].gameObject.SetActive(true);
                    Messages[currentPhase].MessageLabel.text = Dialogue[currentPhase].Message;

                    for (int i = currentPhase; i < Messages.Count; i++)
                    {
                        Messages[i].transform.position = new Vector3(0f, Messages[i - 1].transform.position.y - ((Messages[i - 1].MessageLabel.height * 0.00167239447142323f) + (Distance * 0.00167239447142323f)), 0f);
                    }
                }
            }
        }

        void JoinServer()
        {
            NewServer.SetActive(true);

            SelectedServer.gameObject.SetActive(true);
            SelectedServer.position = new Vector3(SelectedServer.position.x, NewServer.transform.position.y, SelectedServer.position.z);

            CreateNewServer.position = new Vector3(CreateNewServer.position.x, 0.3740740740740741f, CreateNewServer.position.z);

            DirectMessages.SetActive(false);
            FindLabel.SetActive(false);

            ServerRelated.SetActive(true);
            FirstTimeUI.gameObject.SetActive(false);
        }

        void CalculateMessageDelay()
        {
        	messageDelay = 3;
            //messageDelay = 1 + (LetterPerSecond * Dialogue[currentPhase].Message.Length);
        }
    }
}
