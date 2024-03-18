using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundsCollectionSO : ScriptableObject
{

    [Header("Music")]
    public SoundSO[] FightMusic;
    public SoundSO[] DiscoBallMusic;

    [Header("SFX")]
    public SoundSO[] GunShoot;
    public SoundSO[] Jump;
    public SoundSO[] Land;
    public SoundSO[] Splat;
    public SoundSO[] BulletHit;
    public SoundSO[] Jetpack;
    public SoundSO[] GrenadeShoot;
    public SoundSO[] GrenadeBeep;
    public SoundSO[] GrenadeExplode;
    public SoundSO[] PlayerHit;
    public SoundSO[] MegaKill;
    public SoundSO[] Flamethrower;
}
