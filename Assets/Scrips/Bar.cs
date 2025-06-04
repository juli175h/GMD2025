using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient;
    public Image fill;

    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
        if(fill != null)
        fill.color = gradient.Evaluate(1f);
    }
    public void SetValue(float value)
    {
        slider.value = value;
        if (fill != null)
            fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
