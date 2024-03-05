using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{

    public Text textElement;
    bool hasUsedW = false;
    bool hasScrolled = false;
    Vector2 startScroll = new Vector2(0, 0);
    Vector2 currentScroll = new Vector2(0, 0);
    // Start is called before the first frame update
    void Start()
    {
        textElement.text = "Use the mouse to control direction.";
        StartCoroutine(TutorialMovementText());
        startScroll = Input.mouseScrollDelta;
        currentScroll = Input.mouseScrollDelta;
    }

    // Update is called once per frame
    void Update()
    {
        currentScroll = Input.mouseScrollDelta;
        if (Input.GetKey(KeyCode.W) && !hasUsedW)
        {
            hasUsedW = true;
            StopAllCoroutines();
            TutorialScroll();
        }

        if (currentScroll != startScroll && !hasScrolled)
        {
            hasScrolled = true;
            TutorialBark();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            textElement.text = "";
        }


    }

    IEnumerator TutorialMovementText()
    {
        yield return new WaitForSeconds(4);
        textElement.text = "Use W to Move.";
    }

    void TutorialScroll()
    {
        textElement.text = "Scroll with Mousewheel to zoom out and see more of the level.";
    }

    void TutorialBark()
    {
        textElement.text = "Hit Space to Bark at the sheep and send it into the pen.";
    }

}
