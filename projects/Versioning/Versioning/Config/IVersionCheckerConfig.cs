﻿// Acarus
// Copyright (C) 2015 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace DustInTheWind.Versioning.Config
{
    /// <summary>
    /// This is an interface to be used by the Version Checker window to access some
    /// configuration values like "CheckAtStartUp".
    /// </summary>
    public interface IVersionCheckerConfig
    {
        bool CheckAtStartup { get; set; }
        string Url { get; set; }
        VersionCheckerConfigurationSection ConfigSection { get; }

        event EventHandler CheckAtStartupChanged;
        event EventHandler UrlChanged;
    }
}