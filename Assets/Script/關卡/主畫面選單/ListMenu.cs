using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListMenu : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject[] listMeuns;
    public GameObject nextBtn;
    public GameObject prevBtn;
    int currentPage;
    private void OnEnable()
    {
        CloseAllMenu();
        currentPage = 0;
        listMeuns[0].SetActive(true);
        CheckCurrentPage();
        //SelectListPage(currentPage);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void NextListPage()
    {
        CloseAllMenu();
        currentPage += 1;
        if (currentPage <= listMeuns.Length - 1)
        {
            listMeuns[currentPage].SetActive(true);
        }
        CheckCurrentPage();

    }
    public void BackListPage()
    {
        CloseAllMenu();
        currentPage -= 1;
        if (currentPage >= 0)
        {
            listMeuns[currentPage].SetActive(true);
        }
        CheckCurrentPage();
    }
    void CheckCurrentPage()
    {
        if (currentPage == listMeuns.Length - 1)
        {
            nextBtn.SetActive(false);
        }
        else
        {
            nextBtn.SetActive(true);
        }

        if (currentPage == 0)
        {
            prevBtn.SetActive(false);
        }
        else
        {
            prevBtn.SetActive(true);
        }
    }


    void CloseAllMenu()
    {
        foreach (GameObject i in listMeuns)
        {
            i.SetActive(false);
        }
    }
}
