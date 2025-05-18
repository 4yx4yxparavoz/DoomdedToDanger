using UnityEngine;

public class Collectible : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void Highlight(bool state)
    {
        if (state)
            rend.material.color = Color.yellow; 
        else
            rend.material.color = originalColor;
    }

    public void Collect()
    {
        GameManager.instance.CollectItem();
        Destroy(gameObject);
    }
}
