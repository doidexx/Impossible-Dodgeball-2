using UnityEngine;

public class LegRings : MonoBehaviour
{
    public GameObject[] rings = null;
    public TrailRenderer[] trails = null;

    public void MakeRings(Color left, Color right, bool value)
    {
        var color = new Color();
        for (int i = 0; i < rings.Length; i++)
        {
            color = (i % 2 == 0) ? left : right;
            foreach (Transform ring in rings[i].transform)
            {
                ring.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", color);
            }
            trails[i].startColor = color;
            trails[i].endColor = color;
            trails[i].emitting = value;
            rings[i].SetActive(value);
            if (value == false)
                rings[i].GetComponent<Animator>().Rebind();
        }
    }

    public void EndingRings()
    {
        for (int i = 0; i < rings.Length; i++)
        {
            var animator = rings[i].GetComponent<Animator>();
            if (animator.isActiveAndEnabled == false)
                return;
            rings[i].GetComponent<Animator>().SetBool("Ending", true);
        }
    }
}