using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ExceptionLogger
{
    // Method to recursively build the exception message including inner exceptions
    public static string BuildExceptionMessage(Exception exception)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Exception caught:");

        // Loop to handle case where there are multiple nested inner exceptions
        Exception currentException = exception;
        int exceptionLayer = 0; // To track the depth of the inner exception
        while (currentException != null)
        {
            sb.AppendLine($"Exception Layer {exceptionLayer}: {currentException.GetType().FullName}");
            sb.AppendLine($"Message: {currentException.Message}");
            sb.AppendLine($"StackTrace: {currentException.StackTrace}");

            currentException = currentException.InnerException;
            exceptionLayer++;
            if (currentException != null)
            {
                sb.AppendLine($"--- Inner Exception {exceptionLayer} ---");
            }
        }

        return sb.ToString();
    }

    // Example usage within a catch block
    public static void LogException(Exception ex)
    {
        string detailedErrorMessage = BuildExceptionMessage(ex);
        Debug.LogError(detailedErrorMessage);
    }
}
