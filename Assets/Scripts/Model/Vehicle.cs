using UnityEngine;
using UnityEngine.UI;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private float speed;
    public Image image;

    public virtual void Drive()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.right);
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        if (image == null)
        {
            image = gameObject.AddComponent<Image>();
        }
    }
}