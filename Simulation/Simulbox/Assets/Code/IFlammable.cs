using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlammable
{
    void Ignite(float timeToLive);
}