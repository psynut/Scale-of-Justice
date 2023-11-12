using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MusicLevelCommand
{
    public enum Command { Stop, OnePlay, Loop, PlayFirstLoopSecond };
    public AudioClip[] songs;
    public Command command;
}
