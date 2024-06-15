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

using AlastairLundy.Extensions.System.BoolArrayExtensions;
using AlastairLundy.Extensions.System.StringExtensions;

namespace CliUtilsLib;

public static class EscapeCharacterRemover
{
    /// <summary>
    /// Returns a modified string array with the Escape Characters removed.
    /// </summary>
    /// <param name="args">The string array to be modified.</param>
    /// <returns>a modified string array with the Escape Characters removed</returns>
    public static string[] Remove(string[] args)
    {
        string[] newArgs = new string[args.Length];
        
        args.CopyTo(newArgs, 0);
        
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
    public static bool TryRemove(string[] input, out string[] output)
    {
        bool[] containsEscapeChars = new bool[input.Length];

        for (int index = 0; index < input.Length; index++)
        {
            containsEscapeChars[index] = input[index].ContainsEscapeCharacters();
        }

        if (containsEscapeChars.IsAllFalse())
        {
            output = input;
            return false;
        }
        
        List<string> newInput = new List<string>();
        
        foreach (string s in input)
        {
            if (s.ContainsEscapeCharacters())
            {
                s.RemoveEscapeCharacters();
                newInput.Add(s);
            }
            else
            {
                newInput.Add(s);
            }
        }

        output = newInput.ToArray();
        return true;
    }
}