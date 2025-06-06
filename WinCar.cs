using UnityEngine;

public class WinTriggerInteract : MonoBehaviour
{
    public float interactDistance = 5f;
    public LayerMask interactLayer;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
            {
                if (hit.collider.gameObject.CompareTag("Car"))
                {
                    if (GameManager.instance.collected >= GameManager.instance.total)
                    {
                        GameManager.instance.SendMessage("ShowWinScreen");
                    }
                }
            }
        }
    }
}
