using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarControl : MonoBehaviour
{
    public Image imgCD;
    public float CD;
    bool isCD = false;

    public Image imgDur;
    public float Dur;
    bool isDur = false;

    public void StartCD(float cooldown)
    {
        CD = cooldown;
        isCD = true;
    }

    public void StartDuration(float dur)
    {
        Dur = dur-0.3f;
        isDur = true;
    }

    private void Update()
    {
        if(isCD)
        {
            imgDur.fillAmount = 0;
            isDur = false;

            imgCD.fillAmount += 1 / CD * Time.deltaTime;

            if (imgCD.fillAmount >= 1)
            {
                imgCD.fillAmount = 0;
                isCD = false;
            }
        }

        if(isDur)
        {
            imgDur.fillAmount += 1 / Dur * Time.deltaTime;

            if (imgDur.fillAmount >= 1)
            {
                imgDur.fillAmount = 0;
                isDur = false;
            }
        }
    }
}
