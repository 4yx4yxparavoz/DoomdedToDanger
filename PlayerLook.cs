using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float range = 3f;
    public Camera playerCamera;
    private Collectible currentTarget;

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            Collectible collectible = hit.collider.GetComponent<Collectible>();

            if (collectible != null)
            {
                if (collectible != currentTarget)
                {
                    ClearHighlight();
                    currentTarget = collectible;
                    currentTarget.Highlight(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    collectible.Collect();
                    currentTarget = null;
                }
            }
            else
            {
                ClearHighlight();
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    void ClearHighlight()
    {
        if (currentTarget != null)
        {
            currentTarget.Highlight(false);
            currentTarget = null;
        }
    }
}
