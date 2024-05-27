using UnityEngine;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour
{
    public GameObject creditsObject;
    public float scrollSpeed;

    private bool scrolling = false;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = creditsObject.transform.position;
    }

    void Update()
    {
        if (scrolling)
        {
            creditsObject.transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
        }
    }
    
    public void StartScrolling()
    {
        scrolling = true;
    }
    public void StopScrolling()
    {
        scrolling = false;
        creditsObject.transform.position = originalPosition;
    }
}