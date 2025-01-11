using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemScrolling : MonoBehaviour
{
    public Image FirstItemImage;
    public Image LastItemImage;

    public float timeOfScroll = 3.0f;

    private RectTransform containerRectTransform;
    private float distanceToScroll;
    private Vector2 startPosition;
    
    public bool isScrolling = false;

    void Start()
    {
        // 1. Get the container’s RectTransform
        containerRectTransform = GetComponent<RectTransform>();

        // 2. Use anchoredPosition to calculate the scroll distance
        distanceToScroll = LastItemImage.rectTransform.anchoredPosition.y 
                           - FirstItemImage.rectTransform.anchoredPosition.y;

        // 3. Start scrolling immediately
        
    }

    public void RollItems()
    {
        StartCoroutine(ScrollItems());
    }

    private IEnumerator ScrollItems()
    {
        float elapsedTime = 0.0f;

        // 4. Capture the container’s current anchoredPosition
        startPosition = containerRectTransform.anchoredPosition;

        // 5. Decide the end position 
        //    If we want to move upwards, we might subtract distanceToScroll.
        Vector2 endPosition = new Vector2(
            startPosition.x, 
            startPosition.y - distanceToScroll
        );
        
        isScrolling = true;
            
        // 6. Lerp from start to end over timeOfScroll
        while (elapsedTime < timeOfScroll)
        {
            float t = Mathf.Clamp01(elapsedTime / timeOfScroll);
            containerRectTransform.anchoredPosition = Vector2.Lerp(
                startPosition, 
                endPosition, 
                t
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 7. Snap to the exact end position at the end
        containerRectTransform.anchoredPosition = endPosition;
        
        isScrolling = false;
    }

    // Reset the container to its original start position
    public void Reset()
    {
        containerRectTransform.anchoredPosition = startPosition;
    }
}
