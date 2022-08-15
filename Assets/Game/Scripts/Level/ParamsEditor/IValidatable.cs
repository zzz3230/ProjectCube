using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ValidateResult 
{ 
    public bool result; 
    public string message;
    
    public static ValidateResult Ok()
    {
        return new ValidateResult { result = true };
    }
    public static ValidateResult Error(string msg)
    {
        return new ValidateResult { result = false, message = msg };
    }
}

public interface IValidatable
{
    
    public ValidateResult Validate();
}
