using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class VRPlayerHands : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tools;

    private float grabberZ = 0;

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
                if (touchPad.Axis.x < -.5f && touchPad.Axis.y < .5f && touchPad.Axis.y > -.5f)
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
                else if (touchPad.Axis.x > .5f && touchPad.Axis.y < .5f && touchPad.Axis.y > -.5f)
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
                else if(OnGrabber)
                {
                    if (touchPad.IsPressed && !parentHand.IsInteracting)
                    {
                        if (touchPad.Axis.y > .5f && touchPad.Axis.x < .5f && touchPad.Axis.x > -.5f)
                        {
                            grabberZ += Time.deltaTime;

                            grabberZ = Mathf.Clamp(grabberZ, 0, .25f);

                            tools[currentToolIndex].transform.localPosition = new Vector3(0, 0, grabberZ);

                            parentHand.PhysicalController.PhysicalController.transform.GetChild(0).GetChild(0).localPosition = new Vector3(0, 0, grabberZ);
                        }
                        else if (touchPad.Axis.y < -.5f && touchPad.Axis.x < .5f && touchPad.Axis.x > -.5f)
                        {
                            grabberZ -= Time.deltaTime;

                            grabberZ = Mathf.Clamp(grabberZ, 0, .25f);

                            tools[currentToolIndex].transform.localPosition = new Vector3(0, 0, grabberZ);

                            parentHand.PhysicalController.PhysicalController.transform.GetChild(0).GetChild(0).localPosition = new Vector3(0, 0, grabberZ);
                        }
                    }
                }
            }
        }
    }
}