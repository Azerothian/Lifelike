using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Lifelike.Data.Membership.Util
{
	internal static class KeyCreator
	{
		#region Main for Testing
		//public static void main(string[] args)
		//{
		//    string decryptionKey = CreateKey(Convert.ToInt32(args[1]));
		//    string validationKey = CreateKey(Convert.ToInt32(args[2]));
		//    Console.WriteLine("<machinekey validationkey=\"{0}\" decryptionkey=\"{1}\" validation=\"sha1\"/>", validationKey, decryptionKey);
		//}
		#endregion Main for Testing

		#region Operations
		/// <summary>
		/// Creates a key based on the indicated size.
		/// </summary>
		/// <param name="numBytes">size of the key.</param>
		/// <returns>Generated key of the specified length.</returns>
		public static string CreateKey(int numBytes)
		{
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			byte[] buff = new byte[numBytes];
			rng.GetBytes(buff);
			return BytesToHexString(buff);
		}
		/// <summary>
		/// Converts the given byte array to a hex string representation.
		/// </summary>
		/// <param name="bytes">the data to convert.</param>
		/// <returns>Hexadecimal string representation of the given byte array./</returns>
		public static string BytesToHexString(byte[] bytes)
		{
			StringBuilder hexString = new StringBuilder(64);
			for (int counter = 0; counter < bytes.Length; counter++)
			{
				hexString.Append(string.Format("{0:X2}", bytes[counter]));
			}
			return hexString.ToString();
		}
		#endregion Operations
	}
}
