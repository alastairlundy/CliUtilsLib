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

using System.IO;

namespace CliUtilsLib;

public class FileFinder
{
    /// <summary>
    /// Determines whether a string is the name of a file.
    /// </summary>
    /// <param name="arg">The string to be searched.</param>
    /// <returns>true if the string is a file; return false otherwise.</returns>
    public static bool IsAFile(string arg)
    {
        try
        {
            if (arg.Length > 1)
            {
                if (arg[arg.Length - 3].Equals('.') || arg[arg.Length - 2].Equals('.'))
                {
                    return true;
                }
            }
        
            if (arg.EndsWith(".txt") || arg.EndsWith(".rtf"))
            {
                return true;
            }
            
            return File.Exists(arg);
        }
        catch
        {
            return false;
        }
    }
}