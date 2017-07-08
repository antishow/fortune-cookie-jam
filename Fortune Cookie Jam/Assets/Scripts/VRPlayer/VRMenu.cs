using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pagePrefab;

    private List<GameObject> pages = new List<GameObject>();

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private Transform leftTrigger;

    [SerializeField]
    private Transform rightTrigger;

    private int currentIndex = 0;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(pagePrefab, spawnPoint.position + spawnPoint.up * i * .005f, spawnPoint.rotation);
            temp.transform.parent = transform;
            pages.Add(temp);
        }

        currentIndex = pages.Count - 1;
    }

    void UpdateColliders()
    {
        int size = pages.Count;

        leftTrigger.transform.position = spawnPoint.position + spawnPoint.up * (size / 2) * .005f;
        Vector3 leftScale = leftTrigger.transform.localScale;
        leftScale.y = .005f * size;
        leftTrigger.transform.localScale = leftScale;

        rightTrigger.transform.position = spawnPoint.position + spawnPoint.up * (size / 2) * .005f;
        Vector3 rightScale = rightTrigger.transform.localScale;
        rightScale.y = .005f * size;
        rightTrigger.transform.localScale = leftScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {

        }
    }
}
