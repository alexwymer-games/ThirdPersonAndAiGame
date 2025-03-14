using UnityEngine;
using UnityEngine.UI;

public class AgentHealthBar : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public Image backgroundImage;
    public Image foregroundImage;
    

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = (target.position - Camera.main.transform.position).normalized;

        bool isBehind = Vector3.Dot(direction, Camera.main.transform.forward) <= 0;

        backgroundImage.enabled = !isBehind;
        foregroundImage.enabled = !isBehind;

        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);    
    }


    public void SetHealthBarPercentage(float percentage)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;

        float width = parentWidth * percentage;

        foregroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
