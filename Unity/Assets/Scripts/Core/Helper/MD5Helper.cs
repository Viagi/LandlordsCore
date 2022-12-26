using System.IO;
using System.Security.Cryptography;

namespace ET
{
	public static class MD5Helper
	{
		public static string FileMD5(string filePath)
		{
			byte[] retVal;
            using (FileStream file = new FileStream(filePath, FileMode.Open))
            {
	            MD5 md5 = MD5.Create();
				retVal = md5.ComputeHash(file);
			}
			return retVal.ToHex("x2");
		}

		public static string StringMD5(string input, string format = "x2")
		{
			using(MD5 md5 = MD5.Create())
			{
				byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
				byte[] hashBytes = md5.ComputeHash(bytes);

				return hashBytes.ToHex(format);
            }
		}
	}
}
