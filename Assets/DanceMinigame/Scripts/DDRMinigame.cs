using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DDRMinigame : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private DDRManager manager;
    [SerializeField] private InputManagerScript inputManager;

    [Header("Level select")]
    [SerializeField] private GameObject levelIconPrefab;
    [SerializeField] private RectTransform levelSelectParent;
    [SerializeField] private Text levelNameLabel;
    private DDRLevel[] levels;

    [Header("Gameplay")]
    [SerializeField] private Text comboText;
    [SerializeField] private Text longestComboText;
    [SerializeField] private Text rankText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Image healthImage;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject ratingTextPrefab;
    [SerializeField] private RectTransform gameplayUiParent;
    [SerializeField] private RectTransform[] uiTracks;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float speed;
    
    [Header("Colors")]
    [SerializeField] private Color perfectColor;
    [SerializeField] private Color greatColor;
    [SerializeField] private Color goodColor;
    [SerializeField] private Color okColor;
    [SerializeField] private Color earlyColor;

    private float levelSelectScroll;
    private int selectedLevel;
    private Dictionary<RectTransform, DDRLevel> levelSelectCache;
    private Dictionary<float, RectTransform>[] trackCache;

    #region Level select
    public void LoadLevel(DDRLevel level)
    {
        gameplayUiParent.anchoredPosition = Vector2.zero;
        gameplayUiParent.rotation = Quaternion.identity;
        trackCache = new Dictionary<float, RectTransform>[4];
        for (int i = 0; i < trackCache.Length; i++)
        {
            trackCache[i] = new Dictionary<float, RectTransform>();
            foreach (float node in level.Tracks[i].Nodes)
            {
                RectTransform nodeObject = Instantiate(arrowPrefab, uiTracks[i]).GetComponent<RectTransform>();
                switch (i)
                {
                    case 0:
                        nodeObject.rotation = Quaternion.Euler(0, 0, 90);
                        break;
                    case 1:
                        nodeObject.rotation = Quaternion.Euler(0, 0, 180);
                        break;
                    case 2:
                        nodeObject.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case 3:
                        nodeObject.rotation = Quaternion.Euler(0, 0, -90);
                        break;
                }
                trackCache[i].Add(node, nodeObject);
            }
        }
    }
    public void LoadLevelSelect(DDRLevel[] levels)
    {
        levelSelectCache = new Dictionary<RectTransform, DDRLevel>();
        this.levels = levels;
        for(int i = 0; i<levels.Length; i++)
        {
            RectTransform levelRect = Instantiate(levelIconPrefab, levelSelectParent).GetComponent<RectTransform>();
            levelRect.GetComponent<Image>().sprite = levels[i].LevelIcon;
            levelSelectCache.Add(levelRect, levels[i]);
        }
        positionLevels(true);
    }

    public void UnloadLevelSelect()
    {
        foreach (KeyValuePair<RectTransform, DDRLevel> entry in levelSelectCache)
        {
            Destroy(entry.Key.gameObject);
        }
        levelSelectCache = new Dictionary<RectTransform, DDRLevel>();
    }

    public void UpdateLevelSelect()
    {
		if (inputManager.TappedLeft)
		{
			levelSelectScroll--;
		}
		else if (inputManager.TappedRight)
		{
			levelSelectScroll++;
		}

        //levelSelectScroll += Input.GetAxis("Horizontal") * Time.deltaTime * 10;
        levelSelectScroll = Mathf.Clamp(levelSelectScroll, 0, levels.Length-1);
        selectedLevel = (int)Mathf.Round(levelSelectScroll);
        positionLevels();

        if (Input.GetButtonDown(InputNames.Xbox_A))
        {
            manager.LoadedLevel = levels[selectedLevel];
        }

		if (Input.GetButtonDown(InputNames.Xbox_B))
        {
            manager.BootOut();
        }
    }
    private void positionLevels(bool instant = false)
    {
        for (int i = 0; i < levelSelectCache.Keys.Count; i++)
        {
            RectTransform rect = levelSelectCache.ElementAt(i).Key;
            Vector2 targetPositon = new Vector2(-selectedLevel * 400 + (i * 400), 0);
            rect.anchoredPosition = instant ? targetPositon : Vector2.Lerp(rect.anchoredPosition, targetPositon, 10 * Time.deltaTime);
            levelNameLabel.text = levels[selectedLevel].LevelName;
        }
    }
    #endregion

    #region Gameplay
    public void UpdateGame(float time)
    {
        if (trackCache == null) return;
        bool failed = manager.GameState.FinishStatus == DDRFinishStatus.Failed;

        if (!failed)
        {
            pollInput(time);
            gameplayUiParent.anchoredPosition = Vector2.Lerp(gameplayUiParent.anchoredPosition, Vector2.zero, 10 * Time.deltaTime);
            gameplayUiParent.rotation = Quaternion.identity;
        }
        else
        {
            gameplayUiParent.anchoredPosition += Vector2.down *50* Time.deltaTime;
            gameplayUiParent.Rotate(Vector3.forward * -2 * Time.deltaTime);
            shakeUi(0.5f);
        }

        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, manager.GameState.Health / 100, 10*Time.deltaTime);

        for(int i = 0; i<trackCache.Length; i++)
        {
            Dictionary<float, RectTransform> track = trackCache[i];
            foreach (float key in track.Keys)
            {
                float distance = (key - time);
                if (distance < -0.05f)
                {
                    if(!failed) displayHitRating(i, DDRRating.Miss);
                    assignPoints(DDRRating.Miss);
                    updateCombo(DDRRating.Miss);
                    removeNodeAt(trackCache.ToList().IndexOf(track),0);
                    return;
                }
                track[key].anchoredPosition = new Vector2(0, -distance * speed) + offset;
            }
        }
    }
    public void UpdateEndcard(GameState state)
    {
        scoreText.text = string.Format("Score: {0}", state.Score);
        Color resultColor;
        rankText.text = getRank(state, out resultColor);
        rankText.color = resultColor;
        longestComboText.text = string.Format("Biggest combo: {0}", state.LongestCombo.ToString());
    }
    private DDRRating getRating(int track, float time)
    {
        float nodeTime;
        RectTransform rect;
        getFirstNodeOn(track, out nodeTime, out rect);

        #region Assign rating
        DDRRating rating = DDRRating.Miss;
        float distance = offset.y - rect.localPosition.y;
        if (distance < 130)
        {
            rating = DDRRating.Early;
            if (distance < 75)
            {
                rating = DDRRating.Ok;
            }
            if (distance < 65)
            {
                rating = DDRRating.Good;
            }
            if (distance < 50)
            {
                rating = DDRRating.Great;
            }
            if (distance < 30)
            {
                rating = DDRRating.Perfect;
            }
            if (distance < -30)
            {
                rating = DDRRating.Ok;
            }
            if (distance < -130f)
            {
                rating = DDRRating.Miss;
            }
        }
        #endregion

        return rating;
    }
    private string getRank(GameState state, out Color resultColor)
    {
        string rank = "F";
        int maxScore = 0;
        int referenceScore = manager.LoadedLevel.PerfectScorePoints;
        foreach (DDRTrack track in manager.LoadedLevel.Tracks) maxScore += track.Nodes.Count * referenceScore;

        float scorePercentage = ((float)state.Score / (float)maxScore)*100;

        if (scorePercentage >= 30) rank = "D";
        if (scorePercentage >= 50) rank = "C";
        if (scorePercentage >= 75) rank = "B";
        if (scorePercentage >= 80) rank = "A";
        if (scorePercentage >= 95) rank = "S";
        if (scorePercentage >= 100) rank = "S+";
        resultColor = Color.Lerp(Color.red, Color.green, scorePercentage / 100);
        return rank;
    }
    private void pollInput(float time)
    {
        if (inputManager.TappedLeft)
        {
            registerKeypress(0, time);
        }
        if (inputManager.TappedDown)
        {
            registerKeypress(1, time);
        }
        if (inputManager.TappedUp)
        {
            registerKeypress(2, time);
        }
        if (inputManager.TappedRight)
        {
            registerKeypress(3, time);
        }
    }
    private void registerKeypress(int track, float time)
    {
        DDRRating rating = getRating(track, time);
        displayHitRating(track, rating);
        assignPoints(rating);
        registerRating(rating);
        updateCombo(rating);
        if (rating != DDRRating.Miss)
        {
            removeNodeAt(track);
        }
    }
    private void registerRating(DDRRating rating)
    {
        manager.GameState.Ratings[rating] += 1;
        manager.GameState.Ratings.OrderBy(x => x.Value);
    }
    private void updateCombo(DDRRating rating)
    {
        comboText.text = "";
        comboText.color = Color.white;
        comboText.GetComponent<Animation>().Play();
        if (rating != DDRRating.Miss && rating != DDRRating.Early)
        {
            manager.GameState.Combo++;
            if (manager.GameState.Combo > manager.GameState.LongestCombo)
            {
                manager.GameState.LongestCombo = manager.GameState.Combo;
                comboText.color = Color.yellow;
            }
            if (manager.GameState.Combo >= 2)
            {
                comboText.text = string.Format("x{0} combo", manager.GameState.Combo);
                comboText.color = Color.white;
            }
        }
        else
        {
            manager.GameState.Combo = 0;
        }
    }
    private void removeNodeAt(int trackId, float delay=0)
    {
        Dictionary<float, RectTransform> track = trackCache[trackId];
        float[] values = track.Keys.ToArray();
        Array.Sort(values, (a, b) => a.CompareTo(b));

        Destroy(track[values[0]].gameObject,delay);
        track.Remove(values[0]);
    }
    private void getFirstNodeOn(int track, out float time, out RectTransform rect)
    {
    	RectTransform WorstCaseScenarioRect;

        Dictionary<float, RectTransform> trackData = trackCache[track];
        float[] times = trackData.Keys.ToArray();
        Array.Sort(times, (a, b) => a.CompareTo(b));

	    time = times[0];
		rect = trackData[time];
    }
    private void displayHitRating(int track, DDRRating rating)
    {
        Text ratingText = Instantiate(ratingTextPrefab, uiTracks[track]).GetComponent<Text>();
        ratingText.rectTransform.anchoredPosition = new Vector2(0, 280);
        switch (rating)
        {
            case DDRRating.Miss:
                ratingText.text = "Miss";
                ratingText.color = Color.red;
                shakeUi(5);
                break;
            case DDRRating.Early:
                ratingText.text = "Early";
                ratingText.color = earlyColor;
                break;
            case DDRRating.Ok:
                ratingText.text = "Ok";
                ratingText.color = okColor;
                break;
            case DDRRating.Good:
                ratingText.text = "Good";
                ratingText.color = goodColor;
                break;
            case DDRRating.Great:
                ratingText.text = "Great";
                ratingText.color = greatColor;
                break;
            case DDRRating.Perfect:
                ratingText.text = "Perfect";
                ratingText.color = perfectColor;
                break;
        }
        Destroy(ratingText, 1);
    }
    private void assignPoints(DDRRating rating)
    {
        int points = 0;
        switch (rating)
        {
            case DDRRating.Early:
                points = manager.LoadedLevel.EarlyScorePoints;
                break;
            case DDRRating.Good:
                points = manager.LoadedLevel.GoodScorePoints;
                break;
            case DDRRating.Great:
                points = manager.LoadedLevel.GreatScorePoints;
                break;
            case DDRRating.Miss:
                points = manager.LoadedLevel.MissScorePoints;
                break;
            case DDRRating.Ok:
                points = manager.LoadedLevel.OkScorePoints;
                break;
            case DDRRating.Perfect:
                points = manager.LoadedLevel.PerfectScorePoints;
                break;
        }
        if(rating!=DDRRating.Miss) manager.GameState.Score += points;
        manager.GameState.Health += points;
    }
    private void shakeUi(float factor)
    {
        Vector2 offset = new Vector2(UnityEngine.Random.Range(-factor, factor), UnityEngine.Random.Range(-factor, factor));
        gameplayUiParent.anchoredPosition += offset;
    }
    #endregion

    public void Unload()
    {
        UnloadLevelSelect();
    }
}
