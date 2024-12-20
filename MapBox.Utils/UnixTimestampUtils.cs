﻿//-----------------------------------------------------------------------
// <copyright file="PolylineUtils.cs" company="Mapbox">
//     Copyright (c) 2016 Mapbox. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Mapbox.Utils
{
    using System;

    /// <summary>
    /// A set of Unix Timestamp utils.
    /// </summary>
    public static class UnixTimestampUtils
    {
        /// <summary>
        /// Convert from Unitx timestamp to DateTime
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime From(double timestamp)
        {
            // return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Add(TimeSpan.FromSeconds(timestamp)).ToLocalTime();
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Add(TimeSpan.FromSeconds(timestamp));
        }

        // http://gigi.nullneuron.net/gigilabs/converting-tofrom-unix-timestamp-in-c/

        /// <summary>
        /// Convert from DateTime to Unix timestamp
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static double To(DateTime date)
        {
            // return date.ToLocalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            return date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }
    }
}