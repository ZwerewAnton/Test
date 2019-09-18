using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RotationController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    private Image bgImg;
    private Image stickImg;
    private Vector3 inputVector;
    private bool isShoot = false, longShoot = false, drag = false, quickShoot = false;
    float timeLeft = 0f;
    public GameObject player;
    void Start()
    {
        bgImg = GetComponent<Image>();
        stickImg = transform.GetChild(0).GetComponent<Image>();
    }

    void LateUpdate()
    {
        //Debug.Log(inputVector);
        timeLeft -= Time.deltaTime;
        if (!drag && timeLeft <=0)
        {
            inputVector = Vector3.zero;
        }
        longShoot = false;
        quickShoot = false;




    }
    public void OnEndDrag(PointerEventData eventData)
    {
        drag = false;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        drag = true;
        isShoot = false;
        GetComponent<Image>().enabled = true;
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);
            inputVector = new Vector3(pos.x*2 + 1, 0, pos.y * 2 - 1);
            if (inputVector.magnitude > 1.0f)
            {
                inputVector = inputVector.normalized;
            }

            stickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3),
                                                                                        inputVector.z * (bgImg.rectTransform.sizeDelta.x / 3));
        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        isShoot = true;
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        if (isShoot)
        {
            
            quickShoot = true;
            timeLeft = 1f;

            //inputVector.x = closestEnemy.transform.position.x;
            //inputVector.z = closestEnemy.transform.position.y;
            inputVector.x = 0f;
            // Debug.Log("quickshoot");
            // player.transform.LookAt(closestEnemy.transform);
        }
        else if(!isShoot && inputVector.magnitude != 0)
        {
            longShoot = true;
            //Debug.Log("lookshoot");
            // Debug.Log(inputVector.magnitude);
        }
        stickImg.rectTransform.anchoredPosition = Vector3.zero;
        GetComponent<Image>().enabled = false;
        isShoot = false;
        drag = false;
    }


    public float Horizontal()
    {
       return inputVector.x;
    }
    public float Vertical()
    { 
        return inputVector.z;
    }
    public bool LongShoot()
    {
        return longShoot;
    }
    public bool QuickShoot()
    {
        return quickShoot;
    }

}
