using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FES__File_Encryption_Software_
{
    static class Key
    {
        /// <summary>
        /// Value of the <see cref="Key"/>
        /// </summary>
        internal static byte[] Value { get; private set; }

        /// <summary>
        /// Sets a new value of the <see cref="Key"/>
        /// </summary>
        /// <param name="key">Key as <see cref="Byte"/> array generated using <see cref="GenerateKey"/></param>
        internal static async void SetValue(byte[] key)
        {
            await SQLAcc.UpdateKey(Convert.ToBase64String(key));
            Value = key;
        }

        /// <summary>
        /// Generates an AES key as <see cref="Byte"/> array
        /// </summary>
        /// <returns>AES key as <see cref="Byte"/> array</returns>
        public static byte[] Generate()
        {
            using (AesCryptoServiceProvider aesp = new AesCryptoServiceProvider())
            {
                aesp.Mode = CipherMode.CBC;
                aesp.GenerateKey();

                return aesp.Key;
            }
        }
    }
}
