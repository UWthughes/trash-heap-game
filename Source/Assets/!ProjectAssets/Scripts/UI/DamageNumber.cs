using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageNumber : MonoBehaviour 
{
    public string number = "";
    public float lifetime = 1f;
    public float moveSpeed = 3f;
    public Color color;
    public Canvas canvas;
    public Text text;

	// Use this for initialization
	void Start () 
    {
        Destroy(gameObject, lifetime);
        text = GetComponentInChildren<Text>();
        canvas = GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!text.text.Equals(number))
            text.text = number;
        if (!text.color.Equals(color))
            text.color = color;

        text.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
	}
}
