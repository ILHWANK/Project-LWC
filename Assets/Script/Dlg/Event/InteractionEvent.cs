using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] StoryEvent storyEvent;

    public Story[] GetStory()
    {
        int endIndex = CSVDataManager.Instance.GetEndIndex();

        storyEvent.storys = CSVDataManager.Instance.GetStory(1, endIndex);

        return storyEvent.storys;
    }
}
