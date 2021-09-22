using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Material characterQuest;
    [SerializeField] private Quest[] quests;
    [SerializeField] private string[] ansList;
    [SerializeField] private Text[] answerSlots; 

    private static int currentIndex = 0;
    private static List<string> ans = new List<string>();

    public int CurrentIndex { get { return currentIndex; } set { currentIndex = value; } }
    public Quest[] Quests { get { return quests; } set { quests = value; } }

    private void Awake()
    {
        for (int i = 0; i < ansList.Length; i++)
        {
            ans.Add(ansList[i]);
        }
    }

    void Start()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            Quest quest = quests[i];
            int correctIndex = Random.Range(0, 4);
            quest.answers = new string[4];
            quest.answers[correctIndex] = quest.name;
            for (int j = 0; j < 4; j++)
            {
                int randomIndex = Random.Range(0, 15);
                if (randomIndex != correctIndex && j != correctIndex)
                {
                    quest.answers[j] = ans[randomIndex];
                }
            }
        }
    }

    
    void Update()
    {
        if (quests.Length > 0 && currentIndex < quests.Length)
        {
            characterQuest.mainTexture = quests[currentIndex].source;
            SetAnswerSlots();
        }
    }

    private void SetAnswerSlots()
    {
        Quest quest = quests[currentIndex];

        for (int i = 0; i < answerSlots.Length; i++)
        {
            answerSlots[i].text = quest.answers[i];
        }
    }
}
