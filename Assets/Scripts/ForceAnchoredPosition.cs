using UnityEngine;

public class ForceAnchoredPosition : MonoBehaviour
{
    private RectTransform rtransform;
    private Vector2 initialPos;
    private Quaternion initialRot;

    private void Awake()
    {
        rtransform = GetComponent<RectTransform>();
        initialPos = rtransform.position;
        initialRot = rtransform.rotation;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        rtransform.anchoredPosition = initialPos;
        rtransform.rotation = initialRot;
    }
}
