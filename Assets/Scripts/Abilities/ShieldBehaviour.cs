using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    public float shieldDuration = 5f;

    float timeSinceActive = 0;

    private void OnEnable()
    {
        timeSinceActive = 0;
        GetComponent<Animator>().Rebind();
    }

    private void Update()
    {
        timeSinceActive += Time.deltaTime;
        if (timeSinceActive >= shieldDuration)
            gameObject.SetActive(false);
        else if (timeSinceActive >= shieldDuration - 2)
            GetComponent<Animator>().SetBool("Ending", true);
    }
}
