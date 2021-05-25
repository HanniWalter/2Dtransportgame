using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameLogicException : System.Exception
{
    public GameLogicException() { }
    public GameLogicException(string message) : base(message) { }
    public GameLogicException(string message, System.Exception inner) : base(message, inner) { }
    protected GameLogicException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
