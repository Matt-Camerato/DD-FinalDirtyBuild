using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : MonoBehaviour
{
    [Header("AI Settings")]
    public float stopDistance;
    public float moveSpeed;
    public float waitTime;

    private RectTransform rectTransform;
    private UIDragItem item;
    private Vector3 destination;
    private bool dragging = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        item = GetComponent<UIDragItem>();
        StartCoroutine(Wander()); //begin wander AI
    }

    private IEnumerator Wander()
    {
        //set random destination
        destination = new Vector3(Random.Range(-5.5f, 5.5f), Random.Range(1.5f, -1.5f), 0); // <-hard-coded boundaries of igloo floor (in terms of canvas

        //wait for random period of time
        float wait = waitTime + Random.Range(-3f, 3f);
        yield return new WaitForSeconds(wait);

        StartCoroutine(Wander()); //restart wander cycle
    }

    private void Update()
    {
        //if(item.)

        if (destination == null) return;

        //move towards destination at randomized speed
        Vector3 dir = destination - rectTransform.localPosition;
        if (dir.magnitude < stopDistance) return;
        float speed = moveSpeed + Random.Range(-2f, 2f);
        rectTransform.localPosition += dir.normalized * speed * Time.deltaTime;
    }
}
