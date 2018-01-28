using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Autotype : MonoBehaviour
{

    public float letterPause = 0.2f;
    private Text _text;
    string message;

    // Use this for initialization
    void Start()
    {
        _text = GetComponent<Text>();
        message = _text.text;
        _text.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            _text.text += letter;
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }
}