using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DescriptionMessage : MonoBehaviour
{
    int kWidth = 50;
    float mHeight = 0;

    GameObject panel;
    GameObject textObj;
    GameObject textUGUIObj;
    GameObject panel2;
    GameObject textObj2;
    GameObject panel3;
    GameObject textObj3;
    // Start is called before the first frame update
    void Start()
    {
        panel = transform.Find("Panel").gameObject;
        textObj = panel.transform.Find("Text").gameObject;
        textUGUIObj = panel.transform.Find("TextUGUI").gameObject;
        panel2 = transform.Find("Panel2").gameObject;
        textObj2 = panel2.transform.Find("Text").gameObject;
        panel3 = transform.Find("Panel3").gameObject;
        textObj3 = panel3.transform.Find("Text").gameObject;
    }

    float alpha = 0f;

    // Update is called once per frame
    void Update()
    {

        Setup(Database.descriptionMessagePos, Database.descriptionMessageAlign);
        //Setup(new Vector2(Input.mousePosition.x * 1f / Screen.width * 160f, Input.mousePosition.y * 1f / Screen.height * 90f));
    }

    void Setup(Vector2 _pos,string _align)
    {
        panel.GetComponent<Image>().color = new Color(0f, 0f, 0f, alpha * 0.99f);
        //textObj.GetComponent<Text>().color = new Color(1f, 1f, 1f, alpha * 2f);
        panel2.GetComponent<Image>().color = new Color(0f, 0f, 0f, alpha * 0.99f);
        //textObj2.GetComponent<Text>().color = new Color(1f, 1f, 1f, alpha * 2f);
        panel3.GetComponent<Image>().color = new Color(0f, 0f, 0f, alpha * 0.99f);
        //textObj3.GetComponent<Text>().color = new Color(1f, 1f, 1f, alpha * 2f);
        textObj.GetComponent<CanvasRenderer>().SetAlpha(alpha);
        textObj2.GetComponent<CanvasRenderer>().SetAlpha(alpha);
        textObj3.GetComponent<CanvasRenderer>().SetAlpha(alpha);

        float maxWidth = 0;
        mHeight = 0;
        if (Database.descriptionMessage.Length > 0)
        {
            alpha = Mathf.Min(1, alpha + Time.unscaledDeltaTime * 4f);
            if (Database.descriptionMessage.Trim().Length > 0)
            {
                transform.SetAsLastSibling();
                Text m_myTextBox = textObj.GetComponent<Text>();
                m_myTextBox.rectTransform.sizeDelta = new Vector3(kWidth, 20);
                //TextMeshProUGUI m_myTextUGUIBox = textUGUIObj.GetComponent<TextMeshProUGUI>();
                //m_myTextUGUIBox.rectTransform.sizeDelta = new Vector3(kWidth, 20);
                string newText = Database.descriptionMessage;
                textObj.GetComponent<Text>().text = newText;
                //textUGUIObj.GetComponent<TextMeshProUGUI>().text = newText;
                TextGenerator textGen = new TextGenerator();
                TextGenerationSettings generationSettings = m_myTextBox.GetGenerationSettings(m_myTextBox.rectTransform.rect.size);
                float width = textGen.GetPreferredWidth(newText, generationSettings) / 10f;
                float height = textGen.GetPreferredHeight(newText, generationSettings) / 10f;
                if (maxWidth < width)
                {
                    maxWidth = width;
                }
                //print("width " + width + ",height " + height);
                if (width < kWidth)
                {
                    m_myTextBox.rectTransform.sizeDelta = new Vector3(width, height);
                    //m_myTextUGUIBox.rectTransform.sizeDelta = new Vector3(width, height);
                    panel.GetComponent<RectTransform>().sizeDelta = new Vector3(width + 2, height + 2);
                }
                else
                {
                    m_myTextBox.rectTransform.sizeDelta = new Vector3(kWidth, height);
                    //m_myTextUGUIBox.rectTransform.sizeDelta = new Vector3(kWidth, height);
                    panel.GetComponent<RectTransform>().sizeDelta = new Vector3(kWidth + 2, height + 2);
                }
                //m_myTextBox.rectTransform.sizeDelta = new Vector3(width, height);
                //panel.GetComponent<RectTransform>().sizeDelta = new Vector3(width + 20, height + 20);
                //Vector2 mousePosition = new Vector2(Input.mousePosition.x * 1f / Screen.width * 160f, Input.mousePosition.y * 1f / Screen.height * 90f);
                //float ratio = ((float)Screen.width) / 1600f;
                GetComponent<RectTransform>().anchoredPosition = _pos + new Vector2(0, panel.GetComponent<RectTransform>().sizeDelta.y + Database.descriptionMessageAppendY) / 2;

                if (_align == "left")
                {
                    GetComponent<RectTransform>().anchoredPosition -= Vector2.left * panel.GetComponent<RectTransform>().sizeDelta.x / 2f;
                }
                if (_align == "right")
                {
                    GetComponent<RectTransform>().anchoredPosition -= Vector2.right * panel.GetComponent<RectTransform>().sizeDelta.x / 2f;
                }

                panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                mHeight += (height + 4);
            }

            if (Database.descriptionMessage2.Trim().Length > 0)
            {
                transform.SetAsLastSibling();
                Text m_myTextBox = textObj2.GetComponent<Text>();
                m_myTextBox.rectTransform.sizeDelta = new Vector3(kWidth, 200);
                textObj2.GetComponent<Text>().text = Database.descriptionMessage2;
                TextGenerator textGen = new TextGenerator();
                TextGenerationSettings generationSettings = m_myTextBox.GetGenerationSettings(m_myTextBox.rectTransform.rect.size);
                float width = textGen.GetPreferredWidth(Database.descriptionMessage2, generationSettings) / 10f;
                float height = textGen.GetPreferredHeight(Database.descriptionMessage2, generationSettings) / 10f;
                if (maxWidth < width)
                {
                    maxWidth = width;
                }
                //print("width " + width + ",height " + height);
                if (width < kWidth)
                {
                    m_myTextBox.rectTransform.sizeDelta = new Vector3(width, height);
                    panel2.GetComponent<RectTransform>().sizeDelta = new Vector3(width + 2, height + 2);
                }
                else
                {
                    m_myTextBox.rectTransform.sizeDelta = new Vector3(kWidth, height);
                    panel2.GetComponent<RectTransform>().sizeDelta = new Vector3(kWidth + 2, height + 2);
                }
                //m_myTextBox.rectTransform.sizeDelta = new Vector3(width, height);
                //panel.GetComponent<RectTransform>().sizeDelta = new Vector3(width + 20, height + 20);
                //Vector2 mousePosition = Input.mousePosition;
                float ratio = ((float)Screen.width) / 160f;
                panel2.transform.localPosition = new Vector2(0, panel.transform.localPosition.y + panel.GetComponent<RectTransform>().sizeDelta.y / 2f + panel2.GetComponent<RectTransform>().sizeDelta.y / 2f + 10f);
                mHeight += (height + 4);
            }
            else
            {
                panel2.transform.localPosition = new Vector2(0, 1000);
            }

            if (Database.descriptionMessage3.Trim().Length > 0)
            {
                transform.SetAsLastSibling();
                Text m_myTextBox = textObj3.GetComponent<Text>();
                m_myTextBox.rectTransform.sizeDelta = new Vector3(kWidth, 200);
                textObj3.GetComponent<Text>().text = Database.descriptionMessage3;
                TextGenerator textGen = new TextGenerator();
                TextGenerationSettings generationSettings = m_myTextBox.GetGenerationSettings(m_myTextBox.rectTransform.rect.size);
                float width = textGen.GetPreferredWidth(Database.descriptionMessage3, generationSettings) / 10f;
                float height = textGen.GetPreferredHeight(Database.descriptionMessage3, generationSettings) / 10f;
                if (maxWidth < width)
                {
                    maxWidth = width;
                }
                //print("width " + width + ",height " + height);
                if (width < kWidth)
                {
                    m_myTextBox.rectTransform.sizeDelta = new Vector3(width, height);
                    panel3.GetComponent<RectTransform>().sizeDelta = new Vector3(width + 2, height + 2);
                }
                else
                {
                    m_myTextBox.rectTransform.sizeDelta = new Vector3(kWidth, height);
                    panel3.GetComponent<RectTransform>().sizeDelta = new Vector3(kWidth + 2, height + 2);
                }
                //m_myTextBox.rectTransform.sizeDelta = new Vector3(width, height);
                //panel.GetComponent<RectTransform>().sizeDelta = new Vector3(width + 20, height + 20);
                //Vector2 mousePosition = Input.mousePosition;
                float ratio = ((float)Screen.width) / 1600f;
                panel3.transform.localPosition = new Vector2(0, panel2.transform.localPosition.y + panel2.GetComponent<RectTransform>().sizeDelta.y / 2f + panel3.GetComponent<RectTransform>().sizeDelta.y / 2f + 10f);
                mHeight += (height + 40);
            }
            else
            {
                panel3.transform.localPosition = new Vector2(0, 1000);
            }

            if (GetComponent<RectTransform>().anchoredPosition.y + mHeight > 90)
            {
                GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition + new Vector2(0, (transform.position.y / Screen.height * 90 + mHeight + 5 - 90));

                //if (transform.position.y + mHeight > Screen.height)
                //{
                //    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - (transform.position.y + mHeight + 50 - Screen.height), transform.localPosition.z);
                //Vector2 _temp = panel3.transform.localPosition;

                if (Database.descriptionMessage3.Trim().Length > 0)
                {
                    panel3.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, panel3.GetComponent<RectTransform>().sizeDelta.y / 2f - 1f);
                    panel2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, panel3.GetComponent<RectTransform>().sizeDelta.y + panel2.GetComponent<RectTransform>().sizeDelta.y / 2f - 1f);
                    panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, panel3.GetComponent<RectTransform>().sizeDelta.y + panel2.GetComponent<RectTransform>().sizeDelta.y + panel.GetComponent<RectTransform>().sizeDelta.y / 2f - 1f);
                }
                else if (Database.descriptionMessage2.Trim().Length > 0)
                {
                    panel2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, panel2.GetComponent<RectTransform>().sizeDelta.y / 2f - 1f);
                    panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, panel2.GetComponent<RectTransform>().sizeDelta.y + panel.GetComponent<RectTransform>().sizeDelta.y / 2f - 1f);
                }
                else
                {
                    panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, panel.GetComponent<RectTransform>().sizeDelta.y / 2f - 1f);
                }

            }

            if (maxWidth > kWidth)
            {
                maxWidth = kWidth;
            }

            if (GetComponent<RectTransform>().anchoredPosition.x + maxWidth / 2f + 5 > 80)
            {
                GetComponent<RectTransform>().anchoredPosition = (GetComponent<RectTransform>().anchoredPosition + new Vector2((160 - (transform.position.x / Screen.width * 160 + maxWidth / 2f + 5)), 0));
            }

            if (GetComponent<RectTransform>().anchoredPosition.x - maxWidth / 2f - 5 < -80)
            {
                GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition + new Vector2(-(transform.position.x / Screen.width * 160 - maxWidth / 2f - 5), 0);
            }

        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 1000);
            alpha = 0f;

            //foreach (GameObject _go in gameObject.GetComponentsInChildren<GameObject>())
            //{

            //}
        }
    }
}
