using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using System.Security.Permissions;
using UnityEngine;
using RWCustom;
using System.Security;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Debug = System.Diagnostics.Debug;
using Random = UnityEngine.Random;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

namespace Narcolepsy;
[BepInPlugin(GUID: MOD_ID, Name: MOD_NAME, Version: VERSION)]
public class Plugin : BaseUnityPlugin
{
    public const string MOD_ID = "cactus.eepy";
    public const string MOD_NAME = "Narcolepsy";
    public const string VERSION = "1.0";
    public const string AUTHORS = "ASlightlyOverGrownCactus, aprilWorstMonth, Turt, Ethan Barron, Intikus";

    private readonly ConditionalWeakTable<Creature, Sleepy> sleepyCWT = new();

    public void OnEnable()
    {
        try
        {
            On.Creature.ctor += CreatureOnctor;
            On.Creature.Update += CreatureOnUpdate;
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("what the hell!!!" + e);
        }
    }

    private void CreatureOnctor(On.Creature.orig_ctor orig, Creature self, AbstractCreature abstractcreature, World world)
    {
        orig(self, abstractcreature, world);
        sleepyCWT.Add(self, new Sleepy());
    }

    private void CreatureOnUpdate(On.Creature.orig_Update orig, Creature self, bool eu)
    {
        orig(self, eu);
        bool sleepy = false;
        sleepyCWT.TryGetValue(self, out Sleepy eep);
        if (eep.sleepTick == 0)
        {
            int rand = Random.Range(0, 3500);
            if (rand >= 3499)
            {
                self.Stun(Random.Range(50, 800));
                sleepy = true;
            }
            else if (rand >= 3495)
            {
                self.Stun(Random.Range(50, 500));
                sleepy = true;
            }
            else if (rand >= 3490)
            {
                self.Stun(Random.Range(50, 350));
                sleepy = true;
            }
            else if (rand >= 3475)
            {
                self.Stun(Random.Range(50, 100));
                sleepy = true;
            }
            else if (rand >= 3450)
            {
                self.Stun(Random.Range(25, 50));
                sleepy = true;
            }
        }
        else
        {
            eep.sleepTick -= 1;
        }
        if (sleepy)
        {
            eep.GoToSleep();
        }
            
    }
}
