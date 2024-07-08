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

// ReSharper disable UseIndexFromEndExpression

using System.Collections.Generic;

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
                if (FileFinder.IsAFile(s))
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
        if (FoundAFileInArgs(arguments))
        {
            List<string> list = new();
            
            foreach (string arg in arguments)
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

            return list.ToArray();
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
        foreach (string arg in arguments)
        {
            if (FileFinder.IsAFile(arg))
            {
                return FileFinder.IsAFile(arg);
            }
        }

        return false;
    }
}