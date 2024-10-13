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

using System.Linq;

namespace CliUtilsLib.Arguments;

public static class ArgumentFinder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="acceptShortVersion"></param>
    /// <param name="licenseDisplayCommand"></param>
    /// <returns></returns>
    public static bool IsLicenseDisplayRequested(string[] arguments, bool acceptShortVersion, string licenseDisplayCommand)
    {
        if (arguments.Contains("--license") || (acceptShortVersion && arguments.Contains("-l")) || arguments.Contains(licenseDisplayCommand))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="acceptShortVersion"></param>
    /// <returns></returns>
    public static bool IsHelpMessageRequested(string[] arguments, bool acceptShortVersion)
    {
        if (arguments.Contains("--help") || (acceptShortVersion && arguments.Contains("-h")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}