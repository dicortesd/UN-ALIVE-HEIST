using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField][HideInInspector] float width;

    public void SetWidth(float width)
    {
        this.width = width;
    }
    public Vector3 GetCenter()
    {
        return transform.position + transform.right * width / 2;
    }

}