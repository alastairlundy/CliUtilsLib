/*
    CliUtilsLib
    Copyright (C) 2024  Alastair Lundy

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, version 3 of the License.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;

using AlastairLundy.Extensions.System.Strings.EscapeCharacters;

// ReSharper disable RedundantBoolCompare

namespace CliUtilsLib;

public static class EscapeCharacterRemover
{

    [Obsolete("Use Remove method instead.")]
    public static IEnumerable<string> RemoveToNew(IEnumerable<string> args)
    {
        return Remove(args);
    }
    
    /// <summary>
    /// Creates a new string array with the Escape Characters removed from the searched string array.
    /// </summary>
    /// <param name="args">The array to be searched.</param>
    /// <returns>the new string array with the Escape Characters removed.</returns>
    public static IEnumerable<string> Remove(IEnumerable<string> args)
    {
        string[] enumerable = args as string[] ?? args.ToArray();

        return enumerable.Select(x => x.RemoveEscapeCharacters());
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
        
        bool[] containsEscapeChars = enumerable.Select(x => x.ContainsEscapeCharacters()).ToArray();

        if (containsEscapeChars.All(x => x == true))
        {
            output = enumerable;
            return false;
        }

        try
        {
            output = Remove(enumerable);
            return true;
        }
        catch
        {
            output = enumerable;
            return false;
        }
    }
}