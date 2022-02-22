using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class LibraryCanvas : ControlableUI
{
    public List<GameObject> bookGameobjectList;

    int libraryPointId;
    public List<int> bookList;

    private void Update()
    {
        if (ControlView.Instance.controls.Map1.Cancel.triggered)
        {
            OnBackPressed();
        }
    }

    public void AddUI(int _libraryPointId)
    {
        libraryPointId = _libraryPointId;
        bookList = ResourcePointManager.Instance.GetResourcePointDataList(_libraryPointId)[0].bookId;

        //Refresh();

        AddUI();
    }

    void Refresh()
    {
        for (int i = 0; i < bookGameobjectList.Count; i++)
        {
            bookGameobjectList[i].SetActive(false);
        }

        for (int i = 0; i < bookList.Count; i++)
        {
            if (!Database.userDataJson.book.Contains(bookList[i]))
            {
                bookGameobjectList[i].SetActive(true);
            }

            BookData _bd = ResourcePointManager.Instance.GetBookData(bookList[i]);
            bookGameobjectList[i].transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Book/" + _bd.id.ToString("000"));
            bookGameobjectList[i].transform.Find("Name").GetComponent<Text>().text = _bd.name.GetString();
            bookGameobjectList[i].transform.Find("Text").GetComponent<Text>().text = _bd.description.GetString();
            bookGameobjectList[i].transform.Find("Time").GetComponent<Text>().text = "Time to read: $1h".Replace("$1", _bd.time.ToString("0"));
        }
    }

    public override void OnShow()
    {
        Refresh();

        base.OnShow();
    }

    public void ChooseBook(int _index)
    {
        OnBackPressed();

        MainGameView.Instance.bookConfirmCanvas.AddUI(bookList[_index]);
    }
}
