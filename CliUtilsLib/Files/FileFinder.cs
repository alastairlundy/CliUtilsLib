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
using System.IO;

namespace CliUtilsLib;

/// <summary>
/// 
/// </summary>
[Obsolete("Use IOExtensions FileFinder instead.")]
public static class FileFinder
{
    /// <summary>
    /// Determines whether a string is the name of a file.
    /// </summary>
    /// <param name="arg">The string to be searched.</param>
    /// <returns>true if the string is a file; return false otherwise.</returns>
    [Obsolete("Use IOExtensions FileFinder instead.")]
    public static bool IsAFile(string arg)
    {
        try
        {
            if (File.Exists(arg))
            {
                return true;
            }
            
            if (arg.Length > 1)
            {
                if (arg.Length - 4 >= 0 && arg.Length - 4 < arg.Length)
                {
                    // Uses new .NET 6 and newer ^ Index
                    if (arg[^4].Equals('.'))
                    {
                        return true;
                    }
                }
                if (arg.Length - 3 >= 0 && arg.Length - 3 < arg.Length)
                {
                    // Uses new .NET 6 and newer ^ Index
                    if (arg[^3].Equals('.') || arg[^2].Equals('.'))
                    {
                        return true;
                    }
                }

                if (arg.Length - 2 >= 0 && arg.Length - 2 < arg.Length)
                {
                    // Uses new .NET 6 and newer ^ Index
                    if (arg[^2].Equals('.'))
                    {
                        return true;
                    }
                }
            }
            
            return File.Exists(arg);
        }
        catch
        {
            return false;
        }
    }
}