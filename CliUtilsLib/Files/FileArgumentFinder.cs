﻿/*
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

// ReSharper disable UseIndexFromEndExpression

using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable RedundantBoolCompare

namespace CliUtilsLib;

/// <summary>
/// A class to assist with finding files within a string array.
/// </summary>
public static class FileArgumentFinder
{
    /// <summary>
    /// Returns a tuple of files that appear before and after a separator in a string array;
    /// </summary>
    /// <param name="args">The IEnumerable of type string to be searched.</param>
    /// <param name="separator">The separator to look for.</param>
    /// <returns>a tuple containing the files before and after a separator if found; returns null if no files were found in the array.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the separator is null.</exception>
    public static (IEnumerable<string> filesBeforeSeparator, IEnumerable<string> filesAfterSeparator)? GetFilesBeforeAndAfterSeparator(
        IEnumerable<string> args, char separator)
    {
        List<string> filesBefore = new List<string>();
        List<string> filesAfter = new List<string>();

        string[] enumerable = args as string[] ?? args.ToArray();
        
        if (!enumerable.Contains(separator.ToString()))
        {
            throw new ArgumentNullException(nameof(separator));
        }

        bool foundSeparator = true;

        foreach (string s in enumerable)
        {
            if (s.Equals(separator.ToString()))
            {
                foundSeparator = true;
            }

            AlastairLundy.Extensions.IO.Files.FileFinder
                fileFinder = new AlastairLundy.Extensions.IO.Files.FileFinder();
            
            if (fileFinder.IsAFile(s))
            {
                switch (foundSeparator)
                {
                    case false:
                        filesBefore.Add(s);
                        break;
                    case true:
                        filesAfter.Add(s);
                        break;
                }
            }
        }

        return (filesBefore.ToArray(), filesAfter.ToArray());
    }
    
    /// <summary>
    /// Returns a tuple of files that appear before and after a separator in a string array;
    /// </summary>
    /// <param name="args">The IEnumerable of type string to be searched.</param>
    /// <param name="separator">The separator to look for.</param>
    /// <returns>a tuple containing the files before and after a separator if found; returns null if no files were found in the array.</returns>
    public static (IEnumerable<string> filesBeforeSeparator, IEnumerable<string> filesAfterSeparator)? GetFilesBeforeAndAfterSeparator(IEnumerable<string> args, string separator)
    {
        List<string> filesBefore = new List<string>();
        List<string> filesAfter = new List<string>();

        bool foundSeparator = false;
        
        foreach (string s in args)
        {
            if (s.ToLower().Equals(separator.ToLower()))
            {
                foundSeparator = true;
            }
            else
            {
                AlastairLundy.Extensions.IO.Files.FileFinder fileFinder =
                    new AlastairLundy.Extensions.IO.Files.FileFinder();
                
                if (fileFinder.IsAFile(s))
                {
                    switch (foundSeparator)
                    {
                        case false:
                            filesBefore.Add(s);
                            break;
                        case true:
                            filesAfter.Add(s);
                            break;
                    }
                }
            }
        }

        if (filesBefore.Count == 0 && filesAfter.Count == 0)
        {
            return null;
        }
        
        return (filesBefore.ToArray(), filesAfter.ToArray());
    }
    
    /// <summary>
    /// A method to return any file names found within a specified string array.
    /// </summary>
    /// <param name="arguments">The string array to be checked.</param>
    /// <returns>the file(s) if one was provided in the list of arguments; returns null otherwise.</returns>
    public static IEnumerable<string>? FindFileNamesInArgs(IEnumerable<string> arguments)
    {
        AlastairLundy.Extensions.IO.Files.FileFinder fileFinder = new AlastairLundy.Extensions.IO.Files.FileFinder();
        
        string[] enumerable = arguments as string[] ?? arguments.ToArray();
        
        if (FoundAFileInArgs(enumerable))
        {
            List<string> list = new();
            
            foreach (string arg in enumerable.Where(x => fileFinder.IsAFile(x)))
            {
                if (arg.Length > 3)
                {
                    if (arg[arg.Length - 3].Equals('.') || arg[arg.Length - 2].Equals('.'))
                    {
                        list.Add(arg);
                    }
                }
                
                if (arg.EndsWith(".txt") || arg.EndsWith(".rtf"))
                {
                    list.Add(arg);
                }
            }

            return list;
        }

        return null;
    }

    /// <summary>
    /// Returns the number of files found in a string array
    /// </summary>
    /// <param name="args">The array to be searched.</param>
    /// <returns>the number of files found in a string array; If none are found, 0 will be returned.</returns>
    public static int GetNumberOfFilesFoundInArgs(IEnumerable<string> args)
    {
        IEnumerable<string>? argsFound = FindFileNamesInArgs(args);

        if (argsFound != null)
        {
            return argsFound.Count();
        }
        // ReSharper disable once RedundantIfElseBlock
        else
        {
            return 0;
        }
    }
    
    /// <summary>
    /// A method to determine if a file name is contained within a string array.
    /// </summary>
    /// <param name="arguments">The string array to be searched.</param>
    /// <returns>true if a file is found within the specified string array; returns false otherwise.</returns>
    public static bool FoundAFileInArgs(IEnumerable<string> arguments)
    {
        AlastairLundy.Extensions.IO.Files.FileFinder fileFinder = new AlastairLundy.Extensions.IO.Files.FileFinder();
        
        bool[] argumentsBools = arguments.Select(a => fileFinder.IsAFile(a)).ToArray();

        return argumentsBools.Any(x => x == true);
    }
}