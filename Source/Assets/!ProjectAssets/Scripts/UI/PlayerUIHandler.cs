using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUIHandler : MonoBehaviour 
{
    public Image bar, a, b, x, y;
    private float ypos;
    private float xwidth;
    private float xstart;

    private float minx;
    private float maxx;

    private float adone, bdone, xdone, ydone;

    void Start ()
    {
        
	}

    public void SetUp(Image healthBar, Image nx, Image ny, Image na, Image nb, CharController owner)
    {
        bar = healthBar;
        a = na;
        b = nb;
        x = nx;
        y = ny;
        owner.stats.OnHealthChange += SetHealth;
    }

    public void SetHealth(int currHealth, int maxHealth)
    {
        float healthFraction = (float)currHealth / (float)maxHealth;
        //color changing would go here if there's time.
        bar.fillAmount = healthFraction;
    }

    public void StartCooldown(char slot, float duration)
    {
        switch (slot)
        {
            case 'A':
                StartCoroutine(CooldownHandler(a, Time.time, duration));
                break;
            case 'B':
                StartCoroutine(CooldownHandler(b, Time.time, duration));
                break;
            case 'X':
                StartCoroutine(CooldownHandler(x, Time.time, duration));
                break;
            case 'Y':
                StartCoroutine(CooldownHandler(y, Time.time, duration));
                break;
        }
    }

    IEnumerator CooldownHandler(Image i, float startTime, float duration)
    {
        i.fillAmount = 0f;
        while (Time.time < startTime + duration)
        {
            i.fillAmount = (Time.time - startTime) / duration;
            yield return null;
        }
        i.fillAmount = 1f;
        yield return null;
    }

    public void ResetCooldown(char slot)
    {
    }
}
