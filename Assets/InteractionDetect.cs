using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ResourcePoint>() != null)
        {
            Material mat = Instantiate(collision.gameObject.GetComponent<SpriteRenderer>().material);
            mat.SetFloat("IsReactable", 1);

            collision.gameObject.GetComponent<SpriteRenderer>().material = mat;

            MainGameView.Instance.ShowInteractiveDialog(collision.gameObject.GetComponent<ResourcePoint>().reactText);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ResourcePoint>() != null)
        {
            Material mat = Instantiate(collision.gameObject.GetComponent<SpriteRenderer>().material);
            mat.SetFloat("IsReactable", 0);

            collision.gameObject.GetComponent<SpriteRenderer>().material = mat;

            MainGameView.Instance.HideInteractiveDialog();
        }
    }
}
