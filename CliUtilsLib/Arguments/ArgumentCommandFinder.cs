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

using System.Linq;
// ReSharper disable RedundantIfElseBlock

namespace CliUtilsLib.Arguments;

public static class ArgumentCommandFinder
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
        return (arguments.Contains("--license") || (acceptShortVersion && arguments.Contains("-l")) ||
                arguments.Contains(licenseDisplayCommand));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="acceptShortVersion"></param>
    /// <returns></returns>
    public static bool IsHelpMessageRequested(string[] arguments, bool acceptShortVersion)
    {
        return (arguments.Contains("--help") || (acceptShortVersion && arguments.Contains("-h")));
    }
}