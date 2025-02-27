﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    public Pathology pathology;
    int _pathologyStep = 0;
    float lifetime = 0;
    bool _isCured;

    public AudioSource fartAudioSource, OKAudioSource, OhNoAudioSource, WowAudioSource;

    void Awake()
    {
        lifetime = pathology.timeOfLife;
    }

    void Start()
    {
        GameManager.Instance.RegisterPatient();

        Invoke("CheckIfDead", lifetime);
    }

    public void ReceiveActuator(Actuator actuator)
    {
        if (actuator.id == pathology.actuators[_pathologyStep].id)
        {
            _pathologyStep++;

            if (_pathologyStep >= pathology.actuators.Count)
            {
                _isCured = true;
                Debug.LogFormat(this, "Patient {0} is cured! Good job!", name);
                _pathologyStep = 0;
                
                WowAudioSource.Play();
                GameManager.Instance.PatientCured();
                Leave();
                
                return;
            }

            OKAudioSource.Play();
            Debug.Log("Seems that will do...");
        }
        else
        {
            Debug.Log("Dude, how are you even a doctor?");
            fartAudioSource.Play();
            GameManager.Instance.WrongAction();
        }
    }

    void Leave()
    {

    }

    void CheckIfDead()
    {
        if (!_isCured)
        {
            Debug.LogFormat("Patient {0} died you fool!", name);
            OhNoAudioSource.Play();
        }
    }
}
