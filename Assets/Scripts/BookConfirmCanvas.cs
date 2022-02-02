using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class BookConfirmCanvas : ControlableUI
{
    int bookId;
    public GameObject content;

    public void AddUI(int _bookId)
    {
        bookId = _bookId;
        AddUI();
    }

    public override void AddUI()
    {
        BookData _bd = ResourcePointManager.Instance.GetBookData(bookId);
        content.transform.Find("Name").GetComponent<Text>().text = _bd.name.GetString();
        content.transform.Find("Text").GetComponent<Text>().text = _bd.GetBookDescription();

        base.AddUI();

        UIManager.Instance.choiceUI.Setup(new Vector2(Screen.width / 2f, Screen.height / 2f), new List<string> { "Confirm", "Cancel" }, new List<Callback> { Confirm, Cancel });
    }

    void Confirm()
    {
        BookData _bd = ResourcePointManager.Instance.GetBookData(bookId);
        Database.TimePass(_bd.time * 60);

        Database.AddBook(bookId);

        OnBackPressed();
        OnBackPressed();
    }

    void Cancel()
    {
        OnBackPressed();
        OnBackPressed();
        MainGameView.Instance.libraryCanvas.AddUI();
    }
}
