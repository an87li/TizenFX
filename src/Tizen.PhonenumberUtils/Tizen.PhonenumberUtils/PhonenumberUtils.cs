/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;

namespace Tizen.PhonenumberUtils
{
    /// <summary>
    /// The PhonenumberUtils class provides methods for parsing, formatting and normalizing phone numbers.
    /// </summary>
    public class PhonenumberUtils : IDisposable
    {
        private bool disposed = false;

        public PhonenumberUtils()
        {
            int ret;

            ret = Interop.PhonenumberUtils.Connect();
            if (ret != (int)PhonenumberUtilsError.None)
            {
                Log.Error(Globals.LogTag, "Failed to connect, Error - " + (PhonenumberUtilsError)ret);
                PhonenumberUtilsErrorFactory.ThrowPhonenumberUtilsException(ret);
            }
        }

        ~PhonenumberUtils()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all resources used by the PhonenumberUtils.
        /// It should be called after finished using of the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            // Free unmanaged objects
            int ret;

            ret = Interop.PhonenumberUtils.Disconnect();
            if (ret != (int)PhonenumberUtilsError.None)
            {
                Log.Error(Globals.LogTag, "Failed to disconnect, Error - " + (PhonenumberUtilsError)ret);
                PhonenumberUtilsErrorFactory.ThrowPhonenumberUtilsException(ret);
            }

            disposed = true;
        }

        /// <summary>
        /// Gets the location string from number, region, and language.
        /// </summary>
        /// <param name="number">The number</param>
        /// <param name="region">The region of number</param>
        /// <param name="language">The language of location</param>
        /// <returns>The location string</returns>
        /// <exception cref="InvalidOperationException">Thrown when method failed due to invalid operation</exception>
        /// <exception cref="NotSupportedException">Thrown when phonenumber-utils is not supported</exception>
        /// <exception cref="ArgumentException">Thrown when input coordinates are invalid</exception>
        /// <exception cref="OutOfMemoryException">Thrown when failed due to out of memory</exception>
        public string GetLocationFromNumber(string number, Region region, Language language)
        {
            int ret;
            string result;

            ret = Interop.PhonenumberUtils.GetLocationFromNumber(number, (int)region, (int)language, out result);
            if (ret != (int)PhonenumberUtilsError.None)
            {
                Log.Error(Globals.LogTag, "Failed to get location, Error - " + (PhonenumberUtilsError)ret);
                PhonenumberUtilsErrorFactory.ThrowPhonenumberUtilsException(ret);
            }

            return result;
        }

        /// <summary>
        /// Gets the formatted number.
        /// </summary>
        /// <param name="number">The number</param>
        /// <param name="region">The region of number</param>
        /// <returns>The formatted number string</returns>
        /// <exception cref="InvalidOperationException">Thrown when method failed due to invalid operation</exception>
        /// <exception cref="NotSupportedException">Thrown when phonenumber-utils is not supported</exception>
        /// <exception cref="ArgumentException">Thrown when input coordinates are invalid</exception>
        /// <exception cref="OutOfMemoryException">Thrown when failed due to out of memory</exception>
        public string GetFormattedNumber(string number, Region region)
        {
            int ret;
            string result;

            ret = Interop.PhonenumberUtils.GetFormmatedNumber(number, (int)region, out result);
            if (ret != (int)PhonenumberUtilsError.None)
            {
                Log.Error(Globals.LogTag, "Failed to get formatted number, Error - " + (PhonenumberUtilsError)ret);
                PhonenumberUtilsErrorFactory.ThrowPhonenumberUtilsException(ret);
            }

            return result;
        }

        /// <summary>
        /// Gets the normalized number.
        /// </summary>        
        /// <param name="number">The number</param>
        /// <returns>The normalized number</returns>
        /// <privilege>http://tizen.org/privilege/telephony</privilege>
        /// <feature>http://tizen.org/feature/network.telephony</feature>
        /// <exception cref="InvalidOperationException">Thrown when method failed due to invalid operation</exception>
        /// <exception cref="NotSupportedException">Thrown when phonenumber-utils is not supported</exception>
        /// <exception cref="ArgumentException">Thrown when input coordinates are invalid</exception>
        /// <exception cref="OutOfMemoryException">Thrown when failed due to out of memory</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when application does not have proper privileges</exception>
        /// <remarks>
        /// Normalized number starts with plus('+') and country code, and excludes the separators such as dash or space. 
        /// It is a format of E.164 standard including the country code based on current network.
        /// </remarks>
        public string GetNormalizedNumber(string number)
        {
            int ret;
            string result;

            ret = Interop.PhonenumberUtils.GetNormailizedNumber(number, out result);
            if (ret != (int)PhonenumberUtilsError.None)
            {
                Log.Error(Globals.LogTag, "Failed to get normalized number, Error - " + (PhonenumberUtilsError)ret);
                PhonenumberUtilsErrorFactory.ThrowPhonenumberUtilsException(ret);
            }

            return result;
        }
    }
}
