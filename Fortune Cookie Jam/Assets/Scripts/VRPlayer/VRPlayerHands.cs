using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class VRPlayerHands : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tools;

    private int currentToolIndex = 0;
    public bool OnGrabber { get { return currentToolIndex == 0; } }

    private NVRHand parentHand;

    private void Update()
    {
        if (parentHand == null)
        {
            parentHand = GetComponentInParent<NVRHand>();
        }
        else
        {
            NVRButtonInputs touchPad = parentHand.Inputs[NVRButtons.Touchpad];
            if (touchPad.IsTouched)
            {
                if (touchPad.Axis.x < 0)
                {
                    if (touchPad.PressDown)
                    {
                        tools[currentToolIndex].SetActive(false);

                        currentToolIndex -= 1;

                        if (currentToolIndex < 0)
                        {
                            currentToolIndex = tools.Length - 1;
                        }

                        tools[currentToolIndex].SetActive(true);
                    }
                }
                else
                {
                    if (touchPad.PressDown)
                    {
                        tools[currentToolIndex].SetActive(false);

                        currentToolIndex += 1;

                        if (currentToolIndex >= tools.Length)
                        {
                            currentToolIndex = 0;
                        }

                        tools[currentToolIndex].SetActive(true);
                    }
                }
            }
        }
    }
}