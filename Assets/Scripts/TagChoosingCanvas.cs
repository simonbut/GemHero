using ClassHelper;
using UnityEngine;
using UnityEngine.UI;

public class TagChoosingCanvas : ControlableUI
{

    public GameObject tagDescription;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayTagDescription(int _tagId)
    {
        if (_tagId <= 0)
        {
            tagDescription.SetActive(false);
            return;
        }
        tagDescription.SetActive(true);

        TagData _td = TagManager.Instance.GetTagData(_tagId);

        tagDescription.transform.Find("Text").GetComponent<Text>().text = _td.name.GetString() + "\n" + _td.description.GetString();
        if (_td.tagType == TagType.CharacterTag)
        {
            foreach (Tag _t in Database.userDataJson.playerTags)
            {
                if (_t.tagDataId == _tagId && !_t.isReady)
                {
                    tagDescription.transform.Find("Text").GetComponent<Text>().text = _td.name.GetString() + "(Not Ready)" + "\n" + _td.description.GetString();
                }
            }
        }
    }
}
