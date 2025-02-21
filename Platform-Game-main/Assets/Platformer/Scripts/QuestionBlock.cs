using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.Rotate(0, -180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material.mainTextureOffset = new Vector2(.2f * Time.time, .2f * Time.time);
    }
}
