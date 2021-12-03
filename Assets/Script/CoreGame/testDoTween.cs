using DG.Tweening;
using UnityEngine;

public class testDoTween : MonoBehaviour
{
    // Start is called before the first frame update
    // public GameObject khargush;
    private void Start()
    {
        transform.DOMove(new Vector3(1, 1, 3), 12);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}