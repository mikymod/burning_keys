using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private GameObject[] interactables;

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, mask))
        {
            foreach (var interactable in interactables)
            {
                var coll = interactable.GetComponentInChildren<Collider>();
                var outline = interactable.GetComponentInParent<Outline>();

                if (coll == hit.collider)
                {
                    outline.OutlineMode = Outline.Mode.OutlineVisible;
                    outline.OutlineColor = Color.yellow;
                    outline.OutlineWidth = 10f;
                }
                else
                {
                    outline.OutlineMode = Outline.Mode.OutlineVisible;
                    outline.OutlineColor = Color.white;
                    outline.OutlineWidth = 4f;
                }                 
            }

        }

    }
}
