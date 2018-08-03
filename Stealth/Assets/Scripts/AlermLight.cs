using UnityEngine;
using System.Collections;

public class AlermLight : MonoBehaviour {
    private Light _light;//声明的对灯光的引用
    private float lowIntensity = 0f;//低值
    private float heighIntensity = 1.5f;//高值
    private float targetIntensity;//线性插值函数的目标值
    public float intensitySpeed = 1.0f;//线性插值函数的差值变化速率
    public bool alermOn = false;//触发警报的判断条件
    public static AlermLight _intance;
    public static AlermLight Intance
    {
        get
        {
            return _intance;
        }
        set
        {
            _intance = value;
        }
    }
    private AlermLight() { }
	// Use this for initialization
	void Start () {
        _intance = this;
        _light = GetComponent<Light>();
        _light.intensity = 0;
        targetIntensity = heighIntensity;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (alermOn == true)
        {
            _light.intensity = Mathf.Lerp(_light.intensity, targetIntensity, Time.deltaTime * intensitySpeed);
            if ((Mathf.Abs(_light.intensity - targetIntensity)) <= 0.05f)//因为线性插值使得fromintensity永远不会与tointensity相等，并且到后来每次变化的值也会减小
            {//如果达到了这种地步将起始的两个值对调，使灯光渐渐变暗，达到自然的情况
                if (targetIntensity == heighIntensity)
                {//当目标数值为高数值时，需要将它换成低数值
                    targetIntensity = lowIntensity;

                }
                else
                {//当目标数值是低数值时，将它换成高数值，如此可实现由高到低由低到高的渐变过程
                    if (targetIntensity == lowIntensity)
                    {
                        targetIntensity = heighIntensity;
                    }
                }
            }
        }
        else
        {
            if (alermOn == false)
            {
                _light.intensity = Mathf.Lerp(_light.intensity, 0, Time.deltaTime * intensitySpeed);
                if (Mathf.Abs(_light.intensity - 0) <= 0.05)
                {
                    return;
                }
            }
        }
        
           

    }
}
