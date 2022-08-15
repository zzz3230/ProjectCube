using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(RectTransform))]
public class UILineScript : MonoBehaviour
{
    [SerializeField] private RectTransform object1;
    [SerializeField] private RectTransform object2;
    [SerializeField] private float _width = 5;
    private Image image;
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetPositions(Vector3 startPos, Vector3 endPos)
    {
        if(startPos == endPos)
            return;
        
        if (startPos.x > endPos.x)
        {
            (startPos, endPos) = (endPos, startPos);
        }
        
        object1.position = startPos;
        object2.position = endPos;
    }
    
    public void SetObjects(GameObject one, GameObject two)
    {
        object1 = one.GetComponent<RectTransform>();
        object2 = two.GetComponent<RectTransform>();

        RectTransform aux;
        if (object1.position.x > object2.position.x)
        {
            aux = object1;
            object1 = object2;
            object2 = aux;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (object1.gameObject.activeSelf && object2.gameObject.activeSelf)
        {
            rectTransform.position = (object1.position + object2.position) / 2;
            Vector3 dif = object2.position - object1.position;
            rectTransform.sizeDelta = new Vector3(dif.magnitude, _width);
            rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 180 * Mathf.Atan(dif.y / dif.x) / Mathf.PI));
        }
    }


}
