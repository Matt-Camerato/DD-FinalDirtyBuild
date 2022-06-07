using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreCompManager : MonoBehaviour
{
    public GameObject preCompPanel;

    //for displaying all your current units
    public RectTransform contentTransform;
    public GameObject slotPrefab;
    public GameObject unitDisplayPrefab;

    //for displaying current selected team
    public RectTransform teamSlots;

    public void PreCompScreen()
    {
        preCompPanel.SetActive(true); //turn on pre comp screen
        PreCompSetup(); //must be done after comp panel is active
    }

    private void PreCompSetup()
    {
        //reset slots on pre comp screen
        foreach (Transform child in contentTransform) Destroy(child.gameObject); //delete any scrollview units

        foreach (Transform child in teamSlots)
        {
            if (child.childCount == 0) continue;
            
            foreach (Transform c in child)
            {
                c.gameObject.SetActive(false);
                c.transform.parent = transform;
            }
        }

        //set size of content area for scroll view
        contentTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, UnitManager.CurrentUnits.Count * 3f); //each unit has a width of 3

        //loop through current team 
        foreach (UnitData data in UnitManager.CurrentUnits)
        {
            //create slot for unit
            GameObject slotObj = Instantiate(slotPrefab, contentTransform);
            UIDropSlot slot = slotObj.GetComponent<UIDropSlot>();

            //create unit display and link it to slot
            GameObject unitDisplayObj = Instantiate(unitDisplayPrefab, slot.transform.position, Quaternion.identity, slot.transform);
            unitDisplayObj.transform.localPosition = Vector3.zero;
            unitDisplayObj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            UIDragItem item = unitDisplayObj.GetComponent<UIDragItem>();
            item.Link(slot);
            UnitTempDisplay display = unitDisplayObj.GetComponent<UnitTempDisplay>();
            display.SetData(data);
        }
    }

    public void GoButton()
    {
        List<UnitData> team = new List<UnitData>();
        foreach(Transform child in teamSlots)
        {
            if (child.childCount == 0) continue;
            team.Add(child.GetChild(0).GetComponent<UnitTempDisplay>().data);
        }
        UnitListStaticRef.Starters = team;
        SceneManager.LoadScene(1);
    }

    public void ReturnButton() => preCompPanel.SetActive(false); //turn off pre comp screen
}
