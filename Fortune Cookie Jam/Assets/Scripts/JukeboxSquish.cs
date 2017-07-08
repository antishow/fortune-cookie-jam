using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeboxSquish : MonoBehaviour
{
    private void Start()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("scale", transform.localScale * .9f, "time", 1f, "easetype", iTween.EaseType.easeOutBounce, "looptype", iTween.LoopType.pingPong));
    }
}
