/*
    Copyright 2024 Alastair Lundy

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */

using System.Collections.Generic;
using System.Linq;

using AlastairLundy.Extensions.System;
using AlastairLundy.Extensions.System.EscapeCharacters;

namespace CliUtilsLib;

public static class EscapeCharacterRemover
{
    /// <summary>
    /// Returns a modified string array with the Escape Characters removed.
    /// </summary>
    /// <param name="args">The string array to be modified.</param>
    /// <returns>a modified string array with the Escape Characters removed.</returns>
    public static IEnumerable<string> Remove(IEnumerable<string> args)
    {
        string[] enumerable = args as string[] ?? args.ToArray();
        
        foreach (string arg in enumerable)
        {
            arg.RemoveEscapeCharacters();
        }

        return enumerable;
    }
    
    /// <summary>
    /// Creates a new string array with the Escape Characters removed from the searched string array.
    /// </summary>
    /// <param name="args">The array to be searched.</param>
    /// <returns>the new string array with the Escape Characters removed.</returns>
    public static IEnumerable<string> RemoveToNew(IEnumerable<string> args)
    {
        string[] enumerable = args as string[] ?? args.ToArray();
        
        string[] newArgs = new string[enumerable.Length];
        
        enumerable.CopyTo(newArgs, 0);
        
        foreach (string arg in newArgs)
        {
            arg.RemoveEscapeCharacters();
        }

        return newArgs;
    }

    /// <summary>
    /// Attempts to remove Escape Characters from a string array.
    /// </summary>
    /// <param name="input">The string array to be searched.</param>
    /// <param name="output">The modified string array.</param>
    /// <returns>true if Escape Characters were found and removed; return false if no Escape Characters were found.</returns>
    public static bool TryRemove(IEnumerable<string> input, out IEnumerable<string> output)
    {
        string[] enumerable = input as string[] ?? input.ToArray();
        
        bool[] containsEscapeChars = new bool[enumerable.Length];

        for (int index = 0; index < enumerable.Length; index++)
        {
            containsEscapeChars[index] = enumerable[index].ContainsEscapeCharacters();
        }

        if (containsEscapeChars.IsAllFalse())
        {
            output = enumerable;
            return false;
        }

        try
        {
            output = RemoveToNew(enumerable);
            return true;
        }
        catch
        {
            output = enumerable;
            return false;
        }
    }
}