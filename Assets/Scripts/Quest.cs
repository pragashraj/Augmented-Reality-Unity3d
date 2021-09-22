using UnityEngine;

[System.Serializable]
public class Quest
{
    public string name;
    public Texture source;

    [HideInInspector]
    public string[] answers;
}
