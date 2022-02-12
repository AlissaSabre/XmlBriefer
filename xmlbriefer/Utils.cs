using System;

namespace xmlbriefer
{
    /// <summary>Provides several utility methods.</summary>
    public static class Utils
    {
        /// <summary>Tests if a string consists only of XML white space characters.</summary>
        /// <param name="s">The string to be tested.</param>
        /// <returns>True if white space characters only, and false otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="s"/> is null.</exception>
        /// <remarks>
        /// <para>This method returns true for an empty string.</para>
        /// <para>
        /// <i>XML white space character</i> is as defined by XML specification.
        /// It is a subset of white space characters for that <see cref="System.Char.IsWhiteSpace(char)" /> returns true.
        /// </para>
        /// </remarks>
        public static bool IsXmlWhiteSpace(string s)
        {
            if (s is null) throw new ArgumentNullException(nameof(s));
            foreach (var c in s)
            {
                switch (c)
                {
                    case '\u0009': /* TAB */
                    case '\u000A': /* LF */
                    case '\u000D': /* CR */
                    case '\u0020': /* SP */
                        continue;
                    default:
                        return false;
                }
            }
            return true;
        }
    }
}