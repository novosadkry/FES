using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace FES__File_Encryption_Software_
{
    static class Program
    {
        /// <summary>
        /// Encrypts a file
        /// </summary>
        /// <param name="path">Path of the file</param>
        /// <param name="buffersize">Size of the buffer (Multicycle Encryption)</param>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        public async static Task Encrypt(string path, long buffersize, IProgress<byte> progress)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            if (File.Exists(path + ".AES"))
                throw new IOException();

            long filesize = new FileInfo(path).Length;
            long bytesread = 0, index = 0;

            using (AesCryptoServiceProvider aesp = new AesCryptoServiceProvider())
            {
                aesp.Mode = CipherMode.CBC;
                aesp.GenerateIV();
                aesp.Key = Key.Value;

                using (ICryptoTransform ctransform = aesp.CreateEncryptor())
                {
                    while (bytesread < filesize)
                    {
                        long remaining = filesize - bytesread;
                        bool finalBlock = remaining < buffersize;

                        buffersize = finalBlock ? remaining : buffersize;
                        byte[] buffer = new byte[buffersize];

                        using (FileStream newfile = File.Open(path + ".AES", FileMode.Append, FileAccess.Write))
                        using (FileStream reader = File.OpenRead(path))
                        {
                            reader.Seek(index, SeekOrigin.Begin);
                            bytesread += await reader.ReadAsync(buffer, 0, buffer.Length);

                            if (finalBlock)
                                await Task.Run(() => buffer = ctransform.TransformFinalBlock(buffer, 0, buffer.Length).Concat(aesp.IV).ToArray());

                            else
                                await Task.Run(() => ctransform.TransformBlock(buffer, 0, buffer.Length, buffer, 0));

                            GC.Collect();

                            newfile.Seek(index, SeekOrigin.Begin);
                            await newfile.WriteAsync(buffer, 0, buffer.Length);

                            index = bytesread;
                        }

                        GC.Collect();
                        progress.Report((byte)(bytesread / (decimal)filesize * 100));
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts a file
        /// </summary>
        /// <param name="path">Path of the file</param>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="CryptographicException">Invalid cryptokey</exception>
        public async static Task Decrypt(string path, long buffersize, IProgress<byte> progress)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            if (File.Exists(path.Substring(0, path.Length - 4)))
                throw new IOException();

            long filesize = new FileInfo(path).Length;
            long bytesread = 0, index = 0;

            using (AesCryptoServiceProvider aesp = new AesCryptoServiceProvider())
            {
                byte[] IV = new byte[16];

                using (FileStream fs = File.OpenRead(path))
                {
                    fs.Seek(filesize - 16, SeekOrigin.Begin);
                    await fs.ReadAsync(IV, 0, 16);
                }

                aesp.Mode = CipherMode.CBC;
                aesp.IV = IV;
                aesp.Key = Key.Value;

                using (ICryptoTransform ctransform = aesp.CreateDecryptor())
                {
                    while (bytesread < filesize)
                    {
                        long remaining = filesize - bytesread;
                        bool finalBlock = remaining < buffersize;

                        buffersize = finalBlock ? remaining : buffersize;
                        byte[] buffer = new byte[buffersize];

                        using (FileStream newfile = File.Open(path.Substring(0, path.Length - 4), FileMode.Append, FileAccess.Write))
                        using (FileStream reader = File.OpenRead(path))
                        {
                            reader.Seek(index, SeekOrigin.Begin);
                            bytesread += await reader.ReadAsync(buffer, 0, buffer.Length);

                            if (finalBlock)
                                await Task.Run(() => buffer = ctransform.TransformFinalBlock(buffer, 0, buffer.Length - 16));

                            else
                                await Task.Run(() => ctransform.TransformBlock(buffer, 0, buffer.Length, buffer, 0));

                            GC.Collect();

                            newfile.Seek(index, SeekOrigin.Begin);
                            await newfile.WriteAsync(buffer, 0, buffer.Length);

                            index = bytesread;
                        }

                        GC.Collect();
                        progress.Report((byte)(bytesread / (decimal)filesize * 100));
                    }
                }
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (new LoginForm().ShowDialog() == DialogResult.OK)
                Application.Run(new MainForm());
        }
    }
}
