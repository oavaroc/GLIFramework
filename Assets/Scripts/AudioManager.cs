using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField]
    private AudioSource _backgroundMusic;
    [SerializeField]
    private AudioSource _enemyBreached;
    [SerializeField]
    private AudioSource _forceBarrierHit;
    [SerializeField]
    private AudioSource _robotDeath;
    [SerializeField]
    private AudioSource _emptyFire;
    [SerializeField]
    private AudioSource _gunFire;
    [SerializeField]
    private AudioSource _reloadSound;

    public void PlayBackgroundMusic()
    {
        _backgroundMusic.Play();
    }
    public void PlayEnemyBreached()
    {
        _enemyBreached.PlayOneShot(_enemyBreached.clip);
    }
    public void PlayForceBarrierHit()
    {
        _forceBarrierHit.PlayOneShot(_forceBarrierHit.clip);
    }
    public void PlayRobotDeath()
    {
        _robotDeath.PlayOneShot(_robotDeath.clip);
    }
    public void PlayEmptyFire()
    {
        _emptyFire.PlayOneShot(_emptyFire.clip);
    }
    public void PlayGunFire()
    {
        _gunFire.PlayOneShot(_gunFire.clip);
    }
    public void PlayReloadSound()
    {
        _reloadSound.Play();
    }

}
