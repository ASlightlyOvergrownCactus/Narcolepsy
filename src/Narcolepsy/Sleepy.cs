using UnityEngine;

namespace Narcolepsy;

public class Sleepy
{
    public int sleepTick;
    public Sleepy()
    {
        sleepTick = 0;
    }

    public void GoToSleep()
    {
        sleepTick = Random.Range(100, 600);
    }
}