using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool hasEnnemies;
    private bool openDoors = false;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Transform parentTransform = gameObject.transform;

        // Find all children with the name "ChildName"
        foreach (Transform child in parentTransform)
        {
            if (child.name == "Ennemies")
            {
                if(child.transform.childCount == 0)
                {
                    hasEnnemies = false;
                }
                else
                {
                    hasEnnemies = true;
                }
            }
        }

        if (!hasEnnemies)
        {
            OpenDoors();
        } 
        else
        {
            CloseDoors();
        }
    }

    void OpenDoors()
    {
        // Get the transform of the parent object
        Transform parentTransform = gameObject.transform;

        // Find all children with the name "ChildName"
        foreach (Transform child in parentTransform)
        {
            if (child.name == "Doors")
            {
                child.gameObject.SetActive(true);
            }

            if (child.name == "Bars")
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    void CloseDoors()
    {
        // Get the transform of the parent object
        Transform parentTransform = gameObject.transform;

        // Find all children with the name "ChildName"
        foreach (Transform child in parentTransform)
        {
            if (child.name == "Doors")
            {
                child.gameObject.SetActive(false);
            }

            if (child.name == "Bars")
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
