using UnityEngine;
using Valve.VR.InteractionSystem;

public class MyInteractable : UIElement
{

    protected override void OnHandHoverBegin(Hand hand)
    {
        Debug.Log("Hi");
        base.OnHandHoverBegin(hand);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
