using System.Collections;
using TMPro;
using UnityEngine;

public class TextRiseAndFadeOut : MonoBehaviour
{

    private TextMeshProUGUI Text;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeTextToZeroAlpha(2, Text, 50));
    }

    public IEnumerator FadeTextToZeroAlpha(float time, TextMeshProUGUI text, float travalSpeed)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.gameObject.transform.position = new Vector3(text.gameObject.transform.position.x, text.gameObject.transform.position.y + (Time.deltaTime * travalSpeed), text.gameObject.transform.position.z);
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / time));
            yield return null;
        }
        GameObject.Destroy(text.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }



}
